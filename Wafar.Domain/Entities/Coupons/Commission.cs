using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Coupons
{
    public class Commission
    {
        public int Id { get; set; }
        public CommissionType CommissionType { get; set; }
        public decimal CommissionValue { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CommissionStatus Status { get; set; } = CommissionStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidDate { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;

        public int QRCodeId { get; set; }
        public QRCode QRCode { get; set; } = null!;

        public int CouponId { get; set; } // 1:1 FK (unique)
        public Coupon Coupon { get; set; } = null!;
    }
}
