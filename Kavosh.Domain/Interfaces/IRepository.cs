using System.Linq.Expressions;

namespace Kavosh.Domain.Interfaces
{
    //public class Repository<T> : Repository<T> where T : BaseEntity, new()
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<bool> Any();
        Task<bool> Any(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<T> First(Expression<Func<T, bool>> expression);




        Task Add(T entity); 
        Task AddRange(IEnumerable<T> entities);

        Task AddOrUpdate(T entity);


        Task Update(T entity);
        Task UpdateRange(List<T> entity);

        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);

        Task<(T item, bool gNew)> GetOrNew(Guid id);
        Task<(T item, bool gNew)> GetOrNew(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> OrderBy(Expression<Func<T, object>> expression);
        Task<IEnumerable<T>> OrderByDesc(Expression<Func<T, object>> expression);
        Task<int> SaveChangesAsync();
    }
}
