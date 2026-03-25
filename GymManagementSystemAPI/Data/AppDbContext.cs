using GymManagementSystem.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerProfile> TrainerProfiles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ClassSession> ClassSessions { get; set; }
        public DbSet<ClassEnrollment> ClassEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Trainer>()
                .HasOne(t => t.TrainerProfile)
                .WithOne(tp => tp.Trainer)
                .HasForeignKey<TrainerProfile>(tp => tp.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Trainer>()
                .HasMany(t => t.ClassSessions)
                .WithOne(cs => cs.Trainer)
                .HasForeignKey(cs => cs.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ClassEnrollment>()
                .HasKey(ce => new { ce.MemberId, ce.ClassSessionId });

            builder.Entity<ClassEnrollment>()
                .HasOne(ce => ce.Member)
                .WithMany(m => m.ClassEnrollments)
                .HasForeignKey(ce => ce.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ClassEnrollment>()
                .HasOne(ce => ce.ClassSession)
                .WithMany(cs => cs.ClassEnrollments)
                .HasForeignKey(ce => ce.ClassSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Trainer>()
                .HasIndex(t => t.Email)
                .IsUnique();

            builder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();
        }
    }
}