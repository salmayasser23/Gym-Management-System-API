using GymManagementSystem.Api.Data;
using GymManagementSystem.Api.DTOs.TrainerProfiles;
using GymManagementSystem.Api.Entities;
using GymManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Services
{
    public class TrainerProfileService : ITrainerProfileService
    {
        private readonly AppDbContext _context;

        public TrainerProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainerProfileReadDto>> GetAllAsync()
        {
            return await _context.TrainerProfiles
                .AsNoTracking()
                .Select(tp => new TrainerProfileReadDto
                {
                    Id = tp.Id,
                    Bio = tp.Bio,
                    Certification = tp.Certification,
                    EmergencyContactName = tp.EmergencyContactName,
                    EmergencyContactPhone = tp.EmergencyContactPhone,
                    TrainerId = tp.TrainerId,
                    TrainerName = tp.Trainer.FullName
                })
                .ToListAsync();
        }

        public async Task<TrainerProfileReadDto?> GetByIdAsync(int id)
        {
            return await _context.TrainerProfiles
                .AsNoTracking()
                .Where(tp => tp.Id == id)
                .Select(tp => new TrainerProfileReadDto
                {
                    Id = tp.Id,
                    Bio = tp.Bio,
                    Certification = tp.Certification,
                    EmergencyContactName = tp.EmergencyContactName,
                    EmergencyContactPhone = tp.EmergencyContactPhone,
                    TrainerId = tp.TrainerId,
                    TrainerName = tp.Trainer.FullName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TrainerProfileReadDto?> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.TrainerProfiles
                .AsNoTracking()
                .Where(tp => tp.TrainerId == trainerId)
                .Select(tp => new TrainerProfileReadDto
                {
                    Id = tp.Id,
                    Bio = tp.Bio,
                    Certification = tp.Certification,
                    EmergencyContactName = tp.EmergencyContactName,
                    EmergencyContactPhone = tp.EmergencyContactPhone,
                    TrainerId = tp.TrainerId,
                    TrainerName = tp.Trainer.FullName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TrainerProfileReadDto> CreateAsync(CreateTrainerProfileDto dto)
        {
            var trainerExists = await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.Id == dto.TrainerId);

            if (!trainerExists)
            {
                throw new InvalidOperationException("Trainer does not exist.");
            }

            var profileExists = await _context.TrainerProfiles
                .AsNoTracking()
                .AnyAsync(tp => tp.TrainerId == dto.TrainerId);

            if (profileExists)
            {
                throw new InvalidOperationException("This trainer already has a profile.");
            }

            var profile = new TrainerProfile
            {
                Bio = dto.Bio.Trim(),
                Certification = dto.Certification?.Trim(),
                EmergencyContactName = dto.EmergencyContactName?.Trim(),
                EmergencyContactPhone = dto.EmergencyContactPhone?.Trim(),
                TrainerId = dto.TrainerId
            };

            _context.TrainerProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(profile.Id)
                   ?? throw new InvalidOperationException("Trainer profile was created but could not be retrieved.");
        }

        public async Task<TrainerProfileReadDto?> UpdateAsync(int id, UpdateTrainerProfileDto dto)
        {
            var profile = await _context.TrainerProfiles.FindAsync(id);

            if (profile is null)
            {
                return null;
            }

            profile.Bio = dto.Bio.Trim();
            profile.Certification = dto.Certification?.Trim();
            profile.EmergencyContactName = dto.EmergencyContactName?.Trim();
            profile.EmergencyContactPhone = dto.EmergencyContactPhone?.Trim();

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _context.TrainerProfiles.FindAsync(id);

            if (profile is null)
            {
                return false;
            }

            _context.TrainerProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}