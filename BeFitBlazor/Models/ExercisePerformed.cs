using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class ExercisePerformed
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; } = null!;

        [Required]
        [Display(Name = "Exercise Type")]
        public int ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; } = null!;

        [Required(ErrorMessage = "Load is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Load must be 0 or greater")]
        [Display(Name = "Load (kg)", Description = "Weight used")]
        public double Load { get; set; }

        [Required(ErrorMessage = "Set count is required")]
        [Range(1, 100, ErrorMessage = "Set count must be between 1 and 100")]
        [Display(Name = "Sets", Description = "Number of sets performed")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Repetition count is required")]
        [Range(1, 1000, ErrorMessage = "Repetition count must be between 1 and 1000")]
        [Display(Name = "Repetitions", Description = "Number of repetitions per set")]
        public int Repetitions { get; set; }
    }
}
