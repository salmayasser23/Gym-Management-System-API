using GymManagementSystem.Api.DTOs.Trainers;

namespace GymManagementSystem.Api.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerReadDto>> GetAllAsync();
        Task<TrainerReadDto?> GetByIdAsync(int id);
        Task<TrainerReadDto> CreateAsync(CreateTrainerDto dto);
        Task<TrainerReadDto?> UpdateAsync(int id, UpdateTrainerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}