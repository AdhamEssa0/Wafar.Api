using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wafar.Domain.Commen;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<TEntity?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.AnyAsync(predicate);

        // بيسمحلك تعمل استعلامات مخصصة في الريبوهات المتخصصة من غير ما تكرر الكود ده
        public IQueryable<TEntity> Query()
            => _dbSet.AsQueryable();

        public void Add(TEntity entity)
            => _dbSet.Add(entity);

        public void AddRange(IEnumerable<TEntity> entities)
            => _dbSet.AddRange(entities);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Remove(TEntity entity)
            => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities)
            => _dbSet.RemoveRange(entities);
    }
}