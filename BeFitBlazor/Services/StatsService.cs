using BeFitBlazor.Data;
using BeFitBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace BeFitBlazor.Services
{
    public class StatsService
    {
        private readonly ApplicationDbContext _context;

        public StatsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseStatistics>> GetExerciseStatisticsAsync(string userId, int weeks = 4)
        {
            var startDate = DateTime.Now.AddDays(-7 * weeks);

            var statistics = await _context.ExercisePerformeds
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.TrainingSession.StartTime >= startDate && e.TrainingSession.UserId == userId)
                .GroupBy(e => new { e.ExerciseTypeId, e.ExerciseType.Name })
                .Select(g => new ExerciseStatistics
                {
                    ExerciseName = g.Key.Name,
                    TimesPerformed = g.Count(),
                    TotalRepetitions = g.Sum(e => e.Sets * e.Repetitions),
                    AverageLoad = g.Average(e => e.Load),
                    MaxLoad = g.Max(e => e.Load)
                })
                .OrderByDescending(s => s.TimesPerformed)
                .ToListAsync();

            return statistics;
        }
    }
}
