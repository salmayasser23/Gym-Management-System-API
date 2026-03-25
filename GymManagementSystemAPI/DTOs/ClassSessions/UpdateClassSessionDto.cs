using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.ClassSessions
{
    public class UpdateClassSessionDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? RoomName { get; set; }

        [Range(1, 100)]
        public int Capacity { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TrainerId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "EndTime must be later than StartTime.",
                    new[] { nameof(EndTime), nameof(StartTime) });
            }
        }
    }
}