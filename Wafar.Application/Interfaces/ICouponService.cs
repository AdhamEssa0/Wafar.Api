using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResulteDto> ProcessScanAsync(
            string qrCode,
            int? customerId,
            string? ipAddress,
            string? deviceInfo,
            int? categoryId,
            string? extraData);

        Task<CouponRedeemResultDto> RedeemCouponAsync(
            string uniqueCode,
            int usedByUserId,
            int? branchId,
            decimal invoiceAmount,
            string? productName);

        Task<List<RewardCategoryOptionDto>> GetAvailableCategoriesAsync(
            string qrCode);

        Task<IReadOnlyList<CouponListDto>> GetAllWithDetailsAsync();
    }
}