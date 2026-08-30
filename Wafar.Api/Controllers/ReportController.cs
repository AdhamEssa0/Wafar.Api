using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wafar.Application.Interfaces;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary(
            [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _reportService.GetDashboardSummaryAsync(from, to);
            return Ok(result);
        }

        [HttpGet("qrcode-performance")]
        public async Task<IActionResult> GetQRCodePerformance(
            [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _reportService.GetQRCodePerformanceAsync(from, to);
            return Ok(result);
        }

        [HttpGet("reward-distribution")]
        public async Task<IActionResult> GetRewardDistribution(
            [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _reportService.GetRewardDistributionAsync(from, to);
            return Ok(result);
        }
    }
}