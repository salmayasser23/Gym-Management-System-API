using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Admin|Trainer|Member", ErrorMessage = "Role must be Admin, Trainer, or Member.")]
        public string Role { get; set; } = string.Empty;
    }
}