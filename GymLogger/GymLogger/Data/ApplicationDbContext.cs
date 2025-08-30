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

            // Explicitní mapování všech string sloupců Identity na TEXT
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

            // Tvůj ExerciseMuscle vztah
            modelBuilder.Entity<ExerciseMuscle>()
                .HasOne(em => em.Exercise)
                .WithMany(e => e.ExerciseMuscles)
                .HasForeignKey(em => em.ExerciseId);

            modelBuilder.Entity<ExerciseMuscle>()
                .HasOne(em => em.Muscle)
                .WithMany(m => m.ExerciseMuscles)
                .HasForeignKey(em => em.MuscleId);

            // Session → User FK
            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .IsRequired();
        }
    }
}
