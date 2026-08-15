using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Rewards;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class QRCodeRewardRepository : GenericRepository<QRCodeReward>, IQRCodeRewardRepository
    {
        public QRCodeRewardRepository(AppDbContext context) : base(context) { }
        public async Task<bool> ExistsAsync(int qrCodeId, int rewardId)
        {
            return await _context.Set<QRCodeReward>()
                .AnyAsync(x => x.QRCodeId == qrCodeId && x.RewardId == rewardId);
        }
    }
}
