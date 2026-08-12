using CodeKhasm.Domain.Entities.Qr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities.Coupons
{
    public class ScanHistory
    {
        public int Id { get; set; }
        public DateTime ScanDate { get; set; } = DateTime.UtcNow;
        public string? IPAddress { get; set; }
        public string? DeviceInfo { get; set; }

        public int QRCodeId { get; set; }
        public QRCode QRCode { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // One-to-One with Coupon
        public Coupon? Coupon { get; set; }
    }

}
