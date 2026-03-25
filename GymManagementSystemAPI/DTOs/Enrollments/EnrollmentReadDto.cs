namespace GymManagementSystem.Api.DTOs.Enrollments
{
    public class EnrollmentReadDto
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public int ClassSessionId { get; set; }

        public string ClassTitle { get; set; } = string.Empty;

        public DateTime EnrollmentDate { get; set; }

        public DateTime ClassStartTime { get; set; }

        public DateTime ClassEndTime { get; set; }
    }
}