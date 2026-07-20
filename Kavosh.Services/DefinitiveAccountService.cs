using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class DefinitiveAccountService
    {
        private readonly IDefinitiveAccountRepository _repository;

        public DefinitiveAccountService(IDefinitiveAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DefinitiveAccountDto>> GetStatementAsync(Guid personId)
        {
            var items = await _repository.GetByPersonAsync(personId);
            return items.Select(ToDto).ToList();
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
                Description = isCheck ? "بدهی ناشی از چک دریافتی" : "بدهی ناشی از فاکتور"
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

        private static DefinitiveAccountDto ToDto(DefinitiveAccount d) => new()
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
            HowToPayId = d.HowToPayId
        };
    }
}