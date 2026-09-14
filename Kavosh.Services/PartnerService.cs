using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class PartnerService
    {
        private readonly IPartnerRepository _repository;

        public PartnerService(IPartnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PartnerDto>> GetAllAsync()
        {
            var items = await _repository.GetAll();
            return items.Select(ToDto).ToList();
        }

        public async Task<PartnerDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<Guid> SaveAsync(PartnerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("نام شریک الزامی است");

            var existing = await _repository.GetByFullNameAsync(dto.FullName);
            var isDuplicate = existing is not null && existing.Id != dto.Id;
            if (isDuplicate)
                throw new ArgumentException("این نام قبلاً ثبت شده است");

            var (entity, isNew) = await _repository.GetOrNew(dto.Id);
            entity.FullName = dto.FullName;
            entity.PhoneNumber = dto.PhoneNumber;

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
                throw new InvalidOperationException("این شریک دارای تراکنش هزینه است و قابل حذف نیست");

            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static PartnerDto ToDto(Partner p) => new()
        {
            Id = p.Id,
            FullName = p.FullName,
            PhoneNumber = p.PhoneNumber
        };
    }
}