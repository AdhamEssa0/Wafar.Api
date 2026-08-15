using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Qr
{
    public class QRCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int ScanLimit { get; set; } // 0 = unlimited
        public int CurrentScanCount { get; set; }
        public CommissionType? CommissionType { get; set; }
        public decimal? CommissionValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int QRCategoryId { get; set; }
        public QRCategory QRCategory { get; set; } = null!;

        public int? PartnerId { get; set; }
        public Partner? Partner { get; set; }

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<QRCodeReward> QRCodeRewards { get; set; } = new List<QRCodeReward>();
        public ICollection<ScanHistory> ScanHistories { get; set; } = new List<ScanHistory>();
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }
}
