using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<QRCode> QRCodes { get; set; } = new List<QRCode>();
        public ICollection<CouponUsage> CouponUsages { get; set; } = new List<CouponUsage>();
    }
}
