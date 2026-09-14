using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface ISettlementRecordRepository : IRepository<SettlementRecord>
    {
        Task<long> GetMaxCodeAsync();
        Task<List<SettlementRecord>> GetAllOrderedAsync();
    }

    public class SettlementRecordRepository : Repository<SettlementRecord>, ISettlementRecordRepository
    {
        public SettlementRecordRepository(AppDbContext context) : base(context) { }

        public async Task<long> GetMaxCodeAsync()
        {
            return await _dbSet.MaxAsync(d => (long?)d.Code) ?? 0;
        }

        public async Task<List<SettlementRecord>> GetAllOrderedAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .OrderByDescending(d => d.Code)
                .ToListAsync();
        }
    }
}