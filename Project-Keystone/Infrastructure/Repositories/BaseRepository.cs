using Microsoft.EntityFrameworkCore;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ProjectKeystoneDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ProjectKeystoneDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existing = await _dbSet.FindAsync(id);
            if(existing is not null)
            {
                _dbSet.Remove(existing);
                return true;
            }
            return false;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {

            var entity = await _dbSet.FindAsync(id);
            return entity;
            
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
