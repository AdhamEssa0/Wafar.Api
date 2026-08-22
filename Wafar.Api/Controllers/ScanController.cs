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
        public async Task<IActionResult> Scan(string qrCode, [FromQuery] int? customerId)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var deviceInfo = Request.Headers.UserAgent.ToString();

            var result = await _couponService.ProcessScanAsync(qrCode, customerId, ipAddress, deviceInfo);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        } 
    }
}
