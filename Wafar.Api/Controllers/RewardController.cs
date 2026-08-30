using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;

namespace Wafar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RewardController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rewards = await _rewardService.GetAllAsync();
            return Ok(rewards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reward = await _rewardService.GetByIdAsync(id);
            if (reward == null)
                return NotFound();

            return Ok(reward);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRewardDto dto)
        {
            var created = await _rewardService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRewardDto dto)
        {
            var updated = await _rewardService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _rewardService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}