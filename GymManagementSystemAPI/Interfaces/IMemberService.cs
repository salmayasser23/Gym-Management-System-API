using GymManagementSystem.Api.DTOs.Members;

namespace GymManagementSystem.Api.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberReadDto>> GetAllAsync();
        Task<MemberReadDto?> GetByIdAsync(int id);
        Task<MemberReadDto> CreateAsync(CreateMemberDto dto);
        Task<MemberReadDto?> UpdateAsync(int id, UpdateMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}