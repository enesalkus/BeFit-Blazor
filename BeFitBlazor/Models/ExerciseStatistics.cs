namespace BeFitBlazor.Models
{
    public class ExerciseStatistics
    {
        public string ExerciseName { get; set; } = string.Empty;
        public int TimesPerformed { get; set; }
        public int TotalRepetitions { get; set; }
        public double AverageLoad { get; set; }
        public double MaxLoad { get; set; }
    }
}
