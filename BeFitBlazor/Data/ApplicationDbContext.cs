using BeFitBlazor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFitBlazor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<ExercisePerformed> ExercisePerformeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ExerciseType configuration
            modelBuilder.Entity<ExerciseType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // TrainingSession configuration
            modelBuilder.Entity<TrainingSession>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                
                // User relationship
                entity.HasOne(e => e.User)
                    .WithMany(u => u.TrainingSessions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasMany(e => e.Exercises)
                    .WithOne(e => e.TrainingSession)
                    .HasForeignKey(e => e.TrainingSessionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ExercisePerformed configuration
            modelBuilder.Entity<ExercisePerformed>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(e => e.TrainingSession)
                    .WithMany(t => t.Exercises)
                    .HasForeignKey(e => e.TrainingSessionId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.ExerciseType)
                    .WithMany()
                    .HasForeignKey(e => e.ExerciseTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
