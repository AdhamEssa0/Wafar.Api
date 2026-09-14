using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResulteDto> ProcessScanAsync(
            string qrCode, int? customerId, string? ipAddress, string? deviceInfo, int? categoryId);

        Task<CouponRedeemResultDto> RedeemCouponAsync(
            string uniqueCode, int usedByUserId, int? branchId);

        Task<List<RewardCategoryOptionDto>> GetAvailableCategoriesAsync(string qrCode);
    }
}