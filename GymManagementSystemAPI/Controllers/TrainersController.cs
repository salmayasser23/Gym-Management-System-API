using GymManagementSystem.Api.DTOs.Trainers;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);

            if (trainer is null)
            {
                return NotFound(new { message = "Trainer not found." });
            }

            return Ok(trainer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTrainerDto dto)
        {
            try
            {
                var createdTrainer = await _trainerService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTrainerDto dto)
        {
            try
            {
                var updatedTrainer = await _trainerService.UpdateAsync(id, dto);

                if (updatedTrainer is null)
                {
                    return NotFound(new { message = "Trainer not found." });
                }

                return Ok(updatedTrainer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _trainerService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "Trainer not found." });
                }

                return Ok(new { message = "Trainer deleted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}