using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wafar.Application.Interfaces;
using System.Security.Claims;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Staff,Admin,SuperAdmin")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coupons = await _couponService.GetAllWithDetailsAsync();

            return Ok(coupons);
        }

        [HttpPost("redeem")]
        public async Task<IActionResult> Redeem(
            [FromQuery] string uniqueCode,
            [FromQuery] int? branchId,
            [FromQuery] decimal invoiceAmount,
            [FromQuery] string? productName)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var usedByUserId))
                return Unauthorized();

            var result = await _couponService.RedeemCouponAsync(
                uniqueCode,
                usedByUserId,
                branchId,
                invoiceAmount,
                productName);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}