using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Wafar.Application.Interfaces;

namespace Wafar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public ScanController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost("{qrCode}")]
        [EnableRateLimiting("ScanPolicy")]
        public async Task<IActionResult> Scan(string qrCode, [FromQuery] int? customerId, [FromQuery] int? categoryId)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var deviceInfo = Request.Headers.UserAgent.ToString();
            var result = await _couponService.ProcessScanAsync(qrCode, customerId, ipAddress, deviceInfo, categoryId, null);
            return Ok(result);
        }

        [HttpGet("{qrCode}/categories")]
        public async Task<IActionResult> GetCategories(string qrCode)
        {
            var categories = await _couponService.GetAvailableCategoriesAsync(qrCode);
            return Ok(categories);
        }
    } 
    
}
