using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IProductUnitRepository : IRepository<ProductUnit>
    {
        Task<ProductUnit> GetByTitleAsync(string title);
        Task<bool> HasProductsAsync(Guid unitId);
    }

    public class ProductUnitRepository : Repository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(AppDbContext context) : base(context) { }

        public async Task<ProductUnit> GetByTitleAsync(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Title == title);
        }

        public async Task<bool> HasProductsAsync(Guid unitId)
        {
            return await _context.Set<Product>()
                .AnyAsync(p => p.ProductUnitId == unitId && !p.IsDeleted);
        }
    }
}