using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.Members
{
    public class UpdateMemberDto
    {
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

        public DateTime JoinDate { get; set; }
    }
}