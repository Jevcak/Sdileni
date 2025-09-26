using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSession> ExerciseSessions { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<ExerciseMuscle> ExerciseMuscles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IdentityUser).IsAssignableFrom(entityType.ClrType) ||
                    typeof(IdentityRole).IsAssignableFrom(entityType.ClrType))
                {
                    foreach (var property in entityType.GetProperties())
                    {
                        if (property.ClrType == typeof(string))
                        {
                            property.SetColumnType("TEXT");
                        }
                    }
                }
            }
            modelBuilder.Entity<ExerciseMuscle>()
                .HasKey(em => new { em.ExerciseId, em.MuscleId });
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Bench Press" },
                new Exercise { Id = 2, Name = "Squat" },
                new Exercise { Id = 3, Name = "Deadlift" },
                new Exercise { Id = 4, Name = "Weighted Pull-ups" },
                new Exercise { Id = 5, Name = "Clean" },
                new Exercise { Id = 6, Name = "Bulgarian Split Squat" },
                new Exercise { Id = 7, Name = "Romanian Deadlift" },
                new Exercise { Id = 8, Name = "Biceps Curl" },
                new Exercise { Id = 9, Name = "Weighted Lunges" },
                new Exercise { Id = 10, Name = "Calf Raises" },
                new Exercise { Id = 11, Name = "Triceps Extension" },
                new Exercise { Id = 12, Name = "Leg Extensions" },
                new Exercise { Id = 13, Name = "Leg Raises" },
                new Exercise { Id = 14, Name = "Overhead Press" },
                new Exercise { Id = 15, Name = "Rows" },
                new Exercise { Id = 16, Name = "Shoulder Press" },
                new Exercise { Id = 17, Name = "Hip Thrust" },
                new Exercise { Id = 18, Name = "Kettlebell Swing" }
            );
            modelBuilder.Entity<Muscle>().HasData(
                new Muscle { Id = 1, Name = "Hamstrings", Importance = 4 },
                new Muscle { Id = 2, Name = "Quadriceps", Importance = 5 },
                new Muscle { Id = 3, Name = "Glutes", Importance = 5 },
                new Muscle { Id = 4, Name = "Calves", Importance = 2 },
                new Muscle { Id = 5, Name = "Chest", Importance = 5 },
                new Muscle { Id = 6, Name = "Back", Importance = 5 },
                new Muscle { Id = 7, Name = "Shoulders", Importance = 4 },
                new Muscle { Id = 8, Name = "Biceps", Importance = 3 },
                new Muscle { Id = 9, Name = "Triceps", Importance = 3 },
                new Muscle { Id = 10, Name = "Forearms", Importance = 1 },
                new Muscle { Id = 11, Name = "Core", Importance = 3 },
                new Muscle { Id = 12, Name = "Trapezius", Importance = 2 },
                new Muscle { Id = 13, Name = "Adductors", Importance = 1 }
            );

            modelBuilder.Entity<ExerciseMuscle>().HasData(
                // 1. Bench Press
                new ExerciseMuscle { ExerciseId = 1, MuscleId = 5 }, // Chest
                new ExerciseMuscle { ExerciseId = 1, MuscleId = 7 }, // Shoulders
                new ExerciseMuscle { ExerciseId = 1, MuscleId = 9 }, // Triceps

                // 2. Squat
                new ExerciseMuscle { ExerciseId = 2, MuscleId = 2 }, // Quadriceps
                new ExerciseMuscle { ExerciseId = 2, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 2, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 2, MuscleId = 13 }, // Adductors

                // 3. Deadlift
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 6 }, // Back
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 10 }, // Forearms
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 12 }, // Trapezius
                new ExerciseMuscle { ExerciseId = 3, MuscleId = 11 }, // Core

                // 4. Weighted Pull-ups
                new ExerciseMuscle { ExerciseId = 4, MuscleId = 6 }, // Back
                new ExerciseMuscle { ExerciseId = 4, MuscleId = 10 }, // Forearms

                // 5. Clean
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 2 }, // Quads
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 7 }, // Shoulders
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 12 }, // Traps
                new ExerciseMuscle { ExerciseId = 5, MuscleId = 11 }, // Core

                // 6. Bulgarian Split Squat
                new ExerciseMuscle { ExerciseId = 6, MuscleId = 2 }, // Quads
                new ExerciseMuscle { ExerciseId = 6, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 6, MuscleId = 3 }, // Glutes

                // 7. Romanian Deadlift
                new ExerciseMuscle { ExerciseId = 7, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 7, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 7, MuscleId = 6 }, // Back

                // 8. Biceps Curl
                new ExerciseMuscle { ExerciseId = 8, MuscleId = 8 }, // Biceps
                new ExerciseMuscle { ExerciseId = 8, MuscleId = 10 }, // Forearms

                // 9. Weighted Lunges
                new ExerciseMuscle { ExerciseId = 9, MuscleId = 2 }, // Quads
                new ExerciseMuscle { ExerciseId = 9, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 9, MuscleId = 3 }, // Glutes

                // 10. Calf Raises
                new ExerciseMuscle { ExerciseId = 10, MuscleId = 4 }, // Calves

                // 11. Triceps Extension
                new ExerciseMuscle { ExerciseId = 11, MuscleId = 9 }, // Triceps

                // 12. Leg Extensions
                new ExerciseMuscle { ExerciseId = 12, MuscleId = 2 }, // Quads

                // 13. Leg Raises
                new ExerciseMuscle { ExerciseId = 13, MuscleId = 11 }, // Core
                new ExerciseMuscle { ExerciseId = 13, MuscleId = 3 }, // Glutes

                // 14. Overhead Press
                new ExerciseMuscle { ExerciseId = 14, MuscleId = 7 }, // Shoulders
                new ExerciseMuscle { ExerciseId = 14, MuscleId = 12 }, // Traps

                // 15. Rows
                new ExerciseMuscle { ExerciseId = 15, MuscleId = 6 }, // Back
                new ExerciseMuscle { ExerciseId = 15, MuscleId = 10 }, // Forearms
                new ExerciseMuscle { ExerciseId = 15, MuscleId = 12 }, // Traps

                // 16. Shoulder Press
                new ExerciseMuscle { ExerciseId = 16, MuscleId = 7 }, // Shoulders
                new ExerciseMuscle { ExerciseId = 16, MuscleId = 12 }, // Traps

                // 17. Hip Thrust
                new ExerciseMuscle { ExerciseId = 17, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 17, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 17, MuscleId = 11 }, // Core

                // 18. Kettlebell Swing
                new ExerciseMuscle { ExerciseId = 18, MuscleId = 1 }, // Hamstrings
                new ExerciseMuscle { ExerciseId = 18, MuscleId = 3 }, // Glutes
                new ExerciseMuscle { ExerciseId = 18, MuscleId = 11 }  // Core
            );
            modelBuilder.Entity<ExerciseMuscle>()
                .HasOne(em => em.Exercise)
                .WithMany(e => e.ExerciseMuscles)
                .HasForeignKey(em => em.ExerciseId);

            modelBuilder.Entity<ExerciseMuscle>()
                .HasOne(em => em.Muscle)
                .WithMany(m => m.ExerciseMuscles)
                .HasForeignKey(em => em.MuscleId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .IsRequired();
        }
    }
}
