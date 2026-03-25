using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Api.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
    }
}