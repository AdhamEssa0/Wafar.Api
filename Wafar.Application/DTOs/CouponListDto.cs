using System;

namespace Wafar.Application.DTOs
{
    public class CouponListDto
    {
        public int Id { get; set; }
        public string UniqueCode { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public string Status { get; set; } = null!;
        public string? RewardName { get; set; }
        public string? CustomerName { get; set; }

        // بيانات الاستخدام
        public DateTime? UsageDate { get; set; }
        public int? UsedByUserId { get; set; }
        public string? UsedByUserName { get; set; }
        public int? BranchId { get; set; }
    }
}