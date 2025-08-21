using System.ComponentModel.DataAnnotations;


namespace GymLogger
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public MuscleGroup[]? MuscleGroup { get; set; }
    }
    public class WorkoutEntry
    {
        public int Id { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        public Exercise? Exercise { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Range(0, 500)]
        public double Weight { get; set; }

        [Range(1, 20)]
        public int Sets { get; set; }

        [Range(1, 100)]
        public int Reps { get; set; }
    }
    public class MuscleGroup
    {

    }
    public class Muscle : MuscleGroup
    {

    }
}
