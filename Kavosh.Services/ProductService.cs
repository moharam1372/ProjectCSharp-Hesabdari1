using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        // ============= خواندن =============
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllWithDetailsAsync();
            return products.Select(ToDto).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdWithDetailsAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        // ============= ذخیره (افزودن + ویرایش با یک متد) =============
        // متد عمومی که خودتون هم می‌تونید صداش بزنید (مثلاً برای نمایش پیش‌فرض توی فرم قبل از ذخیره)
        public async Task<long> GetNextProductCodeAsync()
        {
            var maxCode = await _repository.GetMaxCodeAsync();
            return maxCode + 1;
        }

        public async Task<Guid> SaveProductAsync(ProductDto dto)
        {
            var (entity, isNew) = await _repository.GetOrNew(dto.Id);

            // اگه کاربر کد وارد نکرده باشه (۰ یا کمتر)، خودکار تولید میشه
            if (dto.ProductCode <= 0)
                dto.ProductCode = await GetNextProductCodeAsync();

            await ValidateAsync(dto, isNew);


            entity.ProductCode = dto.ProductCode;
            entity.ProductGroupId = dto.ProductGroupId;
            entity.Title = dto.Title;
            entity.ProductUnitId = dto.ProductUnitId;
            entity.InitialInventory = dto.InitialInventory;
            entity.UnitPrice = dto.UnitPrice;
            entity.SellPrice = dto.SellPrice;
            entity.Description = dto.Description;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        // ============= حذف (Soft Delete) =============
        public async Task DeleteProductAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null)
                return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        // ============= کمکی =============
        private async Task ValidateAsync(ProductDto dto, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("نام کالا الزامی است");

            if (dto.ProductCode <= 0)
                throw new ArgumentException("کد کالا الزامی است");

            if (dto.ProductGroupId == Guid.Empty)
                throw new ArgumentException("انتخاب گروه کالا الزامی است");

            if (dto.ProductUnitId == Guid.Empty)
                throw new ArgumentException("انتخاب واحد سنجش الزامی است");

            var existing = await _repository.GetByCodeAsync(dto.ProductCode);
            var isDuplicate = existing is not null && (isNew || existing.Id != dto.Id);

            if (isDuplicate)
                throw new ArgumentException("این کد کالا قبلاً ثبت شده است");
        }

        private static ProductDto ToDto(Product p) => new()
        {
            Id = p.Id,
            ProductCode = p.ProductCode,
            ProductGroupId = p.ProductGroupId,
            ProductGroupTitle = p.ProductGroup?.Title,
            Title = p.Title,
            ProductUnitId = p.ProductUnitId,
            ProductUnitTitle = p.ProductUnit?.Title,
            InitialInventory = p.InitialInventory,
            UnitPrice = p.UnitPrice,
            SellPrice = p.SellPrice,
            Description = p.Description,

            InputStock = 0,
            OutputStock = 0,
            CurrentStock = p.InitialInventory
        };
        private static ProductDto ToDto(Product p, float input, float output) => new()
        {
            Id = p.Id,
            ProductCode = p.ProductCode,
            ProductGroupId = p.ProductGroupId,
            ProductGroupTitle = p.ProductGroup?.Title,
            Title = p.Title,
            ProductUnitId = p.ProductUnitId,
            ProductUnitTitle = p.ProductUnit?.Title,
            InitialInventory = p.InitialInventory,
            UnitPrice = p.UnitPrice,
            SellPrice = p.SellPrice,
            Description = p.Description,

            InputStock = input,
            OutputStock = output,
            CurrentStock = p.InitialInventory + input - output
        };
    }
}