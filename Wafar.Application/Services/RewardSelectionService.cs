using Wafar.Application.Interfaces;
using Wafar.Domain.Entities.Rewards;

namespace Wafar.Application.Services
{
    public class RewardSelectionService : IRewardSelectionService
    {
        private static readonly Random _random = new();

        public Reward SelectRandomReward(ICollection<QRCodeReward> qrRewards)
        {
            var activeLinks = qrRewards.Where(l => l.Reward.IsActive).ToList();

            if (activeLinks.Count == 0)
                throw new InvalidOperationException("لا توجد مكافآت فعّالة متاحة لهذا الكود.");

            var weights = activeLinks
                .Select(l => l.ProbabilityOverride ?? l.Reward.ProbabilityPercentage)
                .ToList();

            var totalWeight = weights.Sum();
            if (totalWeight <= 0)
                throw new InvalidOperationException("مجموع نسب الاحتمالية غير صالح.");

            var roll = (decimal)_random.NextDouble() * totalWeight;
            decimal cumulative = 0;

            for (var i = 0; i < activeLinks.Count; i++)
            {
                cumulative += weights[i];
                if (roll <= cumulative)
                    return activeLinks[i].Reward;
            }

            return activeLinks[^1].Reward;
        }
    }
}