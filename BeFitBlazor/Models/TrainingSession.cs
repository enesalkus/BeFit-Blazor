using BeFitBlazor.Data;
using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        // User relationship
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        [Required(ErrorMessage = "Başlangıç zamanı zorunludur")]
        [Display(Name = "Başlangıç Zamanı", Description = "Antrenman seansının başlangıç tarihi ve saati")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Bitiş zamanı zorunludur")]
        [Display(Name = "Bitiş Zamanı", Description = "Antrenman seansının bitiş tarihi ve saati")]
        public DateTime EndTime { get; set; }

        // Basit validasyon: başlangıç zamanı bitiş zamanından önce olmalı
        [Display(Name = "Geçerli Mi?")]
        public bool IsValid => StartTime <= EndTime;

        public ICollection<ExercisePerformed> Exercises { get; set; } = new List<ExercisePerformed>();
    }
}
