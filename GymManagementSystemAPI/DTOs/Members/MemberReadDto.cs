namespace GymManagementSystem.Api.DTOs.Members
{
    public class MemberReadDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string MembershipType { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; }

        public int TotalEnrollments { get; set; }
    }
}