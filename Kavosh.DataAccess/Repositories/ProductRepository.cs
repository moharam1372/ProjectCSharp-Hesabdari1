using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithDetailsAsync();
        Task<Product> GetByIdWithDetailsAsync(Guid id);
        Task<Product> GetByCodeAsync(long code);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

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