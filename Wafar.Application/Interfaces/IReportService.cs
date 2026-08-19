using Wafar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.Interfaces
{
    public interface IReportService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime? from, DateTime? to);
        Task<IEnumerable<QRCodePerformanceDto>> GetQRCodePerformanceAsync(DateTime? from, DateTime? to);
        Task<IEnumerable<RewardDistributionDto>> GetRewardDistributionAsync(DateTime? from, DateTime? to);
    }
}
