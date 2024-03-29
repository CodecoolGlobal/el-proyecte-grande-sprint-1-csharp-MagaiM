﻿using System;
using System.Linq;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestBackend
{
    public class DatabaseMockInSetup
    {

        protected ApplicationDbContext _context;

        protected DbContextOptions<ApplicationDbContext> _dbContextOptions;

        protected Guid _guidId;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
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
                _guidId = Guid.NewGuid();
                context.EmailGuid.Add(
                    new EmailGuid
                    {
                        Email = "Admin@Admin.Admin",
                        Guid = _guidId

                    }
                );
                context.SaveChanges();
            }
            _context = new ApplicationDbContext(options);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
