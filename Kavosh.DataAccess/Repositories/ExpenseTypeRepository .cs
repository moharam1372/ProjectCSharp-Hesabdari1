using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IExpenseTypeRepository : IRepository<ExpenseType>
    {
        Task<ExpenseType> GetByTitleAsync(string title);
        Task<bool> HasExpensesAsync(Guid expenseTypeId);
    }

    public class ExpenseTypeRepository : Repository<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(AppDbContext context) : base(context) { }

        public async Task<ExpenseType> GetByTitleAsync(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Title == title);
        }

        public async Task<bool> HasExpensesAsync(Guid expenseTypeId)
        {
            return await _context.Set<PartnerExpense>()
                .AnyAsync(e => e.ExpenseTypeId == expenseTypeId && !e.IsDeleted);
        }
    }
}