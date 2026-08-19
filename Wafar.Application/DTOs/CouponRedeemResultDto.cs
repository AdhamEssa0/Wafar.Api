using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class CouponRedeemResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? RewardName { get; set; }
        public decimal? CommissionAmount { get; set; }
    }
}
