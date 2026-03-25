using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.Trainers
{
    public class UpdateTrainerDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; } = string.Empty;

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }
    }
}