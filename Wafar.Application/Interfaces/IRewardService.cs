using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface IRewardService
    {
        Task<IReadOnlyList<RewardDto>> GetAllAsync();
        Task<RewardDto?> GetByIdAsync(int id);
        Task<RewardDto> CreateAsync(CreateRewardDto dto);
        Task<bool> UpdateAsync(int id, UpdateRewardDto dto);
        Task<bool> DeleteAsync(int id);
    }
}