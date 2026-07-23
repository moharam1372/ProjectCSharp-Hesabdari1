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
        Task<long> GetMaxCodeAsync(); 
        Task<(float Input, float Output)> GetStockMovementAsync(Guid productId);
        Task<Dictionary<Guid, (float Input, float Output)>> GetStockMovementForAllAsync();
        // IProductRepository.cs — اضافه به اینترفیس موجود
        Task<List<FactorDetail>> GetKardexAsync(Guid productId);
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
        // ProductRepository.cs
        public async Task<List<FactorDetail>> GetKardexAsync(Guid productId)
        {
            return await _context.Set<FactorDetail>()
                .AsNoTracking()
                .Include(d => d.FactorHeader)
                .Where(d => d.ProductId == productId && !d.IsDeleted && !d.FactorHeader.IsDeleted)
                .OrderBy(d => d.FactorHeader.DateFactor)
                .ToListAsync();
        }
        // ProductRepository.cs
        public async Task<(float Input, float Output)> GetStockMovementAsync(Guid productId)
        {
            var details = await _context.Set<FactorDetail>()
                .Include(d => d.FactorHeader)
                .Where(d => d.ProductId == productId && !d.IsDeleted && !d.FactorHeader.IsDeleted)
                .ToListAsync();

            var input = details.Where(d => !d.FactorHeader.Type).Sum(d => d.Count);  // خرید = ورودی
            var output = details.Where(d => d.FactorHeader.Type).Sum(d => d.Count); // فروش = خروجی

            return (input, output);
        }

        public async Task<Dictionary<Guid, (float Input, float Output)>> GetStockMovementForAllAsync()
        {
            var details = await _context.Set<FactorDetail>()
                .Include(d => d.FactorHeader)
                .Where(d => !d.IsDeleted && !d.FactorHeader.IsDeleted)
                .ToListAsync();

            return details
                .GroupBy(d => d.ProductId)
                .ToDictionary(
                    g => g.Key,
                    g => (
                        Input: g.Where(d => !d.FactorHeader.Type).Sum(d => d.Count),
                        Output: g.Where(d => d.FactorHeader.Type).Sum(d => d.Count)
                    ));
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