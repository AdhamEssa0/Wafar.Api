using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class RewardDto
    {
        public int Id { get; set; }
        public string RewardType { get; set; } = null!;
        public string RewardName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal ProbabilityPercentage { get; set; }
        public bool IsActive { get; set; }
        public int ExpirationDays { get; set; }
        public int RewardCategoryId { get; set; }
    }
    // بيتبعت من الأدمن وقت إضافة مكافأة جديدة
    public class CreateRewardDto
    {
        public string RewardType { get; set; } = null!;
        public string RewardName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal ProbabilityPercentage { get; set; }
        public int ExpirationDays { get; set; } = 7;
        public int RewardCategoryId { get; set; }
    }

    // بيتبعت من الأدمن وقت التعديل
    public class UpdateRewardDto
    {
        public string RewardType { get; set; } = null!;
        public string RewardName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal ProbabilityPercentage { get; set; }
        public bool IsActive { get; set; }
        public int ExpirationDays { get; set; }
        public int RewardCategoryId { get; set; }
    }
}
