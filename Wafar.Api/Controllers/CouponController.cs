using Wafar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("redeem")]
        public async Task<IActionResult> Redeem(
            [FromQuery] string uniqueCode,
            [FromQuery] int usedByUserId,
            [FromQuery] int? branchId)
        {
            var result = await _couponService.RedeemCouponAsync(uniqueCode, usedByUserId, branchId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}