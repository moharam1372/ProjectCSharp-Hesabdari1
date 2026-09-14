using Kavosh.Domain.Entities;
using Kavosh.Domain.Enums;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IChequeRepository : IRepository<Cheque>
    {
        Task<List<Cheque>> GetAllWithPersonAsync();
        Task<List<Cheque>> GetUpcomingAsync(int days);
        Task<Cheque> GetByHowToPayIdAsync(Guid howToPayId);
        Task<(long TotalAmount, int Count)> GetPendingSummaryAsync();
        Task<Cheque> GetByDefinitiveAccountIdAsync(Guid definitiveAccountId);   // 👈 جدید


    }

    public class ChequeRepository : Repository<Cheque>, IChequeRepository
    {
        public ChequeRepository(AppDbContext context) : base(context) { }

        // 👇 جدید — برای چک‌های ثبت‌شده به‌صورت دستی (بدون فاکتور)
        public async Task<Cheque> GetByDefinitiveAccountIdAsync(Guid definitiveAccountId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.DefinitiveAccountId == definitiveAccountId && !c.IsDeleted);
        }

        public async Task<(long TotalAmount, int Count)> GetPendingSummaryAsync()
        {
            var pending = await _dbSet
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.Status == ChequeStatus.Pending)
                .ToListAsync();

            return (pending.Sum(c => c.Price), pending.Count);
        }

        public async Task<List<Cheque>> GetAllWithPersonAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Person)
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.DueDate)
                .ToListAsync();
        }

        public async Task<List<Cheque>> GetUpcomingAsync(int days)
        {
            var today = DateTime.Today;
            var to = today.AddDays(days);

            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Person)
                .Where(c => !c.IsDeleted
                            && c.Status == ChequeStatus.Pending
                            && c.DueDate.Date >= today
                            && c.DueDate.Date <= to)
                .OrderBy(c => c.DueDate)
                .ToListAsync();
        }

        public async Task<Cheque> GetByHowToPayIdAsync(Guid howToPayId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.HowToPayId == howToPayId && !c.IsDeleted);
        }
    }
}