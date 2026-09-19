using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Wafar.Domain.Interface;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(AppDbContext context) : base(context) { }

        public async Task<Coupon?> GetByUniqueCodeAsync(string uniqueCode)
            => await Query()
                .SingleOrDefaultAsync(c => c.UniqueCode == uniqueCode);

        public async Task<bool> ExistsWithCodeAsync(string uniqueCode)
            => await AnyAsync(c => c.UniqueCode == uniqueCode);

        public async Task<IReadOnlyList<Coupon>> GetExpiredActiveCouponsAsync()
            => await FindAsync(c =>
                c.Status == CouponStatus.Active &&
                c.ExpirationDate < DateTime.UtcNow);

        public async Task<IReadOnlyList<Coupon>> GetByCustomerAsync(int customerId)
            => await FindAsync(c => c.CustomerId == customerId);

        public async Task<IReadOnlyList<Coupon>> GetAllWithDetailsAsync()
            => await Query()
                .Include(c => c.Reward)
                .Include(c => c.Customer)
                .Include(c => c.ScanHistory)
                    .ThenInclude(s => s.QRCode)
                .Include(c => c.CouponUsage)
                .OrderByDescending(c => c.IssueDate)
                .ToListAsync();
    }
}