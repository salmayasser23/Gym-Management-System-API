namespace GymManagementSystem.Api.DTOs.TrainerProfiles
{
    public class TrainerProfileReadDto
    {
        public int Id { get; set; }

        public string Bio { get; set; } = string.Empty;

        public string? Certification { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public int TrainerId { get; set; }

        public string TrainerName { get; set; } = string.Empty;
    }
}