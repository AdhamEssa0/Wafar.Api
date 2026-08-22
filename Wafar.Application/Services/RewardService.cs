using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;

namespace Wafar.Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RewardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<RewardDto>> GetAllAsync()
        {
            var repo = _unitOfWork.GetRepository<Reward>();
            var rewards = await repo.GetAllAsync();

            return rewards.Select(ToDto).ToList();
        }

        public async Task<RewardDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Reward>();
            var reward = await repo.GetByIdAsync(id);

            return reward == null ? null : ToDto(reward);
        }

        public async Task<RewardDto> CreateAsync(CreateRewardDto dto)
        {
            var repo = _unitOfWork.GetRepository<Reward>();

            var reward = new Reward
            {
                RewardType = Enum.Parse<RewardType>(dto.RewardType),
                RewardName = dto.RewardName,
                Description = dto.Description,
                DiscountValue = dto.DiscountValue,
                ProbabilityPercentage = dto.ProbabilityPercentage,
                ExpirationDays = dto.ExpirationDays,
                RewardCategoryId = dto.RewardCategoryId,
                IsActive = true
            };

            repo.Add(reward);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(reward);
        }

        public async Task<bool> UpdateAsync(int id, UpdateRewardDto dto)
        {
            var repo = _unitOfWork.GetRepository<Reward>();
            var reward = await repo.GetByIdAsync(id);
            if (reward == null)
                return false;

            reward.RewardType = Enum.Parse<RewardType>(dto.RewardType);
            reward.RewardName = dto.RewardName;
            reward.Description = dto.Description;
            reward.DiscountValue = dto.DiscountValue;
            reward.ProbabilityPercentage = dto.ProbabilityPercentage;
            reward.IsActive = dto.IsActive;
            reward.ExpirationDays = dto.ExpirationDays;
            reward.RewardCategoryId = dto.RewardCategoryId;

            repo.Update(reward);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Reward>();
            var reward = await repo.GetByIdAsync(id);
            if (reward == null)
                return false;

            repo.Remove(reward);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private static RewardDto ToDto(Reward r) => new()
        {
            Id = r.Id,
            RewardType = r.RewardType.ToString(),
            RewardName = r.RewardName,
            Description = r.Description,
            DiscountValue = r.DiscountValue,
            ProbabilityPercentage = r.ProbabilityPercentage,
            IsActive = r.IsActive,
            ExpirationDays = r.ExpirationDays,
            RewardCategoryId = r.RewardCategoryId
        };
    }
}