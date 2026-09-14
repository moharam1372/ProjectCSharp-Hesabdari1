using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IPartnerExpenseRepository : IRepository<PartnerExpense>
    {
        Task<List<PartnerExpense>> GetAllWithDetailsAsync();
    }

    public class PartnerExpenseRepository : Repository<PartnerExpense>, IPartnerExpenseRepository
    {
        public PartnerExpenseRepository(AppDbContext context) : base(context) { }

        public async Task<List<PartnerExpense>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Partner)
                .Include(e => e.ExpenseType)
                .Where(e => !e.IsDeleted)
                .OrderByDescending(e => e.DateCustom)
                .ToListAsync();
        }
    }
}