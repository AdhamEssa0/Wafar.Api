using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _partnerService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var partner = await _partnerService.GetByIdAsync(id);
            return partner == null ? NotFound() : Ok(partner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePartnerDto dto)
        {
            var created = await _partnerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePartnerDto dto)
        {
            var updated = await _partnerService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _partnerService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("{id}/commissions")]
        public async Task<IActionResult> GetCommissions(int id)
        {
            var report = await _partnerService.GetCommissionsAsync(id);
            return report == null ? NotFound() : Ok(report);
        }
    }
}