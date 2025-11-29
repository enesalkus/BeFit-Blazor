using System.ComponentModel.DataAnnotations;

namespace BeFitBlazor.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Egzersiz adı zorunludur")]
        [StringLength(100, ErrorMessage = "Egzersiz adı en fazla 100 karakter olabilir")]
        [Display(Name = "Egzersiz Adı", Description = "Yapılacak egzersizin adı (örn: Bench Press, Squat)")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        [Display(Name = "Açıklama", Description = "Egzersiz hakkında kısa bilgi")]
        public string? Description { get; set; }
    }
}
