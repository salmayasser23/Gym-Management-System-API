using GymManagementSystem.Api.DTOs.Auth;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Helpers;
using GymManagementSystem.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace GymManagementSystem.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenGenerator jwtTokenGenerator,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "A user with this email already exists."
                };
            }

            var roleExists = await _roleManager.RoleExistsAsync(dto.Role);
            if (!roleExists)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = $"Role '{dto.Role}' does not exist."
                };
            }

            var user = new AppUser
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                UserName = dto.Email.Trim()
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", result.Errors.Select(e => e.Description))
                };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", roleResult.Errors.Select(e => e.Description))
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60"));

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration completed successfully.",
                Token = token,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Role = roles.FirstOrDefault() ?? string.Empty,
                Expiration = expiration
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:DurationInMinutes"] ?? "60"));

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful.",
                Token = token,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Role = roles.FirstOrDefault() ?? string.Empty,
                Expiration = expiration
            };
        }
    }
}