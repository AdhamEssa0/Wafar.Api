using CodeKhasm.Domain.Entities.Coupons;
using CodeKhasm.Domain.Entities.Catalog;
using CodeKhasm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities.Rewards
{
    public class Reward
    {
        public int Id { get; set; }
        public RewardType RewardType { get; set; }
        public string RewardName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal ProbabilityPercentage { get; set; } // default probability
        public bool IsActive { get; set; } = true;
        public int ExpirationDays { get; set; } = 7;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RewardCategoryId { get; set; }
        public RewardCategory RewardCategory { get; set; } = null!;

        public int? DeviceId { get; set; }
        public Device? Device { get; set; }

        public int? AccessoryId { get; set; }
        public Accessory? Accessory { get; set; }

        public int? SparePartId { get; set; }
        public SparePart? SparePart { get; set; }

        public int? MaintenanceServiceId { get; set; }
        public MaintenanceService? MaintenanceService { get; set; }

        public ICollection<QRCodeReward> QRCodeRewards { get; set; } = new List<QRCodeReward>();
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }

}
