using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Enums;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class DefinitiveAccountService
    {
        private readonly IDefinitiveAccountRepository _repository;
        private readonly ChequeService _chequeService;
        private readonly IRepository<HowToPay> _howToPayRepository;   // 👈 جدید

        public DefinitiveAccountService(IDefinitiveAccountRepository repository, ChequeService chequeService,
            IRepository<HowToPay> howToPayRepository)
        {
            _repository = repository;
            _chequeService = chequeService;
            _howToPayRepository = howToPayRepository;
        }

        public async Task<long> GetPreviousDebtAsync(Guid personId, long excludeFactorCode)
        {
            var items = await _repository.GetByPersonAsync(personId);
            return items
                .Where(d => d.DocNumber != excludeFactorCode)
                .Sum(d => d.Debtor ? d.Price : -d.Price);
        }

        public async Task<(long TotalDebt, long CheckDebt, long OtherDebt)> GetDebtSummaryAsync()
        {
            var debtors = await GetDebtorsListAsync();
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
                .Where(x => x.TotalDebt > 0)
                .OrderByDescending(x => x.TotalDebt)
                .ToList();
        }

        public async Task<List<DefinitiveAccountDto>> GetStatementAsync(Guid personId)
        {
            var items = await _repository.GetByPersonAsync(personId);

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
        public async Task CreateDebtFromHowToPayAsync(Guid personId, Guid howToPayId, long price, long factorCode,
            bool isCheck, bool factorType)
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
                Debtor = factorType,   // فروش => شخص بدهکار / خرید => ما بدهکاریم (شخص بستانکار)
                IsCheck = isCheck,
                HowToPayId = howToPayId,
                Description = isCheck
                    ? "بدهی بابت چک"
                    : (factorType ? "بدهی بابت فاکتور فروش" : "بستانکاری بابت فاکتور خرید")
            };

            await _repository.Add(entity);
            await _repository.SaveChangesAsync();
        }

        // فراخوانی از FactorHeaderService — وقتی چک‌باکس «تسویه» در فاکتور علامت خورده
        public async Task SettleCheckByHowToPayIdAsync(Guid howToPayId)
        {
            var debtEntry = await _repository.GetDebtByHowToPayIdAsync(howToPayId);
            if (debtEntry is null)
                return;

            await SettleCheckAsync(debtEntry.Id);
        }

        // 👇 جدید — فراخوانی از FrmChequeList (دکمه‌ی «پاس شد») — از روی خودِ چک شروع می‌کنه
        public async Task SettleCheckByChequeIdAsync(Guid chequeId)
        {
            var cheque = await _chequeService.GetEntityByIdAsync(chequeId);
            if (cheque is null) return;

            DefinitiveAccount debtEntry = null;

            if (cheque.HowToPayId.HasValue)
                debtEntry = await _repository.GetDebtByHowToPayIdAsync(cheque.HowToPayId.Value);
            else if (cheque.DefinitiveAccountId.HasValue)
                debtEntry = await _repository.GetById(cheque.DefinitiveAccountId.Value);

            if (debtEntry is not null)
            {
                await SettleCheckAsync(debtEntry.Id);   // خودش Cheque و HowToPay رو هم سینک می‌کنه
            }
            else
            {
                // حالت نادر: بدهی مرتبط پیدا نشد — فقط وضعیت خودِ چک آپدیت بشه
                await _chequeService.SyncStatusAsync(cheque.HowToPayId, cheque.DefinitiveAccountId, ChequeStatus.Cleared);
            }
        }

        // 👇 جدید — چک برگشت خورد. بدهی دست‌نخورده می‌مونه چون پولی دریافت/پرداخت نشده
        public async Task BounceChequeAsync(Guid chequeId)
        {
            var cheque = await _chequeService.GetEntityByIdAsync(chequeId);
            if (cheque is null) return;

            await _chequeService.SyncStatusAsync(cheque.HowToPayId, cheque.DefinitiveAccountId, ChequeStatus.Bounced);
        }

        // ============= مرکز اصلی وصول چک =============
        // از هر جا صدا زده بشه (FrmDefinitiveAccount, FrmPerson, ذخیره‌ی فاکتور، یا FrmChequeList)
        // همزمان: 1) رکورد خنثی‌کننده در DefinitiveAccounts   2) HowToPay.Settlement = true   3) Cheque.Status = Cleared
        public async Task SettleCheckAsync(Guid definitiveAccountId)
        {
            var original = await _repository.GetById(definitiveAccountId);
            if (original is null)
                throw new InvalidOperationException("رکورد بدهی یافت نشد");

            if (!original.IsCheck)
                throw new InvalidOperationException("این رکورد بدهیِ چک نیست");

            var alreadySettled = await _repository.IsAlreadySettledAsync(definitiveAccountId);
            if (!alreadySettled)
            {
                var nextCode = await GetNextCodeAsync();

                var offsetting = new DefinitiveAccount
                {
                    Id = Guid.NewGuid(),
                    Code = nextCode,
                    DocNumber = original.DocNumber,
                    PersonId = original.PersonId,
                    DateCustom = DateTime.Now,
                    Price = original.Price,
                    Debtor = !original.Debtor,   // عکسِ رکورد اصلی (چه بدهکار چه بستانکار)
                    IsCheck = true,
                    SettledFromId = original.Id,
                    HowToPayId = original.HowToPayId,
                    Description = original.Debtor ? "وصول چک دریافتی" : "پرداخت چک صادرشده"
                };

                await _repository.Add(offsetting);
                await _repository.SaveChangesAsync();
            }

            // 👇 جدید — هماهنگ‌سازی با HowToPay.Settlement و Cheque.Status
            // این بخش idempotent هست، حتی اگه از قبل تسویه شده باشه، اجرا میشه تا مطمئن بشیم همه‌جا سینکه
            if (original.HowToPayId.HasValue)
            {
                var howToPay = await _howToPayRepository.GetById(original.HowToPayId.Value);
                if (howToPay is not null && !howToPay.Settlement)
                {
                    howToPay.Settlement = true;
                    await _howToPayRepository.Update(howToPay);
                    await _howToPayRepository.SaveChangesAsync();
                }
            }

            await _chequeService.SyncStatusAsync(original.HowToPayId, original.Id, ChequeStatus.Cleared);
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
                DocNumber = nextCode,
                PersonId = dto.PersonId,
                DateCustom = dto.DateCustom == default ? DateTime.Now : dto.DateCustom,
                Price = dto.Price,
                Debtor = !dto.IsReceipt,
                IsCheck = dto.IsCheckPayment,
                Description = description
            };
            await _repository.Add(entity);
            await _repository.SaveChangesAsync();

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