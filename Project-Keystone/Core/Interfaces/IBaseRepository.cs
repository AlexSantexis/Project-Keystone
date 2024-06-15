using System.Linq.Expressions;

namespace Project_Keystone.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {



        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();
    }
}
