using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Rewards;

namespace Wafar.Domain.Contracts
{
    public interface IRewardRepository : IGenericRepository<Reward>
    {
        Task<IEnumerable<QRCodeReward>> GetActiveRewardsForQRAsync(int qrCodeId);
    }
}
