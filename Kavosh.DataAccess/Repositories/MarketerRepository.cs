using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IMarketerRepository : IRepository<Marketer>
    {
        Task<Marketer> GetByFullNameAsync(string fullname);
        Task<bool> HasFactorsAsync(Guid marketerId);
    }

    public class MarketerRepository : Repository<Marketer>, IMarketerRepository
    {
        public MarketerRepository(AppDbContext context) : base(context) { }

        public async Task<Marketer> GetByFullNameAsync(string fullname)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.FullName == fullname);
        }

        public async Task<bool> HasFactorsAsync(Guid marketerId)
        {
            return await _context.Set<FactorHeader>()
                .AnyAsync(f => f.MarketerId == marketerId && !f.IsDeleted);
        }
    }
}