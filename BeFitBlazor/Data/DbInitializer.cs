using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeFitBlazor.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BeFitBlazor.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Rolleri oluştur
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Admin kullanıcısı oluştur
            var adminEmail = "admin@befit.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Normal kullanıcı oluştur
            var userEmail = "user@befit.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);
            
            if (normalUser == null)
            {
                normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(normalUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
            // Veri ekleme işlemleri için DbContext'i al
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Egzersiz Tiplerini Ekle (İngilizce)
            if (!context.ExerciseTypes.Any())
            {
                var exerciseTypes = new List<ExerciseType>
                {
                    new ExerciseType { Name = "Bench Press", Description = "Chest exercise" },
                    new ExerciseType { Name = "Squat", Description = "Leg exercise" },
                    new ExerciseType { Name = "Deadlift", Description = "Back and leg exercise" },
                    new ExerciseType { Name = "Overhead Press", Description = "Shoulder exercise" },
                    new ExerciseType { Name = "Barbell Row", Description = "Back exercise" },
                    new ExerciseType { Name = "Pull Up", Description = "Back exercise" },
                    new ExerciseType { Name = "Dips", Description = "Triceps and chest exercise" },
                    new ExerciseType { Name = "Lunge", Description = "Leg exercise" },
                    new ExerciseType { Name = "Leg Press", Description = "Leg exercise" },
                    new ExerciseType { Name = "Calf Raise", Description = "Calf exercise" }
                };
                context.ExerciseTypes.AddRange(exerciseTypes);
                await context.SaveChangesAsync();
            }


            // user@befit.com için örnek antrenman verileri ekle (5 adet)
            if (normalUser != null)
            {
                await SeedUserTrainingData(context, normalUser, 5);
            }

            // admin@befit.com için örnek antrenman verileri ekle (7 adet)
            if (adminUser != null)
            {
                await SeedUserTrainingData(context, adminUser, 7);
            }
        }

        private static async Task SeedUserTrainingData(ApplicationDbContext context, ApplicationUser user, int count)
        {
            // Kullanıcının hiç antrenmanı yoksa ekle
            if (!context.TrainingSessions.Any(t => t.UserId == user.Id))
            {
                var exerciseTypes = await context.ExerciseTypes.ToListAsync();
                if (!exerciseTypes.Any()) return;

                var random = new Random();
                var startDate = DateTime.Now.AddDays(-30);

                for (int i = 0; i < count; i++)
                {
                    // Her antrenman arasına 2-3 gün koy
                    startDate = startDate.AddDays(random.Next(2, 4));
                    if (startDate > DateTime.Now) break;

                    var session = new TrainingSession
                    {
                        UserId = user.Id,
                        StartTime = startDate.Date.AddHours(18),
                        EndTime = startDate.Date.AddHours(19).AddMinutes(30)
                    };

                    context.TrainingSessions.Add(session);
                    await context.SaveChangesAsync();

                    int exerciseCount = random.Next(3, 6);
                    var selectedTypes = exerciseTypes.OrderBy(x => random.Next()).Take(exerciseCount).ToList();

                    foreach (var type in selectedTypes)
                    {
                        var performed = new ExercisePerformed
                        {
                            TrainingSessionId = session.Id,
                            ExerciseTypeId = type.Id,
                            Sets = random.Next(3, 5),
                            Repetitions = random.Next(8, 13),
                            Load = random.Next(20, 100)
                        };
                        context.ExercisePerformeds.Add(performed);
                    }
                }
                await context.SaveChangesAsync();
            }
    }
    }
}
