using CodeKhasm.Domain.Entities.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ScanHistory> ScanHistories { get; set; } = new List<ScanHistory>();
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}
