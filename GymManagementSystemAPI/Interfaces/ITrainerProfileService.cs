using GymManagementSystem.Api.DTOs.TrainerProfiles;

namespace GymManagementSystem.Api.Interfaces
{
    public interface ITrainerProfileService
    {
        Task<IEnumerable<TrainerProfileReadDto>> GetAllAsync();
        Task<TrainerProfileReadDto?> GetByIdAsync(int id);
        Task<TrainerProfileReadDto?> GetByTrainerIdAsync(int trainerId);
        Task<TrainerProfileReadDto> CreateAsync(CreateTrainerProfileDto dto);
        Task<TrainerProfileReadDto?> UpdateAsync(int id, UpdateTrainerProfileDto dto);
        Task<bool> DeleteAsync(int id);
    }
}