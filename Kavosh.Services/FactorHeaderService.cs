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

        public FactorHeaderService(IFactorHeaderRepository repository, IRepository<PaymentType> paymentTypeRepository,
            DefinitiveAccountService definitiveAccountService, StoreInfoService storeInfoService,
            ProductUnitService productUnitService, ChequeService chequeService)
        {
            _repository = repository;
            _paymentTypeRepository = paymentTypeRepository;
            _definitiveAccountService = definitiveAccountService;
            _storeInfoService = storeInfoService;
            _productUnitService = productUnitService;
            _chequeService = chequeService;
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
        private static FactorHeaderDto ToListDto(FactorHeader f) => new()
        {
            Id = f.Id,
            Code = f.Code,
            PersonId = f.PersonId,
            PersonName = f.Person?.FullName,
            Type = f.Type,
            DateFactor = f.DateFactor,
            Discount = f.Discount,
            PriceTotal = f.PriceTotal,
            Malyat1 = f.Malyat1,
            Malyat2 = f.Malyat2
        };

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
            ValidateHowToPays(dto.HowToPays);

            // 👇 Snapshot از وضعیت «قبل از ذخیره» برای تشخیص تغییرات (فقط اگه ویرایشه)
            var oldSettlements = dto.Id != Guid.Empty
                ? await _repository.GetHowToPaySettlementSnapshotAsync(dto.Id)
                : new Dictionary<Guid, bool>();

            // 👇 جمع کل فاکتور بر مبنای «مبلغ فروش» محاسبه می‌شود (نه مبلغ خرید)
            var calculatedTotal = dto.Details.Sum(d => (long)(d.Count * d.SellPrice)) - dto.Discount;

            var header = new FactorHeader
            {
                Id = dto.Id,
                Code = dto.Code,
                PersonId = dto.PersonId,
                Type = dto.Type,
                DateFactor = dto.DateFactor,
                Discount = dto.Discount,
                PriceTotal = calculatedTotal,
                Malyat1 = dto.Malyat1,
                Malyat2 = dto.Malyat2,

            };

            var details = dto.Details.Select(d => new FactorDetail
            {
                Id = d.Id,
                ProductId = d.ProductId,
                Count = d.Count,
                PriceUnit = d.PriceUnit,
                SellPrice = d.SellPrice,   // 👈 جدید
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

            // 👇 حالا که HowToPayها Id واقعی گرفتن، منطق DefinitiveAccount رو اجرا می‌کنیم
            //await SyncDefinitiveAccountsAsync(dto.PersonId, dto.Code, howToPays, oldSettlements);
            await SyncDefinitiveAccountsAsync(dto.PersonId, dto.Code, dto.Type, howToPays, oldSettlements);
            return savedId;
        }
        private async Task SyncDefinitiveAccountsAsync(Guid personId, long factorCode, bool factorType,
            List<HowToPay> howToPays, Dictionary<Guid, bool> oldSettlements)
        {
            foreach (var hp in howToPays)
            {
                bool isDebtType = hp.PaymentTypeId == PaymentTypeIds.Debtor;
                bool isCheckType = hp.PaymentTypeId == PaymentTypeIds.Check;

                if (!isDebtType && !isCheckType)
                    continue;

                var isNewRow = !oldSettlements.ContainsKey(hp.Id);

                if (isCheckType)
                {
                    // فروش (factorType=true) => چک دریافتی از مشتری / خرید (factorType=false) => چک صادرشده به تامین‌کننده
                    await _chequeService.CreateOrUpdateFromHowToPayAsync(hp.Id, personId, hp.CheckNumber, hp.CheckDate, hp.Price, isReceived: factorType);
                }

                if (isNewRow)
                {
                    // 👇 اصلاح شد — پارامتر factorType اضافه شد
                    // فروش (factorType=true) => شخص بدهکار است
                    // خرید (factorType=false) => ما بدهکاریم (شخص بستانکار است)
                    await _definitiveAccountService.CreateDebtFromHowToPayAsync(personId, hp.Id, hp.Price, factorCode, isCheckType, factorType);

                    if (isCheckType && hp.Settlement)
                        await _definitiveAccountService.SettleCheckByHowToPayIdAsync(hp.Id);
                }
                else if (isCheckType)
                {
                    var wasSettled = oldSettlements[hp.Id];
                    if (!wasSettled && hp.Settlement)
                        await _definitiveAccountService.SettleCheckByHowToPayIdAsync(hp.Id);
                }
            }
        }
        public async Task DeleteFactorAsync(Guid id)
        {
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

        // 👇 اصلاح‌شده: ساده‌تر، بدون کوئری اضافه به دیتابیس
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

                    if (hp.CheckDate is null || hp.CheckDate == default)   // 👈 چک Nullable
                        throw new ArgumentException("برای پرداخت چکی، تاریخ چک الزامی است");
                }
            }
        }

        // 👇 جدید — تبدیل به مدل مخصوص چاپ
        public async Task<FactorReportDto> GetFactorReportDataAsync(Guid factorId)
        {
            var units = await _productUnitService.GetAllAsync();
            var factor = await GetFactorByIdAsync(factorId);
            if (factor is null) return null;

            var storeInfo = await _storeInfoService.GetAsync();
            var taxPercent = storeInfo?.TaxPercent ?? 0;
            var taxAmount = (long)(factor.PriceTotal * taxPercent / 100);

            // 👇 محاسبه‌ی واقعی بدهی قبلی (بدون احتساب همین فاکتور)
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
                FactorDetails = factor.Details.Select(d => new FactorReportDetailDto
                {
                    ProductTitle = d.ProductTitle,
                    Count = d.Count,
                    // 👇 در چاپ، «مبلغ واحد» همان مبلغ فروش است (نه مبلغ خرید)
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
                PreviousDebt = previousDebt,        // 👈 اصلاح شد
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


            Details = f.FactorDetails.Select(d => new FactorDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                ProductTitle = d.Product?.Title,
                Count = d.Count,
                PriceUnit = d.PriceUnit,
                SellPrice = d.SellPrice,   // 👈 جدید
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
    }
}