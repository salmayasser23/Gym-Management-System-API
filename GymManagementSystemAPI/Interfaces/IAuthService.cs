using GymManagementSystem.Api.DTOs.Auth;

namespace GymManagementSystem.Api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}