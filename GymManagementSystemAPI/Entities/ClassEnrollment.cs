namespace GymManagementSystem.Api.Entities
{
    public class ClassEnrollment
    {
        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public int ClassSessionId { get; set; }

        public ClassSession ClassSession { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}