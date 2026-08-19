using Wafar.Application.DTOs;
using Wafar.Application.Extensions;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;
using Wafar.Domain.Interface;

namespace Wafar.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime? from, DateTime? to)
        {
            var scansQuery = _unitOfWork.GetRepository<ScanHistory>().Query();
            var couponsQuery = _unitOfWork.GetRepository<Coupon>().Query();
            var commissionsQuery = _unitOfWork.GetRepository<Commission>().Query();

            if (from.HasValue)
            {
                scansQuery = scansQuery.Where(s => s.ScanDate >= from.Value);
                couponsQuery = couponsQuery.Where(c => c.IssueDate >= from.Value);
                commissionsQuery = commissionsQuery.Where(c => c.CreatedAt >= from.Value);
            }
            if (to.HasValue)
            {
                scansQuery = scansQuery.Where(s => s.ScanDate <= to.Value);
                couponsQuery = couponsQuery.Where(c => c.IssueDate <= to.Value);
                commissionsQuery = commissionsQuery.Where(c => c.CreatedAt <= to.Value);
            }

            return new DashboardSummaryDto
            {
                TotalScans = scansQuery.Count(),
                TotalCouponsIssued = couponsQuery.Count(),
                TotalCouponsUsed = couponsQuery.Count(c => c.Status == CouponStatus.Used),
                TotalCouponsExpired = couponsQuery.Count(c => c.Status == CouponStatus.Expired),
                TotalPendingCommissions = commissionsQuery
                    .Where(c => c.Status == CommissionStatus.Pending)
                    .Sum(c => (decimal?)c.CalculatedAmount) ?? 0,
                TotalPaidCommissions = commissionsQuery
                    .Where(c => c.Status == CommissionStatus.Paid)
                    .Sum(c => (decimal?)c.CalculatedAmount) ?? 0
            };
        }

        public async Task<IEnumerable<QRCodePerformanceDto>> GetQRCodePerformanceAsync(DateTime? from, DateTime? to)
        {
            var qrCodes = await _unitOfWork.GetRepository<QRCode>().GetAllAsync();
            var result = new List<QRCodePerformanceDto>();

            foreach (var qr in qrCodes)
            {
                var scanCount = await _unitOfWork.GetRepository<ScanHistory>().CountByQRCodeAsync(qr.Id);

                var couponsForQr = _unitOfWork.GetRepository<Coupon>().Query()
                    .Where(c => c.ScanHistory.QRCodeId == qr.Id);

                result.Add(new QRCodePerformanceDto
                {
                    QRCodeId = qr.Id,
                    QRCodeName = qr.Name,
                    ScanCount = scanCount,
                    CouponsIssued = couponsForQr.Count(),
                    CouponsUsed = couponsForQr.Count(c => c.Status == CouponStatus.Used)
                });
            }

            return result;
        }

        public async Task<IEnumerable<RewardDistributionDto>> GetRewardDistributionAsync(DateTime? from, DateTime? to)
        {
            var rewards = await _unitOfWork.GetRepository<Reward>().GetAllAsync();
            var result = new List<RewardDistributionDto>();

            foreach (var reward in rewards)
            {
                var couponsForReward = _unitOfWork.GetRepository<Coupon>().Query()
                    .Where(c => c.RewardId == reward.Id);

                result.Add(new RewardDistributionDto
                {
                    RewardName = reward.RewardName,
                    TimesWon = couponsForReward.Count(),
                    TimesUsed = couponsForReward.Count(c => c.Status == CouponStatus.Used)
                });
            }

            return result;
        }
    }
}