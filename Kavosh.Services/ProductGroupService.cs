using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class ProductGroupService
    {
        private readonly IProductGroupRepository _repository;

        public ProductGroupService(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductGroupDto>> GetAllAsync()
        {
            var items = await _repository.GetAll();
            return items.Select(ToDto).ToList();
        }

        public async Task<ProductGroupDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<Guid> SaveAsync(ProductGroupDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("عنوان گروه الزامی است");

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
            var hasProducts = await _repository.HasProductsAsync(id);
            if (hasProducts)
                throw new InvalidOperationException("این گروه به یک یا چند کالا وصل است و قابل حذف نیست");

            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static ProductGroupDto ToDto(ProductGroup g) => new()
        {
            Id = g.Id,
            Title = g.Title
        };
    }
}