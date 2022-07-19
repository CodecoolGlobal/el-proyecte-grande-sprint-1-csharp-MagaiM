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

            // Look for any user.
            if (!context.Roles.Any())
            {
                var roles = new UserRole[]
                {
                    new UserRole {Name = "User"},
                    new UserRole {Name = "Moderator"},
                    new UserRole {Name = "Admin"},
                    new UserRole {Name = "Editor"},
                };

                foreach (var role in roles)
                {
                    context.Roles.Add(role);
                }

                context.SaveChanges();
            }

            if (context.Users.Any()) return;   // DB has been seeded

            var users = new User[]
            {
            new User{UserName = "Admin", Password = new Password(){UserPassword = "Admin"}, Email = "Admin@Admin.Admin", Reputation = 5000, Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet()},
            new User{UserName = "Editor", Password = new Password(){UserPassword = "Editor"}, Email = "Editor@Editor.Editor", Reputation = 2500, Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Editor").ToHashSet()},
            new User{UserName = "Moderator", Password = new Password(){UserPassword = "Moderator"}, Email = "Moderator@Moderator.Moderator", Reputation = 3000, Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Moderator").ToHashSet()},
            new User{UserName = "User", Password = new Password(){UserPassword = "User"}, Email = "User@User.User", Reputation = 1000, Roles = context.Roles.Where(r => r.Name == "User").ToHashSet()},
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();


        }
    }
}