using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface ICashDocumentRepository : IRepository<CashDocument>
    {
        Task<long> GetMaxCodeAsync();
        Task<List<CashDocument>> GetAllByTypeAsync(bool type);
        Task<List<CashDocument>> GetByTypeAndSettlementAsync(bool type, bool? isSettled);
    }

    public class CashDocumentRepository : Repository<CashDocument>, ICashDocumentRepository
    {
        public CashDocumentRepository(AppDbContext context) : base(context) { }

        public async Task<long> GetMaxCodeAsync()
        {
            return await _dbSet.MaxAsync(d => (long?)d.Code) ?? 0;
        }

        public async Task<List<CashDocument>> GetAllByTypeAsync(bool type)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => !d.IsDeleted && d.Type == type)
                .OrderByDescending(d => d.Code)
                .ToListAsync();
        }
        public async Task<List<CashDocument>> GetByTypeAndSettlementAsync(bool type, bool? isSettled)
        {
        Reback:
            //var query1 = _dbSet.AsNoTracking().ToList();
            try
            {
                var query = _dbSet.AsNoTracking().Where(d => !d.IsDeleted && d.Type == type);

                if (isSettled.HasValue)
                    query = query.Where(d => d.IsSettled == isSettled.Value);


                return await query.OrderByDescending(d => d.Code).ToListAsync();
            }
            catch (Exception e)
            {
                await Task.Delay(500);
                goto Reback;
            }
        }
    }
}