using GymManagementSystem.Api.Data;
using GymManagementSystem.Api.DTOs.Enrollments;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnrollmentReadDto>> GetAllAsync()
        {
            return await _context.ClassEnrollments
                .AsNoTracking()
                .Select(e => new EnrollmentReadDto
                {
                    MemberId = e.MemberId,
                    MemberName = e.Member.FullName,
                    ClassSessionId = e.ClassSessionId,
                    ClassTitle = e.ClassSession.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    ClassStartTime = e.ClassSession.StartTime,
                    ClassEndTime = e.ClassSession.EndTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EnrollmentReadDto>> GetByMemberIdAsync(int memberId)
        {
            return await _context.ClassEnrollments
                .AsNoTracking()
                .Where(e => e.MemberId == memberId)
                .Select(e => new EnrollmentReadDto
                {
                    MemberId = e.MemberId,
                    MemberName = e.Member.FullName,
                    ClassSessionId = e.ClassSessionId,
                    ClassTitle = e.ClassSession.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    ClassStartTime = e.ClassSession.StartTime,
                    ClassEndTime = e.ClassSession.EndTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EnrollmentReadDto>> GetByClassSessionIdAsync(int classSessionId)
        {
            return await _context.ClassEnrollments
                .AsNoTracking()
                .Where(e => e.ClassSessionId == classSessionId)
                .Select(e => new EnrollmentReadDto
                {
                    MemberId = e.MemberId,
                    MemberName = e.Member.FullName,
                    ClassSessionId = e.ClassSessionId,
                    ClassTitle = e.ClassSession.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    ClassStartTime = e.ClassSession.StartTime,
                    ClassEndTime = e.ClassSession.EndTime
                })
                .ToListAsync();
        }

        public async Task<EnrollmentReadDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var memberExists = await _context.Members
                .AsNoTracking()
                .AnyAsync(m => m.Id == dto.MemberId);

            if (!memberExists)
            {
                throw new InvalidOperationException("Member does not exist.");
            }

            var classSession = await _context.ClassSessions
                .AsNoTracking()
                .Where(cs => cs.Id == dto.ClassSessionId)
                .Select(cs => new
                {
                    cs.Id,
                    cs.Title,
                    cs.Capacity,
                    cs.StartTime,
                    cs.EndTime
                })
                .FirstOrDefaultAsync();

            if (classSession is null)
            {
                throw new InvalidOperationException("Class session does not exist.");
            }

            var alreadyEnrolled = await _context.ClassEnrollments
                .AsNoTracking()
                .AnyAsync(e => e.MemberId == dto.MemberId && e.ClassSessionId == dto.ClassSessionId);

            if (alreadyEnrolled)
            {
                throw new InvalidOperationException("This member is already enrolled in the selected class.");
            }

            var enrolledCount = await _context.ClassEnrollments
                .AsNoTracking()
                .CountAsync(e => e.ClassSessionId == dto.ClassSessionId);

            if (enrolledCount >= classSession.Capacity)
            {
                throw new InvalidOperationException("This class session is already full.");
            }

            var enrollment = new ClassEnrollment
            {
                MemberId = dto.MemberId,
                ClassSessionId = dto.ClassSessionId,
                EnrollmentDate = DateTime.UtcNow
            };

            _context.ClassEnrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return await _context.ClassEnrollments
                .AsNoTracking()
                .Where(e => e.MemberId == dto.MemberId && e.ClassSessionId == dto.ClassSessionId)
                .Select(e => new EnrollmentReadDto
                {
                    MemberId = e.MemberId,
                    MemberName = e.Member.FullName,
                    ClassSessionId = e.ClassSessionId,
                    ClassTitle = e.ClassSession.Title,
                    EnrollmentDate = e.EnrollmentDate,
                    ClassStartTime = e.ClassSession.StartTime,
                    ClassEndTime = e.ClassSession.EndTime
                })
                .FirstAsync();
        }

        public async Task<bool> DeleteAsync(int memberId, int classSessionId)
        {
            var enrollment = await _context.ClassEnrollments.FindAsync(memberId, classSessionId);

            if (enrollment is null)
            {
                return false;
            }

            _context.ClassEnrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}