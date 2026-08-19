using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalScans { get; set; }
        public int TotalCouponsIssued { get; set; }
        public int TotalCouponsUsed { get; set; }
        public int TotalCouponsExpired { get; set; }
        public decimal TotalPendingCommissions { get; set; }
        public decimal TotalPaidCommissions { get; set; }
    }

    public class QRCodePerformanceDto
    {
        public int QRCodeId { get; set; }
        public string QRCodeName { get; set; } = null!;
        public int ScanCount { get; set; }
        public int CouponsIssued { get; set; }
        public int CouponsUsed { get; set; }
    }

    public class RewardDistributionDto
    {
        public string RewardName { get; set; } = null!;
        public int TimesWon { get; set; }
        public int TimesUsed { get; set; }
    }
    
}
