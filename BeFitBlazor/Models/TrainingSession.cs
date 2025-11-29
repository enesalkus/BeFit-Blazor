using BeFitBlazor.Data;
using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }


        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        [Required(ErrorMessage = "Start time is required")]
        [Display(Name = "Start Time", Description = "Start date and time of the training session")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [Display(Name = "End Time", Description = "End date and time of the training session")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Is Valid?")]
        public bool IsValid => StartTime <= EndTime;

        public ICollection<ExercisePerformed> Exercises { get; set; } = new List<ExercisePerformed>();
    }
}
