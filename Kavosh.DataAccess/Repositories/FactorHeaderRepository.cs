using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IFactorHeaderRepository : IRepository<FactorHeader>
    {
        Task<FactorHeader> GetByIdWithDetailsAsync(Guid id);
        Task<long> GetMaxCodeAsync();
        Task<Guid> SaveWithDetailsAsync(FactorHeader header, List<FactorDetail> details);
    }

    public class FactorHeaderRepository : Repository<FactorHeader>, IFactorHeaderRepository
    {
        public FactorHeaderRepository(AppDbContext context) : base(context) { }

        public async Task<FactorHeader> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(f => f.Person)
                .Include(f => f.FactorDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<long> GetMaxCodeAsync()
        {
            // اگه جدول خالی باشه 999 برمی‌گرده تا اولین کد بشه 1000
            //var gg= await _dbSet.MaxAsync(f => (long?)f.Code) ;
            return await _dbSet.MaxAsync(f => (long?)f.Code) ?? 999;
        }

        public async Task<Guid> SaveWithDetailsAsync(FactorHeader header, List<FactorDetail> details)
        {
            var existing = header.Id != Guid.Empty
                ? await _dbSet.Include(f => f.FactorDetails).FirstOrDefaultAsync(f => f.Id == header.Id)
                : null;

            // ============= فاکتور جدید =============
            if (existing is null)
            {
                header.Id = header.Id != Guid.Empty ? header.Id : Guid.NewGuid();
                header.CreatedAt = DateTime.UtcNow;
                header.IsDeleted = false;

                foreach (var d in details)
                {
                    d.Id = Guid.NewGuid();
                    d.FactorHeaderId = header.Id;
                    d.CreatedAt = DateTime.UtcNow;
                    d.IsDeleted = false;
                }

                header.FactorDetails = details;
                await _dbSet.AddAsync(header);

                return header.Id;
            }

            // ============= ویرایش فاکتور موجود =============
            existing.Code = header.Code;
            existing.PersonId = header.PersonId;
            existing.Type = header.Type;
            existing.DateFactor = header.DateFactor;
            existing.Discount = header.Discount;
            existing.PriceTotal = header.PriceTotal;
            existing.UpdatedAt = DateTime.UtcNow;

            // Sync خط‌های کالا: حذف‌شده‌ها Hard Delete، موجودها آپدیت، جدیدها اضافه
            var incomingIds = details.Where(d => d.Id != Guid.Empty).Select(d => d.Id).ToHashSet();
            var toRemove = existing.FactorDetails.Where(d => !incomingIds.Contains(d.Id)).ToList();

            foreach (var d in toRemove)
            {
                existing.FactorDetails.Remove(d);
                _context.Set<FactorDetail>().Remove(d);
            }

            foreach (var incoming in details)
            {
                var match = incoming.Id != Guid.Empty
                    ? existing.FactorDetails.FirstOrDefault(d => d.Id == incoming.Id)
                    : null;

                if (match is not null)
                {
                    match.ProductId = incoming.ProductId;
                    match.Count = incoming.Count;
                    match.PriceUnit = incoming.PriceUnit;
                    match.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    incoming.Id = Guid.NewGuid();
                    incoming.FactorHeaderId = existing.Id;
                    incoming.CreatedAt = DateTime.UtcNow;
                    incoming.IsDeleted = false;
                    existing.FactorDetails.Add(incoming);
                }
            }

            return existing.Id;
        }
    }
}