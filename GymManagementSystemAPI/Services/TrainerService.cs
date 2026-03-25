using GymManagementSystem.Api.Data;
using GymManagementSystem.Api.DTOs.Trainers;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly AppDbContext _context;

        public TrainerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainerReadDto>> GetAllAsync()
        {
            return await _context.Trainers
                .AsNoTracking()
                .Select(t => new TrainerReadDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Specialization = t.Specialization,
                    YearsOfExperience = t.YearsOfExperience,
                    HasProfile = t.TrainerProfile != null
                })
                .ToListAsync();
        }

        public async Task<TrainerReadDto?> GetByIdAsync(int id)
        {
            return await _context.Trainers
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TrainerReadDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Specialization = t.Specialization,
                    YearsOfExperience = t.YearsOfExperience,
                    HasProfile = t.TrainerProfile != null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TrainerReadDto> CreateAsync(CreateTrainerDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLower();

            var emailExists = await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new InvalidOperationException("A trainer with this email already exists.");
            }

            var trainer = new Trainer
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                PhoneNumber = dto.PhoneNumber?.Trim(),
                Specialization = dto.Specialization.Trim(),
                YearsOfExperience = dto.YearsOfExperience
            };

            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(trainer.Id)
                   ?? throw new InvalidOperationException("Trainer was created but could not be retrieved.");
        }

        public async Task<TrainerReadDto?> UpdateAsync(int id, UpdateTrainerDto dto)
        {
            var trainer = await _context.Trainers.FindAsync(id);

            if (trainer is null)
            {
                return null;
            }

            var normalizedEmail = dto.Email.Trim().ToLower();

            var emailExists = await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.Id != id && t.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new InvalidOperationException("Another trainer with this email already exists.");
            }

            trainer.FullName = dto.FullName.Trim();
            trainer.Email = dto.Email.Trim();
            trainer.PhoneNumber = dto.PhoneNumber?.Trim();
            trainer.Specialization = dto.Specialization.Trim();
            trainer.YearsOfExperience = dto.YearsOfExperience;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _context.Trainers
                .Include(t => t.ClassSessions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trainer is null)
            {
                return false;
            }

            if (trainer.ClassSessions.Any())
            {
                throw new InvalidOperationException("Cannot delete a trainer who is assigned to class sessions.");
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}