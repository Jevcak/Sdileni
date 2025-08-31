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
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Tlaky na lavičce" },
                new Exercise { Id = 2, Name = "Dřepy" },
                new Exercise { Id = 3, Name = "Mrtvý tah" },
                new Exercise { Id = 4, Name = "Shyby s váhou" },
                new Exercise { Id = 5, Name = "Přemístění" },
                new Exercise { Id = 6, Name = "Bulharský dřep" },
                new Exercise { Id = 7, Name = "Rumunský mrtvý tah" },
                new Exercise { Id = 8, Name = "Biceps s jednoručkami" },
                new Exercise { Id = 9, Name = "Bench press s osou" },
                new Exercise { Id = 10, Name = "Výpady s váhou" },
                new Exercise { Id = 11, Name = "Calf Raises" },
                new Exercise { Id = 12, Name = "Triceps" },
                new Exercise { Id = 13, Name = "Leg Extensions" },
                new Exercise { Id = 14, Name = "Leg Raises" },
                new Exercise { Id = 15, Name = "Overhead Press" },
                new Exercise { Id = 16, Name = "Přítahy" },
                new Exercise { Id = 17, Name = "Tlaky na ramena s jednoručkami" },
                new Exercise { Id = 19, Name = "Hip Thrust" },
                new Exercise { Id = 20, Name = "Kettlebell Swing" }
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
