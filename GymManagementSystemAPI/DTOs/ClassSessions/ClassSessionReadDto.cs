namespace GymManagementSystem.Api.DTOs.ClassSessions
{
    public class ClassSessionReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? RoomName { get; set; }

        public int Capacity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TrainerId { get; set; }

        public string TrainerName { get; set; } = string.Empty;

        public int EnrolledCount { get; set; }

        public int AvailableSeats { get; set; }
    }
}