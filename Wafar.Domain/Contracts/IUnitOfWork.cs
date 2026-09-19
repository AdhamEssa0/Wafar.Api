using Wafar.Domain.Commen;
using Wafar.Domain.Interface;

namespace Wafar.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        ICouponRepository CouponRepository { get; }
    }
}