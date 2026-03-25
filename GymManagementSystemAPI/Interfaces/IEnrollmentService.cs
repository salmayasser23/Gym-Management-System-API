using GymManagementSystem.Api.DTOs.Enrollments;

namespace GymManagementSystem.Api.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentReadDto>> GetAllAsync();
        Task<IEnumerable<EnrollmentReadDto>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<EnrollmentReadDto>> GetByClassSessionIdAsync(int classSessionId);
        Task<EnrollmentReadDto> CreateAsync(CreateEnrollmentDto dto);
        Task<bool> DeleteAsync(int memberId, int classSessionId);
    }
}