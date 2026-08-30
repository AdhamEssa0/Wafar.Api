using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;

namespace Wafar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("get-or-create")]
        public async Task<IActionResult> GetOrCreate([FromBody] GetOrCreateCustomerDto dto)
        {
            var customer = await _customerService.GetOrCreateByPhoneAsync(dto);
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpGet("{id}/coupons")]
        public async Task<IActionResult> GetCoupons(int id)
        {
            var coupons = await _customerService.GetCouponsAsync(id);
            return Ok(coupons);
        }
    }
}
