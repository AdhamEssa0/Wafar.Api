using Wafar.Domain.Commen;

namespace Wafar.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}