using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetOrCreateByPhoneAsync(GetOrCreateCustomerDto dto);
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<CouponSummaryDto>> GetCouponsAsync(int customerId);
    }
    public class CouponSummaryDto
    {
        public string UniqueCode { get; set; } = null!;
        public string RewardName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
