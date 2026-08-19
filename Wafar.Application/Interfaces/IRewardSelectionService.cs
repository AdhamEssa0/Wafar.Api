using Wafar.Domain.Entities;
using Wafar.Domain.Entities.Rewards;

namespace Wafar.Application.Interfaces
{
    public interface IRewardSelectionService
    {
        Reward SelectRandomReward(ICollection<QRCodeReward> qrRewards);
    }
}