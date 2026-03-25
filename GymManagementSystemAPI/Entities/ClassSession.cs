using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.Entities
{
    public class ClassSession
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? RoomName { get; set; }

        [Range(1, 100)]
        public int Capacity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();
    }
}