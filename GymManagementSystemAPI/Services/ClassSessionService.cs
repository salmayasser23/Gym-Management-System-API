using GymManagementSystem.Api.Data;
using GymManagementSystem.Api.DTOs.ClassSessions;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Services
{
    public class ClassSessionService : IClassSessionService
    {
        private readonly AppDbContext _context;

        public ClassSessionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassSessionReadDto>> GetAllAsync()
        {
            return await _context.ClassSessions
                .AsNoTracking()
                .Select(cs => new ClassSessionReadDto
                {
                    Id = cs.Id,
                    Title = cs.Title,
                    Description = cs.Description,
                    RoomName = cs.RoomName,
                    Capacity = cs.Capacity,
                    StartTime = cs.StartTime,
                    EndTime = cs.EndTime,
                    TrainerId = cs.TrainerId,
                    TrainerName = cs.Trainer.FullName,
                    EnrolledCount = cs.ClassEnrollments.Count,
                    AvailableSeats = cs.Capacity - cs.ClassEnrollments.Count
                })
                .ToListAsync();
        }

        public async Task<ClassSessionReadDto?> GetByIdAsync(int id)
        {
            return await _context.ClassSessions
                .AsNoTracking()
                .Where(cs => cs.Id == id)
                .Select(cs => new ClassSessionReadDto
                {
                    Id = cs.Id,
                    Title = cs.Title,
                    Description = cs.Description,
                    RoomName = cs.RoomName,
                    Capacity = cs.Capacity,
                    StartTime = cs.StartTime,
                    EndTime = cs.EndTime,
                    TrainerId = cs.TrainerId,
                    TrainerName = cs.Trainer.FullName,
                    EnrolledCount = cs.ClassEnrollments.Count,
                    AvailableSeats = cs.Capacity - cs.ClassEnrollments.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClassSessionReadDto>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.ClassSessions
                .AsNoTracking()
                .Where(cs => cs.TrainerId == trainerId)
                .Select(cs => new ClassSessionReadDto
                {
                    Id = cs.Id,
                    Title = cs.Title,
                    Description = cs.Description,
                    RoomName = cs.RoomName,
                    Capacity = cs.Capacity,
                    StartTime = cs.StartTime,
                    EndTime = cs.EndTime,
                    TrainerId = cs.TrainerId,
                    TrainerName = cs.Trainer.FullName,
                    EnrolledCount = cs.ClassEnrollments.Count,
                    AvailableSeats = cs.Capacity - cs.ClassEnrollments.Count
                })
                .ToListAsync();
        }

        public async Task<ClassSessionReadDto> CreateAsync(CreateClassSessionDto dto)
        {
            if (dto.EndTime <= dto.StartTime)
            {
                throw new InvalidOperationException("End time must be later than start time.");
            }

            var trainerExists = await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.Id == dto.TrainerId);

            if (!trainerExists)
            {
                throw new InvalidOperationException("Trainer does not exist.");
            }

            var classSession = new ClassSession
            {
                Title = dto.Title.Trim(),
                Description = dto.Description?.Trim(),
                RoomName = dto.RoomName?.Trim(),
                Capacity = dto.Capacity,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                TrainerId = dto.TrainerId
            };

            _context.ClassSessions.Add(classSession);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(classSession.Id)
                   ?? throw new InvalidOperationException("Class session was created but could not be retrieved.");
        }

        public async Task<ClassSessionReadDto?> UpdateAsync(int id, UpdateClassSessionDto dto)
        {
            var classSession = await _context.ClassSessions.FindAsync(id);

            if (classSession is null)
            {
                return null;
            }

            if (dto.EndTime <= dto.StartTime)
            {
                throw new InvalidOperationException("End time must be later than start time.");
            }

            var trainerExists = await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.Id == dto.TrainerId);

            if (!trainerExists)
            {
                throw new InvalidOperationException("Trainer does not exist.");
            }

            var enrolledCount = await _context.ClassEnrollments
                .AsNoTracking()
                .CountAsync(e => e.ClassSessionId == id);

            if (dto.Capacity < enrolledCount)
            {
                throw new InvalidOperationException("Capacity cannot be less than the current enrolled count.");
            }

            classSession.Title = dto.Title.Trim();
            classSession.Description = dto.Description?.Trim();
            classSession.RoomName = dto.RoomName?.Trim();
            classSession.Capacity = dto.Capacity;
            classSession.StartTime = dto.StartTime;
            classSession.EndTime = dto.EndTime;
            classSession.TrainerId = dto.TrainerId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var classSession = await _context.ClassSessions.FindAsync(id);

            if (classSession is null)
            {
                return false;
            }

            _context.ClassSessions.Remove(classSession);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}