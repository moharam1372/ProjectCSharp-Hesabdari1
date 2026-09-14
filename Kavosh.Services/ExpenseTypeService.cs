using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class ExpenseTypeService
    {
        private readonly IExpenseTypeRepository _repository;

        public ExpenseTypeService(IExpenseTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExpenseTypeDto>> GetAllAsync()
        {
            var items = await _repository.GetAll();
            return items.Select(ToDto).ToList();
        }

        public async Task<Guid> SaveAsync(ExpenseTypeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("عنوان نوع هزینه الزامی است");

            var existing = await _repository.GetByTitleAsync(dto.Title);
            var isDuplicate = existing is not null && existing.Id != dto.Id;
            if (isDuplicate)
                throw new ArgumentException("این عنوان قبلاً ثبت شده است");

            var (entity, isNew) = await _repository.GetOrNew(dto.Id);
            entity.Title = dto.Title;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var hasExpenses = await _repository.HasExpensesAsync(id);
            if (hasExpenses)
                throw new InvalidOperationException("این نوع هزینه استفاده شده و قابل حذف نیست");

            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static ExpenseTypeDto ToDto(ExpenseType e) => new()
        {
            Id = e.Id,
            Title = e.Title
        };
    }
}