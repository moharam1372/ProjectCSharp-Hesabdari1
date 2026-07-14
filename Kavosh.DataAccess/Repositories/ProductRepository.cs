using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    // IProductRepository.cs
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithDetailsAsync();
        Task<Product> GetByIdWithDetailsAsync(Guid id);
        Task<Product> GetByCodeAsync(long code);
        Task<long> GetMaxCodeAsync();   // 👈 جدید
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
        // ProductRepository.cs
        public async Task<long> GetMaxCodeAsync()
        {
            // شامل رکوردهای Soft-Delete شده هم میشه تا کد تکراری تولید نشه
            return await _dbSet.MaxAsync(p => (long?)p.ProductCode) ?? 0;
        }
        public async Task<List<Product>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(p => p.ProductGroup)
                .Include(p => p.ProductUnit)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.ProductGroup)
                .Include(p => p.ProductUnit)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetByCodeAsync(long code)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ProductCode == code);
        }
    }
}