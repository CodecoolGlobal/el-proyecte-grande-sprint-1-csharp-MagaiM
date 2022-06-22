using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;

namespace ElProyecteGrandeSprint1
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{UserName = "Admin", Password = new Password(){UserPassword = "Admin"}.UserPassword, Reputation = 5000, Rank = Rank.Admin},
            new User{UserName = "Developer", Password = new Password(){UserPassword = "Developer"}.UserPassword, Reputation = 500000, Rank = Rank.Developer},
            new User{UserName = "Moderator", Password = new Password(){UserPassword = "Moderator"}.UserPassword, Reputation = 5, Rank = Rank.Moderator},
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();
        }
    }
}