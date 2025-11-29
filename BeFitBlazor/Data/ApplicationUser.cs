using BeFitBlazor.Models;
using Microsoft.AspNetCore.Identity;

namespace BeFitBlazor.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for user's training sessions
        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }

}
