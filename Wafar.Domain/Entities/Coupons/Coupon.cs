using CodeKhasm.Domain.Entities.Rewards;
using CodeKhasm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities.Coupons
{
    public class Coupon
    {
        public int Id { get; set; }
        public string UniqueCode { get; set; } = null!;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public CouponStatus Status { get; set; } = CouponStatus.Active;

        public int RewardId { get; set; }
        public Reward Reward { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ScanHistoryId { get; set; } // 1:1 FK (unique)
        public ScanHistory ScanHistory { get; set; } = null!;

        public CouponUsage? CouponUsage { get; set; }
        public Commission? Commission { get; set; }
    }

}
