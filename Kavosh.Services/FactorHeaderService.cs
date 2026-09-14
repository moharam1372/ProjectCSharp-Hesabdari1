using System.ComponentModel;
using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Constants;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class FactorHeaderService
    {
        private readonly IFactorHeaderRepository _repository;
        private readonly IRepository<PaymentType> _paymentTypeRepository;
        private readonly DefinitiveAccountService _definitiveAccountService;
        private readonly StoreInfoService _storeInfoService;
        private readonly ProductUnitService _productUnitService;
        private readonly ChequeService _chequeService;

        private readonly IProductRepository _productRepository;
        private readonly AppSettingService _appSettingService;

        public FactorHeaderService(IFactorHeaderRepository repository, IRepository<PaymentType> paymentTypeRepository,
            DefinitiveAccountService definitiveAccountService, StoreInfoService storeInfoService,
            ProductUnitService productUnitService, ChequeService chequeService, IProductRepository productRepository, AppSettingService appSettingService)
        {
            _repository = repository;
            _paymentTypeRepository = paymentTypeRepository;
            _definitiveAccountService = definitiveAccountService;
            _storeInfoService = storeInfoService;
            _productUnitService = productUnitService;
            _chequeService = chequeService;
            _productRepository = productRepository;
            _appSettingService = appSettingService;
        }

        public async Task<FactorHeaderDto> GetLastFactorAsync()
        {
            // GetAllWithPersonAsync از قبل نزولی بر اساس Code مرتب‌شده
            var factors = await _repository.GetAllWithPersonAsync();
            var last = factors.FirstOrDefault();

            return last is null ? null : ToListDto(last);
        }

        public async Task<List<FactorHeaderDto>> GetAllFactorsAsync()
        {
            var factors = await _repository.GetAllWithPersonAsync();
            return factors.Select(ToListDto).ToList();
        }

        // نسخه‌ی سبک برای گرید — بدون Details/HowToPays (که فقط موقع باز کردن تک فاکتور لازمه)
        private static FactorHeaderDto ToListDto(FactorHeader f)
        {
            double mal = (f.Malyat1 + (double)f.Malyat2) / 100;
            long fPriceTotal = (long)((f.PriceTotal + (f.PriceTotal * mal)) - f.Discount + f.Freight);
            return new()
            {
                Id = f.Id,
                Code = f.Code,
                PersonId = f.PersonId,
                PersonName = f.Person?.FullName,
                Type = f.Type,
                DateFactor = f.DateFactor,
                Discount = f.Discount,
                //PriceTotal = f.PriceTotal,
                PriceTotal = fPriceTotal,
                Malyat1 = f.Malyat1,
                Malyat2 = f.Malyat2,
                Description = f.Description,
                Freight = f.Freight
            };
        }

        public async Task<long> GetNextCodeAsync()
        {
            var maxCode = await _repository.GetMaxCodeAsync();
            return maxCode + 1;
        }

        public async Task<FactorHeaderDto> GetFactorByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdWithDetailsAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<Guid> SaveFactorAsync(FactorHeaderDto dto)
        {
            Validate(dto);

            if (dto.Id != Guid.Empty)
            {
                var documentedIds = await _repository.GetDocumentedFactorIdsAsync();   // 👈 اصلاح شد
                if (documentedIds.Contains(dto.Id))
                    throw new InvalidOperationException("این فاکتور در سند صندوق ثبت شده است و قابل ویرایش نیست. برای ویرایش، ابتدا سند مربوطه را از صندوق حذف کنید.");
            }

            ValidateHowToPays(dto.HowToPays);
            await ValidateStockAsync(dto);

            var oldHowToPays = dto.Id != Guid.Empty
                ? await _repository.GetHowToPaySnapshotAsync(dto.Id)
                : new List<HowToPay>();

            // 👇 فاز ۱ — پاکسازی: باید حتماً قبل از حذف/تغییر فیزیکی ردیف‌های HowToPay در دیتابیس انجام بشه
            // وگرنه به خاطر FK Restrict روی DefinitiveAccount.HowToPayId، حذف HowToPay با خطا مواجه می‌شه
            // و همینطور چک‌ها قبل از این‌که HowToPayId شون NULL بشه، باید پیدا و حذف بشن
            await CleanupObsoleteDefinitiveAccountsAsync(dto.HowToPays, oldHowToPays);

            var calculatedTotal = dto.Details.Sum(d => (long)(d.Count * d.SellPrice)) - dto.Discount + dto.Freight;

            var header = new FactorHeader
            {
                Id = dto.Id,
                Code = dto.Code,
                PersonId = dto.PersonId,
                MarketerId = dto.MarketerId,
                Type = dto.Type,
                DateFactor = dto.DateFactor,
                Discount = dto.Discount,
                Freight = dto.Freight,
                PriceTotal = calculatedTotal,
                Malyat1 = dto.Malyat1,
                Malyat2 = dto.Malyat2,
                Description = dto.Description
            };

            var details = dto.Details.Select(d => new FactorDetail
            {
                Id = d.Id,
                ProductId = d.ProductId,
                Count = d.Count,
                PriceUnit = d.PriceUnit,
                SellPrice = d.SellPrice,
            }).ToList();

            var howToPays = dto.HowToPays.Select(p => new HowToPay
            {
                Id = p.Id,
                PaymentTypeId = p.PaymentTypeId,
                Price = p.Price,
                CheckNumber = p.CheckNumber,
                CheckDate = p.CheckDate,
                Settlement = p.Settlement,
                Description = p.Description
            }).ToList();

            var savedId = await _repository.SaveWithDetailsAsync(header, details, howToPays);
            await _repository.SaveChangesAsync();

            // 👇 فاز ۲ — حالا که HowToPayها ذخیره شدن و پاکسازی قدیمی‌ها انجام شده، بدهی/چک‌های جدید یا آپدیت‌شده ثبت می‌شن
            await CreateOrUpdateDefinitiveAccountsAsync(dto.PersonId, dto.Code, dto.Type, howToPays, oldHowToPays);

            return savedId;
        }

        // ============= فاز ۱: پاکسازی قبل از تغییرات ساختاری =============
        // برای هر ردیف قدیمی که «بدهی یا چک» بوده: اگه حذف شده یا نوعش از این دو دسته خارج شده
        // (یا بین بدهی↔چک عوض شده)، رکورد DefinitiveAccount و در صورت لزوم رکورد Cheque مرتبط پاک می‌شن
        private async Task CleanupObsoleteDefinitiveAccountsAsync(List<HowToPayDto> currentHowToPays, List<HowToPay> oldHowToPays)
        {
            var currentById = currentHowToPays
                .Where(x => x.Id != Guid.Empty)
                .ToDictionary(x => x.Id);

            foreach (var old in oldHowToPays)
            {
                bool oldWasDebtOrCheck = old.PaymentTypeId == PaymentTypeIds.Debtor || old.PaymentTypeId == PaymentTypeIds.Check;
                if (!oldWasDebtOrCheck)
                    continue;   // نوع قبلی نقد/کارت بوده، چیزی برای پاکسازی نیست

                var stillExists = currentById.TryGetValue(old.Id, out var current);

                if (!stillExists)
                {
                    // ردیف کاملاً از فاکتور حذف شده
                    await _definitiveAccountService.RemoveDebtByHowToPayIdAsync(old.Id);
                    continue;
                }

                bool sameCategory =
                    (old.PaymentTypeId == PaymentTypeIds.Debtor && current.PaymentTypeId == PaymentTypeIds.Debtor) ||
                    (old.PaymentTypeId == PaymentTypeIds.Check && current.PaymentTypeId == PaymentTypeIds.Check);

                if (!sameCategory)
                {
                    // نوع تغییر کرده (بدهی↔چک، یا به نقد/کارت) — رکورد قدیمی پاک می‌شه تا فاز ۲ از نو بسازدش
                    await _definitiveAccountService.RemoveDebtByHowToPayIdAsync(old.Id);
                }
            }
        }

        // ============= فاز ۲: ایجاد/آپدیت پس از ذخیره‌ی ساختاری =============
        private async Task CreateOrUpdateDefinitiveAccountsAsync(Guid personId, long factorCode, bool factorType,
            List<HowToPay> howToPays, List<HowToPay> oldHowToPays)
        {
            var oldById = oldHowToPays.ToDictionary(x => x.Id);

            foreach (var hp in howToPays)
            {
                bool isDebtType = hp.PaymentTypeId == PaymentTypeIds.Debtor;
                bool isCheckType = hp.PaymentTypeId == PaymentTypeIds.Check;

                if (isCheckType)
                {
                    // چه ردیف کاملاً جدید باشه چه چکی که نوعش عوض نشده - اطلاعات چک ثبت/آپدیت می‌شه
                    await _chequeService.CreateOrUpdateFromHowToPayAsync(hp.Id, personId, hp.CheckNumber, hp.CheckDate, hp.Price, isReceived: factorType);
                }

                if (!isDebtType && !isCheckType)
                    continue;   // نقد/کارت - پاکسازی احتمالیِ لازم قبلاً در فاز ۱ انجام شده

                var old = oldById.GetValueOrDefault(hp.Id);
                bool sameCategoryAsBefore = old is not null &&
                    ((isDebtType && old.PaymentTypeId == PaymentTypeIds.Debtor) ||
                     (isCheckType && old.PaymentTypeId == PaymentTypeIds.Check));

                if (!sameCategoryAsBefore)
                {
                    // ردیف جدید است، یا نوعش تغییر کرده (که در فاز ۱ پاک شد) - باید از نو بدهی ساخته بشه
                    await _definitiveAccountService.CreateDebtFromHowToPayAsync(personId, hp.Id, hp.Price, factorCode, isCheckType, factorType);

                    if (isCheckType && hp.Settlement)
                        await _definitiveAccountService.SettleCheckByHowToPayIdAsync(hp.Id);
                }
                else
                {
                    // همون دسته‌ی قبلی (بدهی یا چک، بدون تغییر نوع) - فقط آپدیت مبلغ/تسویه
                    if (old.Price != hp.Price)
                        await _definitiveAccountService.UpdateDebtPriceAsync(hp.Id, hp.Price);

                    if (isCheckType && !old.Settlement && hp.Settlement)
                        await _definitiveAccountService.SettleCheckByHowToPayIdAsync(hp.Id);
                }
            }
        }





        public async Task DeleteFactorAsync(Guid id)
        {
            var documentedIds = await _repository.GetDocumentedFactorIdsAsync();   // 👈 اصلاح شد
            if (documentedIds.Contains(id))
                throw new InvalidOperationException("این فاکتور در سند صندوق ثبت شده است و قابل حذف نیست. برای حذف، ابتدا سند مربوطه را از صندوق حذف کنید.");

            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static void Validate(FactorHeaderDto dto)
        {
            if (dto.PersonId == Guid.Empty)
                throw new ArgumentException("انتخاب طرف حساب الزامی است");

            if (dto.Details is null || dto.Details.Count == 0)
                throw new ArgumentException("حداقل یک ردیف کالا باید ثبت شود");

            foreach (var d in dto.Details)
            {
                if (d.ProductId == Guid.Empty)
                    throw new ArgumentException("انتخاب کالا برای همه‌ی ردیف‌ها الزامی است");

                if (d.Count <= 0)
                    throw new ArgumentException("تعداد باید بیشتر از صفر باشد");
            }
        }

        private static void ValidateHowToPays(List<HowToPayDto> howToPays)
        {
            if (howToPays is null || howToPays.Count == 0)
                return;

            foreach (var hp in howToPays)
            {
                if (hp.PaymentTypeId == Guid.Empty)
                    throw new ArgumentException("انتخاب نوع پرداخت الزامی است");

                if (hp.Price <= 0)
                    throw new ArgumentException("مبلغ پرداخت باید بیشتر از صفر باشد");

                if (hp.PaymentTypeId == PaymentTypeIds.Check)
                {
                    if (string.IsNullOrWhiteSpace(hp.CheckNumber))
                        throw new ArgumentException("برای پرداخت چکی، شماره چک الزامی است");

                    if (hp.CheckDate is null || hp.CheckDate == default)
                        throw new ArgumentException("برای پرداخت چکی، تاریخ چک الزامی است");
                }
            }
        }

        // تبدیل به مدل مخصوص چاپ
        public async Task<FactorReportDto> GetFactorReportDataAsync(Guid factorId)
        {
            var units = await _productUnitService.GetAllAsync();
            var factor = await GetFactorByIdAsync(factorId);
            if (factor is null) return null;

            var storeInfo = await _storeInfoService.GetAsync();
            var taxPercent = storeInfo?.TaxPercent ?? 0;
            var taxAmount = (long)(factor.PriceTotal * taxPercent / 100);

            // محاسبه‌ی واقعی بدهی قبلی (بدون احتساب همین فاکتور)
            var previousDebt = await _definitiveAccountService.GetPreviousDebtAsync(factor.PersonId, factor.Code);

            // مبلغ قابل پرداخت = جمع کل (این فاکتور + مالیاتش) + بدهی قبلی
            var payable = factor.PriceTotal + taxAmount + previousDebt;

            return new FactorReportDto
            {
                Header = storeInfo?.StoreName ?? "",
                Num = factor.Code.ToString(),
                Date = factor.DateFactor,
                Buyer = factor.PersonName,
                Mobile = factor.PersonMobile,
                Address = factor.PersonAddress,
                Malyat1 = factor.Malyat1,
                Malyat2 = factor.Malyat2,
                Description = factor.Description,
                Freight = factor.Freight,
                FactorDetails = factor.Details.Select(d => new FactorReportDetailDto
                {
                    ProductTitle = d.ProductTitle,
                    Count = d.Count,
                    // در چاپ، «مبلغ واحد» همان مبلغ فروش است (نه مبلغ خرید)
                    PriceUnit = d.SellPrice,
                    UnitTitle = units.First(f => f.Id == d.UnitId).Title
                }).ToList(),

                HowToPays = factor.HowToPays.Select(p => new HowToPayReportDto
                {
                    PaymentTypeTitle = p.PaymentTypeTitle,
                    Price = p.Price,
                    CheckNumber = p.CheckNumber,
                    CheckDate = p.CheckDate,
                    Settlement = p.Settlement,
                    Description = p.Description
                }).ToList(),

                Discount = factor.Discount,
                PriceTotal = factor.PriceTotal,
                TaxAmount = taxAmount,
                PreviousDebt = previousDebt,
                PayableAmount = payable,
                BankName = storeInfo?.BankName,
                AddressSeller = storeInfo?.Address,
                PhoneSeller = storeInfo?.Phone,
                AccountHolderName = storeInfo?.AccountHolderName,
                CardNumber = storeInfo?.CardNumber,
                ShabaNumber = storeInfo?.ShabaNumber,
                Logo = storeInfo?.Logo,
                Mohr = storeInfo?.Mohr
            };
        }

        private static FactorHeaderDto ToDto(FactorHeader f) => new()
        {
            Id = f.Id,
            Code = f.Code,
            PersonId = f.PersonId,
            PersonName = f.Person?.FullName,
            PersonMobile = f.Person?.Mobile,
            PersonAddress = f.Person?.Address,
            Type = f.Type,
            DateFactor = f.DateFactor,
            Discount = f.Discount,
            PriceTotal = f.PriceTotal,
            Malyat1 = f.Malyat1,
            Malyat2 = f.Malyat2,
            MarketerId = f.MarketerId,
            MarketerFullName = f.Marketer?.FullName,
            Description = f.Description,
            Freight = f.Freight,
            Details = f.FactorDetails.Select(d => new FactorDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                ProductTitle = d.Product?.Title,
                Count = d.Count,
                PriceUnit = d.PriceUnit,
                SellPrice = d.SellPrice,
                UnitId = d.Product.ProductUnitId
            }).ToList(),

            HowToPays = f.HowToPays.Select(p => new HowToPayDto
            {
                Id = p.Id,
                PaymentTypeId = p.PaymentTypeId,
                PaymentTypeTitle = p.PaymentType?.Title,
                Price = p.Price,
                CheckNumber = p.CheckNumber,
                CheckDate = p.CheckDate,
                Settlement = p.Settlement,
                Description = p.Description
            }).ToList()
        };

        // ============= بررسی موجودی =============
        private async Task ValidateStockAsync(FactorHeaderDto dto)
        {
            if (!dto.Type)
                return;   // فقط برای فاکتور فروش بررسی می‌شود

            var setting = await _appSettingService.GetAsync();
            if (!setting.PreventNegativeInventory)
                return;

            // اگر فاکتور ویرایش می‌شود، مقدار قبلی هر کالا باید به موجودی برگردد
            // چون قراره جایگزین بشه (نه اضافه بشه)
            var oldCounts = new Dictionary<Guid, float>();
            if (dto.Id != Guid.Empty)
            {
                var existingHeader = await _repository.GetByIdWithDetailsAsync(dto.Id);
                if (existingHeader is not null)
                {
                    oldCounts = existingHeader.FactorDetails
                        .GroupBy(d => d.ProductId)
                        .ToDictionary(g => g.Key, g => g.Sum(d => d.Count));
                }
            }

            foreach (var detail in dto.Details)
            {
                var product = await _productRepository.GetById(detail.ProductId);
                if (product is null) continue;

                var (input, output) = await _productRepository.GetStockMovementAsync(detail.ProductId);
                var currentStock = product.InitialInventory + input - output;

                if (oldCounts.TryGetValue(detail.ProductId, out var oldCount))
                    currentStock += oldCount;   // مقدار قبلیِ همین فاکتور رو برگردون به موجودی

                if (currentStock < detail.Count)
                    throw new ArgumentException(
                        $"موجودی کالای «{product.Title}» کافی نیست (موجودی فعلی: {currentStock:N0})");
            }
        }
    }
}