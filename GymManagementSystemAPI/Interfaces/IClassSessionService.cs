using GymManagementSystem.Api.DTOs.ClassSessions;

namespace GymManagementSystem.Api.Interfaces
{
    public interface IClassSessionService
    {
        Task<IEnumerable<ClassSessionReadDto>> GetAllAsync();
        Task<ClassSessionReadDto?> GetByIdAsync(int id);
        Task<IEnumerable<ClassSessionReadDto>> GetByTrainerIdAsync(int trainerId);
        Task<ClassSessionReadDto> CreateAsync(CreateClassSessionDto dto);
        Task<ClassSessionReadDto?> UpdateAsync(int id, UpdateClassSessionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}