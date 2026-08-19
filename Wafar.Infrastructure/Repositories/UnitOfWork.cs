using System.Collections.Concurrent;
using Wafar.Domain.Commen;
using Wafar.Domain.Contracts;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // بيحفظ الريبو بعد أول مرة يتعمل، عشان لو نفس الـ Entity اتطلبت
        // تاني في نفس الـ Request ماتتعملش من جديد كل مرة
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

            var newRepo = new GenericRepository<TEntity>(_context);
            _repositories[type] = newRepo;

            return newRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}