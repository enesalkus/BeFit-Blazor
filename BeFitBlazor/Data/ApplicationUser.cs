using BeFitBlazor.Models;
using Microsoft.AspNetCore.Identity;

namespace BeFitBlazor.Data
{

    public class ApplicationUser : IdentityUser
    {

        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }

}
