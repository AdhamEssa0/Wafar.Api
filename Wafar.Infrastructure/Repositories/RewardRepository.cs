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
    public class RewardRepository  : GenericRepository<Reward> , IRewardRepository
    {
        public RewardRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<QRCodeReward>> GetActiveRewardsForQRAsync(int qrCodeId)
        {
            return await _context.QRCodeRewards
               .Include(qr => qr.Reward)
               .Where(qr => qr.QRCodeId == qrCodeId && qr.Reward.IsActive)
               .ToListAsync();
        }
    }
}
