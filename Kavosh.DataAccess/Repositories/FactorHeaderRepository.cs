using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kavosh.DataAccess.Repositories
{
    public interface IFactorHeaderRepository : IRepository<FactorHeader>
    {
        Task<FactorHeader> GetByIdWithDetailsAsync(Guid id);
        Task<long> GetMaxCodeAsync();
        Task<Guid> SaveWithDetailsAsync(FactorHeader header, List<FactorDetail> details, List<HowToPay> howToPays); // 👈 پارامتر جدید
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
                .Include(f => f.HowToPays)              // 👈 جدید
                .ThenInclude(p => p.PaymentType)     // 👈 جدید
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<long> GetMaxCodeAsync()
        {
            // اگه جدول خالی باشه 999 برمی‌گرده تا اولین کد بشه 1000
            //var gg= await _dbSet.MaxAsync(f => (long?)f.Code) ;
            return await _dbSet.MaxAsync(f => (long?)f.Code) ?? 999;
        }

        public async Task<Guid> SaveWithDetailsAsync(FactorHeader header, List<FactorDetail> details, List<HowToPay> howToPays)
        {
            var existing = header.Id != Guid.Empty
                ? await _dbSet
                    .Include(f => f.FactorDetails)
                    .Include(f => f.HowToPays)          // 👈 جدید
                    .FirstOrDefaultAsync(f => f.Id == header.Id)
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

                foreach (var p in howToPays)             // 👈 جدید
                {
                    p.Id = Guid.NewGuid();
                    p.FactorHeaderId = header.Id;
                    p.CreatedAt = DateTime.UtcNow;
                    p.IsDeleted = false;
                }
                header.HowToPays = howToPays;

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

            // Sync خط‌های کالا (بدون تغییر نسبت به قبل)
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

            // 👇 Sync ردیف‌های پرداخت (همون منطق، برای HowToPay)
            var incomingPayIds = howToPays.Where(p => p.Id != Guid.Empty).Select(p => p.Id).ToHashSet();
            var payToRemove = existing.HowToPays.Where(p => !incomingPayIds.Contains(p.Id)).ToList();
            foreach (var p in payToRemove)
            {
                existing.HowToPays.Remove(p);
                _context.Set<HowToPay>().Remove(p);
            }
            foreach (var incoming in howToPays)
            {
                var match = incoming.Id != Guid.Empty
                    ? existing.HowToPays.FirstOrDefault(p => p.Id == incoming.Id)
                    : null;

                if (match is not null)
                {
                    match.PaymentTypeId = incoming.PaymentTypeId;
                    match.Price = incoming.Price;
                    match.CheckNumber = incoming.CheckNumber;
                    match.CheckDate = incoming.CheckDate;
                    match.Settlement = incoming.Settlement;
                    match.Description = incoming.Description;
                    match.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    incoming.Id = Guid.NewGuid();
                    incoming.FactorHeaderId = existing.Id;
                    incoming.CreatedAt = DateTime.UtcNow;
                    incoming.IsDeleted = false;
                    existing.HowToPays.Add(incoming);
                }
            }

            return existing.Id;
        }
    }
}