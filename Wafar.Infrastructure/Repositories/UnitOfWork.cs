using System.Collections.Concurrent;
using Wafar.Domain.Commen;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Interface;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var type = typeof(TEntity);

            if (_repositories.TryGetValue(type, out var existingRepo))
                return (IGenericRepository<TEntity>)existingRepo;

            object newRepo;

            if (type == typeof(Coupon))
            {
                newRepo = new CouponRepository(_context);
            }
            else
            {
                newRepo = new GenericRepository<TEntity>(_context);
            }

            _repositories[type] = newRepo;

            return (IGenericRepository<TEntity>)newRepo;
        }

        public ICouponRepository CouponRepository
            => (ICouponRepository)GetRepository<Coupon>();

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}