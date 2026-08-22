using Microsoft.EntityFrameworkCore;
using Wafar.Application.DTOs;
using Wafar.Application.Extensions;
using Wafar.Application.Interfaces;
using Wafar.Domain.Commen;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;
using Wafar.Domain.Interface;

namespace Wafar.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRewardSelectionService _rewardSelectionService;

        private const string CodeChars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private static readonly Random _random = new();

        public CouponService(
            IUnitOfWork unitOfWork,
            IRewardSelectionService rewardSelectionService)
        {
            _unitOfWork = unitOfWork;
            _rewardSelectionService = rewardSelectionService;
        }

        public async Task<CouponResulteDto> ProcessScanAsync(
            string qrCode, int? customerId, string? ipAddress, string? deviceInfo)
        {
            var qrRepo = _unitOfWork.GetRepository<QRCode>();

            var isValid = await qrRepo.IsActiveAndWithinLimitsAsync(qrCode);
            if (!isValid)
            {
                return new CouponResulteDto
                {
                    Success = false,
                    Message = "الكود ده منتهي أو وصل للحد الأقصى من الاستخدام."
                };
            }

            var qr = await qrRepo.GetByCodeWithRewardsAsync(qrCode);
            if (qr == null || qr.QRCodeRewards.Count == 0)
            {
                return new CouponResulteDto
                {
                    Success = false,
                    Message = "لا توجد مكافآت متاحة لهذا الكود حاليًا."
                };
            }

            Reward selectedReward;
            try
            {
                selectedReward = _rewardSelectionService.SelectRandomReward(qr.QRCodeRewards);
            }
            catch (InvalidOperationException ex)
            {
                return new CouponResulteDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            var couponRepo = _unitOfWork.GetRepository<Coupon>();
            var scanHistoryRepo = _unitOfWork.GetRepository<ScanHistory>();

            // 1) سجل الـ ScanHistory الأول — العلاقة بينه وبين الكوبون 1:1 إجبارية
            var scanHistory = new ScanHistory
            {
                QRCodeId = qr.Id,
                CustomerId = customerId,
                IPAddress = ipAddress,
                DeviceInfo = deviceInfo,
                ScanDate = DateTime.UtcNow
            };
            scanHistoryRepo.Add(scanHistory);

            // 2) ولّد كود فريد
            string uniqueCode;
            do
            {
                uniqueCode = GenerateCouponCode();
            }
            while (await couponRepo.ExistsWithCodeAsync(uniqueCode));

            var coupon = new Coupon
            {
                UniqueCode = uniqueCode,
                Reward = selectedReward,
                CustomerId = customerId,
                Status = CouponStatus.Active,
                ExpirationDate = DateTime.UtcNow.AddDays(selectedReward.ExpirationDays),
                ScanHistory = scanHistory
            };
            couponRepo.Add(coupon);

            await qrRepo.IncrementScanCountAsync(qr.Id);

            await _unitOfWork.SaveChangesAsync();

            return new CouponResulteDto
            {
                Success = true,
                UniqueCode = coupon.UniqueCode,
                RewardName = selectedReward.RewardName,
                RewardType = selectedReward.RewardType.ToString(),
                ExpirationDate = coupon.ExpirationDate
            };
        }
        private static string GenerateCouponCode()
        {
            var chars = new char[8];
            for (var i = 0; i < 8; i++)
                chars[i] = CodeChars[_random.Next(CodeChars.Length)];

            return new string(chars);
        }

        public Task<CouponRedeemResultDto> RedeemCouponAsync(string uniqueCode, int usedByUserId, int? branchId)
        {
            throw new NotImplementedException();
        }
    }
}