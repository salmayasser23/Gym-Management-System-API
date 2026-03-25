using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.Entities
{
    public class Member
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
        [MaxLength(50)]
        public string MembershipType { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();
    }
}