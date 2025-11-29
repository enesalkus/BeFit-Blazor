using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Exercise name is required")]
        [StringLength(100, ErrorMessage = "Exercise name cannot exceed 100 characters")]
        [Display(Name = "Exercise Name", Description = "Name of the exercise (e.g., Bench Press, Squat)")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description", Description = "Short information about the exercise")]
        public string? Description { get; set; }
    }
}
