using GymManagementSystem.Api.DTOs.TrainerProfiles;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TrainerProfilesController : ControllerBase
    {
        private readonly ITrainerProfileService _trainerProfileService;

        public TrainerProfilesController(ITrainerProfileService trainerProfileService)
        {
            _trainerProfileService = trainerProfileService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _trainerProfileService.GetAllAsync();
            return Ok(profiles);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _trainerProfileService.GetByIdAsync(id);

            if (profile is null)
            {
                return NotFound(new { message = "Trainer profile not found." });
            }

            return Ok(profile);
        }

        [HttpGet("trainer/{trainerId:int}")]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetByTrainerId(int trainerId)
        {
            var profile = await _trainerProfileService.GetByTrainerIdAsync(trainerId);

            if (profile is null)
            {
                return NotFound(new { message = "Trainer profile not found for this trainer." });
            }

            return Ok(profile);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTrainerProfileDto dto)
        {
            try
            {
                var createdProfile = await _trainerProfileService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdProfile.Id }, createdProfile);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTrainerProfileDto dto)
        {
            try
            {
                var updatedProfile = await _trainerProfileService.UpdateAsync(id, dto);

                if (updatedProfile is null)
                {
                    return NotFound(new { message = "Trainer profile not found." });
                }

                return Ok(updatedProfile);
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
            var deleted = await _trainerProfileService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Trainer profile not found." });
            }

            return Ok(new { message = "Trainer profile deleted successfully." });
        }
    }
}