using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class CouponResulteDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UniqueCode { get; set; }
        public string? RewardName { get; set; }
        public string? RewardType { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
