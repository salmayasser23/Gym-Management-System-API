namespace GymManagementSystem.Api.DTOs.Trainers
{
    public class TrainerReadDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Specialization { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; }

        public bool HasProfile { get; set; }
    }
}