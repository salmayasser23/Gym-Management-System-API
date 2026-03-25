using GymManagementSystem.Api.DTOs.Members;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);

            if (member is null)
            {
                return NotFound(new { message = "Member not found." });
            }

            return Ok(member);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateMemberDto dto)
        {
            try
            {
                var createdMember = await _memberService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMember.Id }, createdMember);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto dto)
        {
            try
            {
                var updatedMember = await _memberService.UpdateAsync(id, dto);

                if (updatedMember is null)
                {
                    return NotFound(new { message = "Member not found." });
                }

                return Ok(updatedMember);
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
            var deleted = await _memberService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Member not found." });
            }

            return Ok(new { message = "Member deleted successfully." });
        }
    }
}