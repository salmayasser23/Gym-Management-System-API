using GymManagementSystem.Api.DTOs.ClassSessions;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClassSessionsController : ControllerBase
    {
        private readonly IClassSessionService _classSessionService;

        public ClassSessionsController(IClassSessionService classSessionService)
        {
            _classSessionService = classSessionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetAll()
        {
            var classSessions = await _classSessionService.GetAllAsync();
            return Ok(classSessions);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetById(int id)
        {
            var classSession = await _classSessionService.GetByIdAsync(id);

            if (classSession is null)
            {
                return NotFound(new { message = "Class session not found." });
            }

            return Ok(classSession);
        }

        [HttpGet("trainer/{trainerId:int}")]
        [Authorize(Roles = "Admin,Trainer,Member")]
        public async Task<IActionResult> GetByTrainerId(int trainerId)
        {
            var classSessions = await _classSessionService.GetByTrainerIdAsync(trainerId);
            return Ok(classSessions);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Trainer")]
        public async Task<IActionResult> Create([FromBody] CreateClassSessionDto dto)
        {
            try
            {
                var createdClassSession = await _classSessionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdClassSession.Id }, createdClassSession);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Trainer")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClassSessionDto dto)
        {
            try
            {
                var updatedClassSession = await _classSessionService.UpdateAsync(id, dto);

                if (updatedClassSession is null)
                {
                    return NotFound(new { message = "Class session not found." });
                }

                return Ok(updatedClassSession);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Trainer")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _classSessionService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Class session not found." });
            }

            return Ok(new { message = "Class session deleted successfully." });
        }
    }
}