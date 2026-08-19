using Wafar.Domain.Enum;
using Wafar.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Application.Extensions;

namespace Wafar.Application.Services
{
    public class CouponExpiryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponExpiryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ExpireOverdueCouponsAsync()
        {
            var expiredCoupons = await _unitOfWork.GetRepository<Coupon>().GetExpiredActiveCouponsAsync();

            var count = 0;
            foreach (var coupon in expiredCoupons)
            {
                coupon.Status = CouponStatus.Expired;
                _unitOfWork.GetRepository<Coupon>().Update(coupon);
                count++;
            }

            if (count > 0)
                await _unitOfWork.SaveChangesAsync();

            return count;
        }
    }
}
