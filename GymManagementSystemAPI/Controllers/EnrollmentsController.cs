using GymManagementSystem.Api.DTOs.Enrollments;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            return Ok(enrollments);
        }

        [HttpGet("member/{memberId:int}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> GetByMemberId(int memberId)
        {
            var enrollments = await _enrollmentService.GetByMemberIdAsync(memberId);
            return Ok(enrollments);
        }

        [HttpGet("classsession/{classSessionId:int}")]
        [Authorize(Roles = "Admin,Trainer")]
        public async Task<IActionResult> GetByClassSessionId(int classSessionId)
        {
            var enrollments = await _enrollmentService.GetByClassSessionIdAsync(classSessionId);
            return Ok(enrollments);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
        {
            try
            {
                var createdEnrollment = await _enrollmentService.CreateAsync(dto);
                return Ok(createdEnrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{memberId:int}/{classSessionId:int}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Delete(int memberId, int classSessionId)
        {
            var deleted = await _enrollmentService.DeleteAsync(memberId, classSessionId);

            if (!deleted)
            {
                return NotFound(new { message = "Enrollment not found." });
            }

            return Ok(new { message = "Enrollment deleted successfully." });
        }
    }
}