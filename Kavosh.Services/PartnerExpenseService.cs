using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class PartnerExpenseService
    {
        private readonly IPartnerExpenseRepository _repository;
        private readonly IPartnerRepository _partnerRepository;   // 👈 جدید

        public PartnerExpenseService(IPartnerExpenseRepository repository, IPartnerRepository partnerRepository)
        {
            _repository = repository;
            _partnerRepository = partnerRepository;
        }

        public async Task<List<PartnerExpenseDto>> GetAllAsync()
        {
            var items = await _repository.GetAllWithDetailsAsync();
            return items.Select(ToDto).ToList();
        }

        // 👇 جدید — صورت‌حساب یک شریک خاص (مرتب بر اساس تاریخ برای محاسبه‌ی مانده‌ی تجمعی)
        public async Task<List<PartnerExpenseDto>> GetStatementByPartnerAsync(Guid partnerId)
        {
            var items = await _repository.GetAllWithDetailsAsync();
            return items
                .Where(e => e.PartnerId == partnerId)
                .OrderBy(e => e.DateCustom)
                .Select(ToDto)
                .ToList();
        }

        // 👇 جدید — جمع کل هر شریک (برای پنل خلاصه)
        public async Task<List<PartnerBalanceDto>> GetPartnerBalancesAsync()
        {
            var partners = await _partnerRepository.GetAll();
            var expenses = await _repository.GetAllWithDetailsAsync();

            return partners.Select(p =>
            {
                var partnerExpenses = expenses.Where(e => e.PartnerId == p.Id);
                var balance = partnerExpenses.Sum(e => e.IsPayment ? e.Amount : -e.Amount);

                return new PartnerBalanceDto
                {
                    PartnerId = p.Id,
                    PartnerFullName = p.FullName,
                    Balance = balance
                };
            })
            .OrderByDescending(x => Math.Abs(x.Balance))
            .ToList();
        }

        // 👇 جدید — برای بارگذاری کامل یک رکورد جهت ویرایش
        public async Task<PartnerExpenseDto> GetByIdAsync(Guid id)
        {
            var withDetails = (await _repository.GetAllWithDetailsAsync()).FirstOrDefault(e => e.Id == id);
            return withDetails is null ? null : ToDto(withDetails);
        }

        public async Task<Guid> SaveAsync(PartnerExpenseDto dto)
        {
            if (dto.PartnerId == Guid.Empty)
                throw new ArgumentException("انتخاب شریک الزامی است");

            if (dto.Amount <= 0)
                throw new ArgumentException("مبلغ باید بیشتر از صفر باشد");

            var (entity, isNew) = await _repository.GetOrNew(dto.Id);
            entity.PartnerId = dto.PartnerId;
            entity.ExpenseTypeId = dto.ExpenseTypeId;
            entity.Amount = dto.Amount;
            entity.IsPayment = dto.IsPayment;
            entity.DateCustom = dto.DateCustom == default ? DateTime.Now : dto.DateCustom;
            entity.Description = dto.Description;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static PartnerExpenseDto ToDto(PartnerExpense e) => new()
        {
            Id = e.Id,
            PartnerId = e.PartnerId,
            PartnerFullName = e.Partner?.FullName,
            ExpenseTypeId = e.ExpenseTypeId,
            ExpenseTypeTitle = e.ExpenseType?.Title,
            Amount = e.Amount,
            IsPayment = e.IsPayment,
            DateCustom = e.DateCustom,
            Description = e.Description
        };
    }
}