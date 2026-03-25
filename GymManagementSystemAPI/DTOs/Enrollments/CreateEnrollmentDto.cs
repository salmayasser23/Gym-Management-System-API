using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClassSessionId { get; set; }
    }
}