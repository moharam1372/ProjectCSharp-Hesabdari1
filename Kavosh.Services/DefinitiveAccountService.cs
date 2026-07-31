using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class DefinitiveAccountService
    {
        private readonly IDefinitiveAccountRepository _repository;
        private readonly ChequeService _chequeService;

        public DefinitiveAccountService(IDefinitiveAccountRepository repository, ChequeService chequeService)
        {
            _repository = repository;
            _chequeService = chequeService;
        }
        // در Kavosh.Services/DefinitiveAccountService.cs — کنار GetStatementAsync اضافه کن

        public async Task<long> GetPreviousDebtAsync(Guid personId, long excludeFactorCode)
        {
            var items = await _repository.GetByPersonAsync(personId);

            // مانده‌ی کل شخص، به‌جز ردیف‌هایی که مربوط به همین فاکتور (excludeFactorCode) هستن
            return items
                .Where(d => d.DocNumber != excludeFactorCode)
                .Sum(d => d.Debtor ? d.Price : -d.Price);
        }
        public async Task<(long TotalDebt, long CheckDebt, long OtherDebt)> GetDebtSummaryAsync()
        {
            var debtors = await GetDebtorsListAsync();   // منطق فیلتر (فقط بدهکارهای واقعی) از قبل توش هست

            var checkDebt = debtors.Sum(d => d.CheckDebt);
            var otherDebt = debtors.Sum(d => d.OtherDebt);

            return (checkDebt + otherDebt, checkDebt, otherDebt);
        }


        public async Task<List<DebtorSummaryDto>> GetDebtorsListAsync()
        {
            var all = await _repository.GetAllWithPersonAsync();

            return all
                .GroupBy(d => d.PersonId)
                .Select(g =>
                {
                    // چون چک وصول‌شده خودش یه رکورد بستانکار خنثی‌کننده داره،
                    // جمع‌زدن ساده‌ی این گروه خودکار مانده‌ی واقعی چک رو میده (وصول‌شده = صفر)
                    var checkDebt = g.Where(x => x.IsCheck).Sum(x => x.Debtor ? x.Price : -x.Price);
                    var otherDebt = g.Where(x => !x.IsCheck).Sum(x => x.Debtor ? x.Price : -x.Price);

                    var lastDebtDate = g.Where(x => x.Debtor)
                        .Select(x => x.DateCustom)
                        .DefaultIfEmpty()
                        .Max();

                    return new DebtorSummaryDto
                    {
                        PersonId = g.Key,
                        PersonName = g.First().Person?.FullName,
                        Mobile = g.First().Person?.Mobile,
                        LastDebtDate = lastDebtDate,
                        CheckDebt = checkDebt,
                        OtherDebt = otherDebt
                    };
                })
                .Where(x => x.TotalDebt > 0)              // فقط کسایی که واقعاً هنوز بدهکارن
                .OrderByDescending(x => x.TotalDebt)       // بدهکارترین‌ها اول (اولویت پیگیری)
                .ToList();
        }
        public async Task<List<DefinitiveAccountDto>> GetStatementAsync(Guid personId)
        {
            var items = await _repository.GetByPersonAsync(personId);

            // هر رکوردی که یه رکورد دیگه با SettledFromId بهش اشاره کرده باشه، یعنی قبلاً وصول شده
            var settledFromIds = items
                .Where(x => x.SettledFromId.HasValue)
                .Select(x => x.SettledFromId!.Value)
                .ToHashSet();

            return items.Select(d => ToDto(d, settledFromIds.Contains(d.Id))).ToList();
        }

        public async Task<long> GetNextCodeAsync()
        {
            var max = await _repository.GetMaxCodeAsync();
            return max + 1;
        }

        // فراخوانی خودکار از FactorHeaderService وقتی یه HowToPay از نوع «بدهی» یا «چک» تازه ثبت میشه
        public async Task CreateDebtFromHowToPayAsync(Guid personId, Guid howToPayId, long price, long factorCode, bool isCheck)
        {
            var nextCode = await GetNextCodeAsync();

            var entity = new DefinitiveAccount
            {
                Id = Guid.NewGuid(),
                Code = nextCode,
                DocNumber = factorCode,
                PersonId = personId,
                DateCustom = DateTime.Now,
                Price = price,
                Debtor = true,
                IsCheck = isCheck,
                HowToPayId = howToPayId,
                Description = isCheck ? "بدهی بابت چک" : $"بدهی بابت فاکتور"
            };

            await _repository.Add(entity);
            await _repository.SaveChangesAsync();
        }

        // وصول چک — چه از FrmFactor چه بعداً از صورت‌حساب مستقیم صدا زده بشه
        public async Task SettleCheckByHowToPayIdAsync(Guid howToPayId)
        {
            var debtEntry = await _repository.GetDebtByHowToPayIdAsync(howToPayId);
            if (debtEntry is null)
                return; // بدهی‌ای برای این پرداخت ثبت نشده بود (مثلاً پرداخت نقدی)

            await SettleCheckAsync(debtEntry.Id);
        }

        public async Task SettleCheckAsync(Guid definitiveAccountId)
        {
            var original = await _repository.GetById(definitiveAccountId);
            if (original is null)
                throw new InvalidOperationException("رکورد بدهی یافت نشد");

            if (!original.IsCheck || !original.Debtor)
                throw new InvalidOperationException("این رکورد بدهیِ چک نیست");

            var alreadySettled = await _repository.IsAlreadySettledAsync(definitiveAccountId);
            if (alreadySettled)
                return; // قبلاً وصول شده، دوباره خنثی‌کننده نسازیم

            var nextCode = await GetNextCodeAsync();

            var offsetting = new DefinitiveAccount
            {
                Id = Guid.NewGuid(),
                Code = nextCode,
                DocNumber = original.DocNumber,
                PersonId = original.PersonId,
                DateCustom = DateTime.Now,
                Price = original.Price,
                Debtor = false,                 // بستانکار — خنثی‌کننده‌ی بدهی چک
                IsCheck = true,
                SettledFromId = original.Id,
                HowToPayId = original.HowToPayId,
                Description = "وصول چک"
            };

            await _repository.Add(offsetting);
            await _repository.SaveChangesAsync();
        }


        // ============= ثبت مستقیم دریافت/پرداخت (بدون فاکتور) =============
        public async Task CreateManualTransactionAsync(ReceiptPaymentDto dto)
        {
            if (dto.PersonId == Guid.Empty)
                throw new ArgumentException("انتخاب طرف حساب الزامی است");

            if (dto.Price <= 0)
                throw new ArgumentException("مبلغ باید بیشتر از صفر باشد");

            if (dto.IsCheckPayment && string.IsNullOrWhiteSpace(dto.CheckNumber))
                throw new ArgumentException("برای پرداخت چکی، شماره چک الزامی است");

         

            var nextCode = await GetNextCodeAsync();

            var description = string.IsNullOrWhiteSpace(dto.Description)
                ? (dto.IsReceipt ? "دریافت وجه" : "پرداخت وجه")
                : dto.Description;

            if (dto.IsCheckPayment && !string.IsNullOrWhiteSpace(dto.CheckNumber))
                description += $" (چک شماره {dto.CheckNumber})";

            var entity = new DefinitiveAccount
            {
                Id = Guid.NewGuid(),
                Code = nextCode,
                DocNumber = nextCode,      // سند مستقل — بدون فاکتور
                PersonId = dto.PersonId,
                DateCustom = dto.DateCustom == default ? DateTime.Now : dto.DateCustom,
                Price = dto.Price,
                Debtor = !dto.IsReceipt,   // دریافت => بستانکار / پرداخت => بدهکار
                IsCheck = dto.IsCheckPayment,
                Description = description
            };
            await _repository.Add(entity);
            await _repository.SaveChangesAsync();

            // 👇 جدید — اگه پرداخت/دریافت با چک بوده، در جدول چک هم ثبت بشه
            if (dto.IsCheckPayment && dto.CheckDate.HasValue)
            {
                await _chequeService.CreateFromManualTransactionAsync(
                    entity.Id, dto.PersonId, dto.CheckNumber, dto.CheckDate.Value,
                    dto.Price, isReceived: dto.IsReceipt, description);
            }
        }

        private static DefinitiveAccountDto ToDto(DefinitiveAccount d, bool isSettled) => new()
        {
            Id = d.Id,
            Code = d.Code,
            DocNumber = d.DocNumber,
            PersonId = d.PersonId,
            PersonName = d.Person?.FullName,
            DateCustom = d.DateCustom,
            Price = d.Price,
            Debtor = d.Debtor,
            Description = d.Description,
            IsCheck = d.IsCheck,
            HowToPayId = d.HowToPayId,
            IsSettled = isSettled
        };
    }
}