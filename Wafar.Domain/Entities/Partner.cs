using CodeKhasm.Domain.Entities.Coupons;
using CodeKhasm.Domain.Entities.Qr;
using CodeKhasm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public CommissionType CommissionType { get; set; }
        public decimal DefaultCommissionValue { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<QRCode> QRCodes { get; set; } = new List<QRCode>();
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }
}
