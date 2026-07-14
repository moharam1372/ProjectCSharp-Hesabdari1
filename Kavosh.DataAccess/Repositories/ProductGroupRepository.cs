using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IProductGroupRepository : IRepository<ProductGroup>
    {
        Task<ProductGroup> GetByTitleAsync(string title);
        Task<bool> HasProductsAsync(Guid groupId);
    }

    public class ProductGroupRepository : Repository<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(AppDbContext context) : base(context) { }

        public async Task<ProductGroup> GetByTitleAsync(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.Title == title);
        }

        public async Task<bool> HasProductsAsync(Guid groupId)
        {
            return await _context.Set<Product>()
                .AnyAsync(p => p.ProductGroupId == groupId && !p.IsDeleted);
        }
    }
}