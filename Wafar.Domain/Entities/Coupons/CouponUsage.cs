using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Coupons
{
    public class CouponUsage
    {
        public int Id { get; set; }
        public DateTime UsageDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string? ProductName { get; set; }

        public int CouponId { get; set; } // 1:1 FK (unique)
        public Coupon Coupon { get; set; } = null!;

        public int UsedByUserId { get; set; }
        public User UsedByUser { get; set; } = null!;

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
