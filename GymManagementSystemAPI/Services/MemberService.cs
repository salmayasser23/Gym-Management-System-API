using GymManagementSystem.Api.Data;
using GymManagementSystem.Api.DTOs.Members;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberReadDto>> GetAllAsync()
        {
            return await _context.Members
                .AsNoTracking()
                .Select(m => new MemberReadDto
                {
                    Id = m.Id,
                    FullName = m.FullName,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    MembershipType = m.MembershipType,
                    JoinDate = m.JoinDate,
                    TotalEnrollments = m.ClassEnrollments.Count
                })
                .ToListAsync();
        }

        public async Task<MemberReadDto?> GetByIdAsync(int id)
        {
            return await _context.Members
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new MemberReadDto
                {
                    Id = m.Id,
                    FullName = m.FullName,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    MembershipType = m.MembershipType,
                    JoinDate = m.JoinDate,
                    TotalEnrollments = m.ClassEnrollments.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MemberReadDto> CreateAsync(CreateMemberDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLower();

            var emailExists = await _context.Members
                .AsNoTracking()
                .AnyAsync(m => m.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new InvalidOperationException("A member with this email already exists.");
            }

            var member = new Member
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                PhoneNumber = dto.PhoneNumber?.Trim(),
                MembershipType = dto.MembershipType.Trim(),
                JoinDate = dto.JoinDate ?? DateTime.UtcNow
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(member.Id)
                   ?? throw new InvalidOperationException("Member was created but could not be retrieved.");
        }

        public async Task<MemberReadDto?> UpdateAsync(int id, UpdateMemberDto dto)
        {
            var member = await _context.Members.FindAsync(id);

            if (member is null)
            {
                return null;
            }

            var normalizedEmail = dto.Email.Trim().ToLower();

            var emailExists = await _context.Members
                .AsNoTracking()
                .AnyAsync(m => m.Id != id && m.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new InvalidOperationException("Another member with this email already exists.");
            }

            member.FullName = dto.FullName.Trim();
            member.Email = dto.Email.Trim();
            member.PhoneNumber = dto.PhoneNumber?.Trim();
            member.MembershipType = dto.MembershipType.Trim();
            member.JoinDate = dto.JoinDate;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member is null)
            {
                return false;
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}