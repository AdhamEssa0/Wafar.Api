using Microsoft.EntityFrameworkCore;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Enum;

namespace Wafar.Application.Extensions
{
    public static class RepositoryQueryExtensions
    {
        // بدل IQRCodeRepository.IsActiveAndWithinLimitsAsync
        public static async Task<bool> IsActiveAndWithinLimitsAsync(
            this IGenericRepository<QRCode> repo, string code)
        {
            var qr = await repo.Query().SingleOrDefaultAsync(q => q.Code == code);
            return qr != null && qr.IsActive
                && (qr.ScanLimit == null || qr.CurrentScanCount < qr.ScanLimit)
                && qr.StartDate <= DateTime.UtcNow
                && (qr.EndDate == null || qr.EndDate >= DateTime.UtcNow);
        }

        // بدل IQRCodeRepository.GetByCodeWithRewardsAsync
        public static async Task<QRCode?> GetByCodeWithRewardsAsync(
            this IGenericRepository<QRCode> repo, string code)
        {
            return await repo.Query()
                .Include(q => q.QRCodeRewards)
                    .ThenInclude(qr => qr.Reward)
                .SingleOrDefaultAsync(q => q.Code == code);
        }

        // بدل ICouponRepository.ExistsWithCodeAsync
        public static Task<bool> ExistsWithCodeAsync(
            this IGenericRepository<Coupon> repo, string uniqueCode)
            => repo.AnyAsync(c => c.UniqueCode == uniqueCode);

        public static async Task IncrementScanCountAsync(
            this IGenericRepository<QRCode> repo, int qrCodeId)
                {
                    var qr = await repo.Query().SingleOrDefaultAsync(q => q.Id == qrCodeId);
                    if (qr != null)
                    {
                        qr.CurrentScanCount += 1;
                        repo.Update(qr);
                    }
                }
        public static Task<int> CountByQRCodeAsync(
            this IGenericRepository<ScanHistory> repo, int qrCodeId)
            => repo.Query().CountAsync(s => s.QRCodeId == qrCodeId);

        // بدل ICouponRepository.GetExpiredActiveCouponsAsync
        public static async Task<IReadOnlyList<Coupon>> GetExpiredActiveCouponsAsync(
            this IGenericRepository<Coupon> repo)
                {
                    return await repo.Query()
                        .Where(c => c.Status == CouponStatus.Active
                                 && c.ExpirationDate < DateTime.UtcNow)
                        .ToListAsync();
                }

        public static Task<Coupon?> GetByUniqueCodeAsync(
            this IGenericRepository<Coupon> repo, string uniqueCode)
                {
                    return repo.Query()
                        .Include(c => c.Reward)
                        .Include(c => c.ScanHistory)
                        .SingleOrDefaultAsync(c => c.UniqueCode == uniqueCode);
                }
    }
}