using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Rewards;

namespace Wafar.Domain.Contracts
{
    public interface IQRCodeRewardRepository : IGenericRepository<QRCodeReward>
    {
        Task<bool> ExistsAsync(int qrCodeId, int rewardId);
    }
}
