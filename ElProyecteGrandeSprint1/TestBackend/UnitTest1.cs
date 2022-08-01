using System.Linq;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestBackend
{
    public class Tests
    {
        private ApplicationDbContext _context;
        private DbContextOptions<ApplicationDbContext> _options;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseForUnitTest")
                .Options;
            using (var context = new ApplicationDbContext(options))
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
                var users = new User[]
                {
                    new User
                    {
                        UserName = "Admin", Password = new Password() {UserPassword = "Admin"},
                        Email = "Admin@Admin.Admin", Reputation = 5000,
                        Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet()
                    },
                    new User
                    {
                        UserName = "Editor", Password = new Password() {UserPassword = "Editor"},
                        Email = "Editor@Editor.Editor", Reputation = 2500,
                        Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Editor").ToHashSet()
                    },
                    new User
                    {
                        UserName = "Moderator", Password = new Password() {UserPassword = "Moderator"},
                        Email = "Moderator@Moderator.Moderator", Reputation = 3000,
                        Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Moderator").ToHashSet()
                    },
                    new User
                    {
                        UserName = "User", Password = new Password() {UserPassword = "User"}, Email = "User@User.User",
                        Reputation = 1000, Roles = context.Roles.Where(r => r.Name == "User").ToHashSet()
                    },
                };

                foreach (User s in users)
                {
                    context.Users.Add(s);
                }

                LoremIpsum loremIpsum = new LoremIpsum();


                if (context.Articles.Any()) return; // DB has been seeded
                var article = new Article[]
                {
                    new Article
                    {
                        Title = "This is a text", Description = "Auto Generated Article", Author = users[0],
                        Theme = Theme.AMA, ArticleText = loremIpsum.loremIpsum
                    },
                    new Article
                    {
                        Title = "This is a text", Description = "Auto Generated Article", Author = users[0],
                        Theme = Theme.Blog, ArticleText = loremIpsum.loremIpsum
                    },
                    new Article
                    {
                        Title = "This is a text", Description = "Auto Generated Article", Author = users[0],
                        Theme = Theme.Game, ArticleText = loremIpsum.loremIpsum
                    },
                    new Article
                    {
                        Title = "This is a text", Description = "Auto Generated Article", Author = users[0],
                        Theme = Theme.Worldnews, ArticleText = loremIpsum.loremIpsum
                    },
                };

                foreach (Article s in article)
                {
                    context.Articles.Add(s);
                }

                context.SaveChanges();
            }

            _options = options;
        }

        [Test]
        public void Test1()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var admin = context.GetUserByName("Admin").Result;
                var newadmin = context.Users.Include(u => u.Roles).ToListAsync().Result
                    .First(x => x.UserName == "Admin");
                Assert.AreSame(newadmin, admin);
            }
        }

        [Test]
        public void Test2()
        {
            Assert.Pass();
        }
    }
}