using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IDefinitiveAccountRepository : IRepository<DefinitiveAccount>
    {
        Task<List<DefinitiveAccount>> GetByPersonAsync(Guid personId);
        Task<long> GetMaxCodeAsync();
        Task<DefinitiveAccount> GetDebtByHowToPayIdAsync(Guid howToPayId);
        Task<bool> IsAlreadySettledAsync(Guid definitiveAccountId);


    }

    public class DefinitiveAccountRepository : Repository<DefinitiveAccount>, IDefinitiveAccountRepository
    {
        public DefinitiveAccountRepository(AppDbContext context) : base(context) { }

        public async Task<List<DefinitiveAccount>> GetByPersonAsync(Guid personId)
        {
            return await _dbSet
                .Include(d => d.Person)  
                .Where(d => d.PersonId == personId && !d.IsDeleted)
                .OrderBy(d => d.DateCustom)
                .ToListAsync();
        }

        public async Task<long> GetMaxCodeAsync()
        {
            return await _dbSet.MaxAsync(d => (long?)d.Code) ?? 0;
        }

        public async Task<DefinitiveAccount> GetDebtByHowToPayIdAsync(Guid howToPayId)
        {
            return await _dbSet.FirstOrDefaultAsync(d =>
                d.HowToPayId == howToPayId && d.IsCheck && d.Debtor);
        }

        public async Task<bool> IsAlreadySettledAsync(Guid definitiveAccountId)
        {
            return await _dbSet.AnyAsync(d => d.SettledFromId == definitiveAccountId);
        }

   
    }
}