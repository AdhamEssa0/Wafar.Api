using Wafar.Application.DTOs;
using Wafar.Application.Extensions;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;

namespace Wafar.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRewardSelectionService _rewardSelectionService;

        // الحروف المسموح بيها في كود الكوبون — مستبعدين منها المتشابهة بصريًا (O مع 0، I مع 1)
        private const string CodeChars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private static readonly Random _random = new();

        public CouponService(
            IUnitOfWork unitOfWork,
            IRewardSelectionService rewardSelectionService)
        {
            _unitOfWork = unitOfWork;
            _rewardSelectionService = rewardSelectionService;
        }

        // ============ عرض التصنيفات المتاحة لكود QR معين ============
        public async Task<List<RewardCategoryOptionDto>> GetAvailableCategoriesAsync(string qrCode)
        {
            var qrRepo = _unitOfWork.GetRepository<QRCode>();
            return await qrRepo.GetAvailableCategoriesAsync(qrCode);
        }

        // ============ معالجة عملية المسح واختيار المكافأة ============
        public async Task<CouponResulteDto> ProcessScanAsync(
            string qrCode, int? customerId, string? ipAddress, string? deviceInfo, int? categoryId)
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

            // فلترة حسب التصنيف اللي العميل اختاره في صفحة الاختيار (لو موجود)
            var pool = categoryId.HasValue
                ? qr.QRCodeRewards.Where(l => l.Reward.RewardCategoryId == categoryId.Value).ToList()
                : qr.QRCodeRewards;

            if (pool.Count == 0)
            {
                return new CouponResulteDto
                {
                    Success = false,
                    Message = "لا توجد مكافآت متاحة في هذا التصنيف حاليًا."
                };
            }

            Reward selectedReward;
            try
            {
                selectedReward = _rewardSelectionService.SelectRandomReward(pool);
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

            // 2) ولّد كود فريد — بيعيد المحاولة لو الكود موجود بالفعل
            string uniqueCode;
            do
            {
                uniqueCode = GenerateCouponCode();
            }
            while (await couponRepo.ExistsWithCodeAsync(uniqueCode));

            // 3) اربط الكوبون بالـ ScanHistory عن طريق الـ Navigation Property
            //    مش عن طريق الـ Id مباشرة — عشان EF Core يظبط الترتيب لوحده وقت الحفظ
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

        // ============ تفعيل الكوبون في المحل (الموظف) ============
        public async Task<CouponRedeemResultDto> RedeemCouponAsync(
            string uniqueCode, int usedByUserId, int? branchId)
        {
            var couponRepo = _unitOfWork.GetRepository<Coupon>();

            var coupon = await couponRepo.GetByUniqueCodeAsync(uniqueCode);
            if (coupon == null)
            {
                return new CouponRedeemResultDto
                {
                    Success = false,
                    Message = "الكود ده غير موجود."
                };
            }

            if (coupon.IsUsed || coupon.Status == CouponStatus.Used)
            {
                return new CouponRedeemResultDto
                {
                    Success = false,
                    Message = "الكود ده مستخدم بالفعل."
                };
            }

            if (coupon.ExpirationDate < DateTime.UtcNow)
            {
                coupon.Status = CouponStatus.Expired;
                couponRepo.Update(coupon);
                await _unitOfWork.SaveChangesAsync();

                return new CouponRedeemResultDto
                {
                    Success = false,
                    Message = "الكود ده منتهي الصلاحية."
                };
            }

            // 1) سجّل عملية الاستخدام
            var usageRepo = _unitOfWork.GetRepository<CouponUsage>();
            var usage = new CouponUsage
            {
                CouponId = coupon.Id,
                UsedByUserId = usedByUserId,
                BranchId = branchId,
                UsageDate = DateTime.UtcNow
            };
            usageRepo.Add(usage);

            // 2) علّم الكوبون كمستخدم
            coupon.IsUsed = true;
            coupon.Status = CouponStatus.Used;
            couponRepo.Update(coupon);

            // 3) لو الـ QR ده مرتبط بشريك، احسب واحفظ العمولة
            decimal? commissionAmount = null;

            var qrRepo = _unitOfWork.GetRepository<QRCode>();
            var qr = await qrRepo.GetByIdAsync(coupon.ScanHistory.QRCodeId);

            if (qr?.PartnerId != null && qr.CommissionType != null && qr.CommissionValue != null)
            {
                commissionAmount = qr.CommissionType == CommissionType.Percentage
                    ? (coupon.Reward.DiscountValue ?? 0) * qr.CommissionValue.Value / 100
                    : qr.CommissionValue.Value;

                var commissionRepo = _unitOfWork.GetRepository<Commission>();
                commissionRepo.Add(new Commission
                {
                    CommissionType = qr.CommissionType.Value,
                    CommissionValue = qr.CommissionValue.Value,
                    CalculatedAmount = commissionAmount.Value,
                    Status = CommissionStatus.Pending,
                    PartnerId = qr.PartnerId.Value,
                    QRCodeId = qr.Id,
                    CouponId = coupon.Id
                });
            }

            await _unitOfWork.SaveChangesAsync();

            return new CouponRedeemResultDto
            {
                Success = true,
                Message = "تم تفعيل الكوبون بنجاح.",
                RewardName = coupon.Reward.RewardName,
                CommissionAmount = commissionAmount
            };
        }

        private static string GenerateCouponCode()
        {
            var chars = new char[8];
            for (var i = 0; i < 8; i++)
                chars[i] = CodeChars[_random.Next(CodeChars.Length)];

            return new string(chars);
        }

        public async Task<IReadOnlyList<CouponListDto>> GetAllWithDetailsAsync()
        {
            var couponRepo = _unitOfWork.CouponRepository;

            var coupons = await couponRepo.GetAllWithDetailsAsync();

            return coupons.Select(c => new CouponListDto
            {
                Id = c.Id,
                UniqueCode = c.UniqueCode,
                IssueDate = c.IssueDate,
                ExpirationDate = c.ExpirationDate,
                IsUsed = c.IsUsed,
                Status = c.Status.ToString(),
                RewardName = c.Reward?.RewardName,
                CustomerName = c.Customer?.FullName,
                UsageDate = c.CouponUsage?.UsageDate,
                UsedByUserId = c.CouponUsage?.UsedByUserId,
                UsedByUserName = c.CouponUsage?.UsedByUser?.FullName,
                BranchId = c.CouponUsage?.BranchId
            }).ToList();
        }
    }
}