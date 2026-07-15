using Kavosh.Domain;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kavosh.DataAccess.Repositories
{

    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // ============= متدهای Any =============
        public async Task<bool> Any()
        {
            return await _dbSet.AnyAsync();
        }

        public async Task<bool> Any(Guid id)
        {
            return await _dbSet.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        // ============= متدهای Get =============
        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _dbSet.Where(w => !w.IsDeleted).ToListAsync();
            }
            catch (Exception e)
            {
                await Task.Delay(1000);
                return await _dbSet.Where(w => !w.IsDeleted).ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> First(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        // ============= متدهای Add =============
        public async Task Add(T entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity.CreatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsDeleted = false;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        // فیلدهایی که فقط موقع «ایجاد» رکورد باید ست بشن و هیچ‌وقت با Update بازنویسی نشن
        private static readonly string[] _preserveOnUpdateFields =
        {
            nameof(BaseEntity.CreatedAt),   // همیشه روی همه‌ی Entity ها هست
            nameof(BaseEntity.IsDeleted),   // نباید با Update وضعیت حذف تغییر کنه
            "CreatedBy",                    // فقط اگه Entity این پراپرتی رو داشته باشه
            "CreatedDate"                   // فقط اگه Entity این پراپرتی رو داشته باشه
        };
        // مقدار فعلی فیلدهای «ایجاد» رو از رکورد موجود روی شیء ورودی کپی می‌کنه
        // تا SetValues اونا رو با مقدار خالی بازنویسی نکنه
        private static void PreserveCreationFields(T existing, T incoming)
        {
            var type = typeof(T);
            foreach (var fieldName in _preserveOnUpdateFields)
            {
                var prop = type.GetProperty(fieldName);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(incoming, prop.GetValue(existing));
                }
            }
        }
        public async Task AddOrUpdate(T entity)
        {
            var existing = entity.Id != Guid.Empty
                ? await _dbSet.FindAsync(entity.Id)
                : null;

            if (existing != null)
            {
                PreserveCreationFields(existing, entity);

                _context.Entry(existing).CurrentValues.SetValues(entity);
                existing.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsDeleted = false;
                await _dbSet.AddAsync(entity);
            }
        }

        // ============= متدهای Update =============
        public async Task Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        // ============= متدهای Remove (Soft Delete) =============
        public async Task Remove(T entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        // ============= متدهای GetOrNew =============
        public async Task<(T item, bool gNew)> GetOrNew(Guid id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item != null)
                return (item, false);

            var newItem = new T
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            return (newItem, true);
        }

        public async Task<(T item, bool gNew)> GetOrNew(Expression<Func<T, bool>> expression)
        {
            var item = await _dbSet.FirstOrDefaultAsync(expression);
            if (item != null)
                return (item, false);

            var newItem = new T
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            return (newItem, true);
        }

        // ============= متدهای OrderBy =============
        public async Task<IEnumerable<T>> OrderBy(Expression<Func<T, object>> expression)
        {
            return await _dbSet.OrderBy(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> OrderByDesc(Expression<Func<T, object>> expression)
        {
            return await _dbSet.OrderByDescending(expression).ToListAsync();
        }

        // ============= SaveChanges =============
        public async Task<int> SaveChangesAsync()
        {
            // به‌روزرسانی خودکار UpdatedAt
            var entries = _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            return await _context.SaveChangesAsync();
        }
    }
}
