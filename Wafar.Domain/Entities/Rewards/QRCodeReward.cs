using CodeKhasm.Domain.Entities.Qr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities.Rewards
{
    public class QRCodeReward
    {
        public int Id { get; set; }

        public int QRCodeId { get; set; }
        public QRCode QRCode { get; set; } = null!;

        public int RewardId { get; set; }
        public Reward Reward { get; set; } = null!;

        public decimal? ProbabilityOverride { get; set; }
    }
}
