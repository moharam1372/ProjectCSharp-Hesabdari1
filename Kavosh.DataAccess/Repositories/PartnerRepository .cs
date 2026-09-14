using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<Partner> GetByFullNameAsync(string fullName);
        Task<bool> HasExpensesAsync(Guid partnerId);
    }

    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AppDbContext context) : base(context) { }

        public async Task<Partner> GetByFullNameAsync(string fullName)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.FullName == fullName);
        }

        public async Task<bool> HasExpensesAsync(Guid partnerId)
        {
            return await _context.Set<PartnerExpense>()
                .AnyAsync(e => e.PartnerId == partnerId && !e.IsDeleted);
        }
    }
}