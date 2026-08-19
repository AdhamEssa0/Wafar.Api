using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Application.DTOs;
using Wafar.Domain.Entities.Coupons;

namespace Wafar.Application.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResulteDto> ProcessScanAsync(string qrCode, int? customerId, string? ipAddress, string? deviceInfo);
        Task<CouponRedeemResultDto> RedeemCouponAsync(string uniqueCode, int usedByUserId, int? branchId);
    }
}
