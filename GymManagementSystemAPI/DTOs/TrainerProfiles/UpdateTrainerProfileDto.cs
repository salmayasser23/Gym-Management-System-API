using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.TrainerProfiles
{
    public class UpdateTrainerProfileDto
    {
        [Required]
        [MaxLength(500)]
        public string Bio { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Certification { get; set; }

        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? EmergencyContactPhone { get; set; }
    }
}