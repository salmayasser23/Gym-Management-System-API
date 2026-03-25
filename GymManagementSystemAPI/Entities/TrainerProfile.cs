using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.Entities
{
    public class TrainerProfile
    {
        public int Id { get; set; }

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

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;
    }
}