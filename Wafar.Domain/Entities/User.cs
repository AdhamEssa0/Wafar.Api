using CodeKhasm.Domain.Entities.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<CouponUsage> CouponUsages { get; set; } = new List<CouponUsage>();
    }
}
