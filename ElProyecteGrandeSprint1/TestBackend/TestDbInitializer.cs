using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestBackend
{
    public class TestDbInitializer : DatabaseMockInSetup
    {
        private ApplicationDbContext _context2;

        [Test]
        public void TestTestDbInitializer()
        {
            DbContextOptions<ApplicationDbContext> options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryDatabaseForUnitTest2")
                .Options;
            using (var context = new ApplicationDbContext(options2))
            {
                DbInitializer.Initialize(context);
            }
            _context2 = new ApplicationDbContext(options2);
            Assert.AreEqual(_context.Articles.Count(), _context2.Articles.Count());
            Assert.AreEqual(_context.Users.Count(), _context2.Users.Count());
            Assert.AreEqual(_context.JWTTokens, _context2.JWTTokens);
            Assert.AreEqual(_context.Roles.Count(), _context2.Roles.Count());
        }
    }
}
