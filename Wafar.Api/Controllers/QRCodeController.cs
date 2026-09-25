using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _qrCodeService;

        public QRCodeController(IQRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _qrCodeService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var qr = await _qrCodeService.GetByIdAsync(id);
            return qr == null ? NotFound() : Ok(qr);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQRCodeDto dto)
        {
            var created = await _qrCodeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQRCodeDto dto)
        {
            var updated = await _qrCodeService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _qrCodeService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var toggled = await _qrCodeService.ToggleActiveAsync(id);
            return toggled ? NoContent() : NotFound();
        }
        [HttpGet("{id}/rewards")]
        public async Task<IActionResult> GetRewards(int id)
        {
            var rewards = await _qrCodeService.GetRewardsAsync(id);
            return Ok(rewards);
        }

        [HttpPut("{id}/rewards")]
        public async Task<IActionResult> UpdateRewards(
            int id,
            [FromBody] IReadOnlyList<QRCodeRewardDto> rewards)
        {
            var updated = await _qrCodeService.UpdateRewardsAsync(id, rewards);

            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}