using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace GymLogger
{
    public class User : IdentityUser
    {
        [Required, StringLength(50)]
        public string FullName { get; set; } = string.Empty;
        public ICollection<Session>? Sessions { get; set; }
    }

    public class Session
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Feeling { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<ExerciseSession>? ExerciseSessions { get; set; }
        public double TotalWeightLifted =>
            ExerciseSessions?.Sum(es => es.Weight * es.NofRepetitions * es.NofSets) ?? 0;
    }

    public class Exercise
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
    }

    public class ExerciseSession
    {
        public int Id { get; set; }

        [Required]
        public int SessionId { get; set; }

        public Session? Session { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        public Exercise? Exercise { get; set; }

        [Required, Range(0, 500)]
        public double Weight { get; set; }

        [Required, Range(1, 100)]
        public int NofRepetitions { get; set; }

        [Required, Range(1, 50)]
        public int NofSets { get; set; }

        public bool IsSingleSet { get; set; } = false;

        public DateTime DateTime { get; set; } = DateTime.Now;

        public string? Note { get; set; }
        public int TotalWeight => (int)(Weight * NofRepetitions * NofSets);

    }

    public class Muscle
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Importance { get; set; }

        public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
    }

    public class ExerciseMuscle
    {
        public int Id { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        public Exercise? Exercise { get; set; }

        [Required]
        public int MuscleId { get; set; }

        public Muscle? Muscle { get; set; }
    }
}
