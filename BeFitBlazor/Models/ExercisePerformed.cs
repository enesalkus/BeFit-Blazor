using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class ExercisePerformed
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Antrenman Seansı")]
        public int TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; } = null!;

        [Required]
        [Display(Name = "Egzersiz Tipi")]
        public int ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; } = null!;

        [Required(ErrorMessage = "Ağırlık zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Ağırlık 0 veya daha büyük olmalıdır")]
        [Display(Name = "Ağırlık (kg)", Description = "Kullanılan ağırlık miktarı")]
        public double Load { get; set; }

        [Required(ErrorMessage = "Set sayısı zorunludur")]
        [Range(1, 100, ErrorMessage = "Set sayısı 1 ile 100 arasında olmalıdır")]
        [Display(Name = "Set Sayısı", Description = "Yapılan set sayısı")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Tekrar sayısı zorunludur")]
        [Range(1, 1000, ErrorMessage = "Tekrar sayısı 1 ile 1000 arasında olmalıdır")]
        [Display(Name = "Tekrar Sayısı", Description = "Her sette yapılan tekrar sayısı")]
        public int Repetitions { get; set; }
    }
}
