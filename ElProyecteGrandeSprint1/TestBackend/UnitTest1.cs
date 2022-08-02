using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using ElProyecteGrandeSprint1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace TestBackend;

public class Tests
{
    private byte[] secret = Encoding.ASCII.GetBytes("MY_SECRET_KEY_dasmd.-dDUNJUOFAOD");

    private ApplicationDbContext _context;
    private readonly ApplicationDbContextHelper _contextHelper = new();
    private readonly EmailSender _emailSender = new();

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDatabaseForUnitTest")
            .Options;
        using (var context = new ApplicationDbContext(options))
        {
            var roles = new[]
            {
                new() {Name = "User"},
                new UserRole {Name = "Moderator"},
                new UserRole {Name = "Admin"},
                new UserRole {Name = "Editor"}
            };

            foreach (var role in roles) context.Roles.Add(role);

            context.SaveChanges();
            var users = new[]
            {
                new()
                {
                    UserName = "Admin", Password = new Password {UserPassword = "Admin"},
                    Email = "Admin@Admin.Admin", Reputation = 5000,
                    Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet()
                },
                new User
                {
                    UserName = "Editor", Password = new Password {UserPassword = "Editor"},
                    Email = "Editor@Editor.Editor", Reputation = 2500,
                    Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Editor").ToHashSet()
                },
                new User
                {
                    UserName = "Moderator", Password = new Password {UserPassword = "Moderator"},
                    Email = "Moderator@Moderator.Moderator", Reputation = 3000,
                    Roles = context.Roles.Where(r => r.Name == "User" || r.Name == "Moderator").ToHashSet()
                },
                new User
                {
                    UserName = "User", Password = new Password {UserPassword = "User"}, Email = "User@User.User",
                    Reputation = 1000, Roles = context.Roles.Where(r => r.Name == "User").ToHashSet()
                }
            };

            foreach (var s in users) context.Users.Add(s);

            var loremIpsum = new LoremIpsum();


            if (context.Articles.Any()) return; // DB has been seeded
            var article = new[]
            {
                new()
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
                }
            };

            foreach (var s in article) context.Articles.Add(s);

            context.SaveChanges();
        }
        _context = new ApplicationDbContext(options);

    }

    [Test]
    public void TestGetUsernameMethodInApplicationDbContext()
    {

            var service = new UserService(_context, _contextHelper, _emailSender);
            var admin = service.GetUserByName("Admin").Result;
            var newadmin = _context.Users.Include(u => u.Roles).ToListAsync().Result
                .First(x => x.UserName == "Admin");
            Assert.AreSame(newadmin, admin);
        
    }

    [Test]
    public void TestApplicationDbContextHelperValidateEmailMethod()
    {
        Assert.IsFalse(_contextHelper.ValidateEmail("Moderator@Moderator.Moderator", _context.Users));
        Assert.IsTrue(_contextHelper.ValidateEmail("Moderator@Moderator.Mderator", _context.Users));
    }

    [Test]
    public void TestApplicationDbContextHelperValidateUsernameMethod()
    {
        Assert.IsFalse(_contextHelper.ValidateUsername("Moderator", _context.Users));
        Assert.IsTrue(_contextHelper.ValidateUsername("Moderator@Moderator.Mderator", _context.Users));
    }

    [TestCase("Nincsen", ExpectedResult = "accepted")]
    [TestCase("nin", ExpectedResult = "The password must be longer than 5 characters")]
    [TestCase("nincsen", ExpectedResult = "The password must contain minimum 1 upper letter")]
    public string TestApplicationDbContextHelperValidatePasswordMethod(string password)
    {
        RegisterUser newUserWithCorrectPassword = new RegisterUser
        {
            Password = password,
        };
        return _contextHelper.ValidatePassword(newUserWithCorrectPassword);
    }
    [TestCase("Moderator@Moderator.Moderator","Moderator",3, ExpectedResult = true)]
    [TestCase("Admin@Admin.Admin", "Admin", 1, ExpectedResult = true)]
    [TestCase("Editor@Editor.Editor", "Editor", 2, ExpectedResult = true)]
    public bool TestApplicationDbContextHelperJWTGeneratorMethod(string email, string name, long id)
    {
        var generatedJWT = _contextHelper.JWTGenerator(email, name, id);
        var decodedJWT = DecodeJWT(generatedJWT);
        var UserName = decodedJWT.Claims.ElementAt(1).Value;
        var Userid = decodedJWT.Claims.ElementAt(2).Value;
        var Useremail = decodedJWT.Claims.ElementAt(3).Value;
        Dictionary<string, object> expectedHeader = new Dictionary<string, object>()
        {
            {"alg", "HS256"},
            {"typ", "JWT"}
        };
        var header = decodedJWT.Header;
        return email == Useremail && name == UserName && id.ToString() == Userid &&
        header["alg"].Equals(expectedHeader["alg"]) && header["typ"].Equals(expectedHeader["typ"]);
    }
    private JwtSecurityToken DecodeJWT(string JWT)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(JWT);
    }

    [Test]
    public void AdminControllerTest()
    {
        Assert.Pass();
    }
}