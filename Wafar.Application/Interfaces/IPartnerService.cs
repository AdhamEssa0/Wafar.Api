using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface IPartnerService
    {
        Task<IReadOnlyList<PartnerDto>> GetAllAsync();
        Task<PartnerDto?> GetByIdAsync(int id);
        Task<PartnerDto> CreateAsync(CreatePartnerDto dto);
        Task<bool> UpdateAsync(int id, UpdatePartnerDto dto);
        Task<bool> DeleteAsync(int id);
        Task<PartnerCommissionReportDto?> GetCommissionsAsync(int partnerId);
    }
}