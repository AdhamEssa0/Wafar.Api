using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface IQRCodeService
    {
        Task<IReadOnlyList<QRCodeDto>> GetAllAsync();
        Task<QRCodeDto?> GetByIdAsync(int id);
        Task<QRCodeDto> CreateAsync(CreateQRCodeDto dto);
        Task<bool> UpdateAsync(int id, UpdateQRCodeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);

        // إدارة المكافآت المرتبطة بالـ QR
        Task<IReadOnlyList<QRCodeRewardDto>> GetRewardsAsync(int qrCodeId);

        Task<bool> UpdateRewardsAsync(
            int qrCodeId,
            IReadOnlyList<QRCodeRewardDto> rewards);
    }
}
