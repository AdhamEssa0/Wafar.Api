using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto.Username, dto.Password);

            if (result == null)
                return Unauthorized(new { message = "اسم المستخدم أو كلمة المرور غلط." });

            return Ok(result);
        }
    }
}