using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.Entities
{
    public class Trainer
    {
        public int Id { get; set; }

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

        public TrainerProfile? TrainerProfile { get; set; }

        public ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();
    }
}