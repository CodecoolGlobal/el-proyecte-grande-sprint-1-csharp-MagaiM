using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace TestBackend
{
    public class TestUserController : DatabaseMockInSetup
    {
        private UserController _userController;
        private EmailSender _mockedEmailSender;
        private UserService _service;
        private UserServiceHelper _serviceHelper = new UserServiceHelper();

        [SetUp]
        public void Setup()
        {
            var mockedEmailSender = NSubstitute.Substitute.For<EmailSender>();
            _mockedEmailSender = mockedEmailSender;
            _service = new UserService(_context, _serviceHelper, _mockedEmailSender);
            _userController = new UserController(_service);
        }

        [TestCase("Admin123", "Admin123", "Admin@Admin@Admin", ExpectedResult = "\"Your profile was Changed successfully\"")]
        public string TestChangePassword(string user, string password, string email)
        {
            RegisterUser newUser = new RegisterUser()
            {
                UserName = user,
                Password = password,
                Email = email
            };
            return _userController.ChangePassword(_guidId, newUser).Result;
        }

        [Test]
        public void TestChangePasswordBadEnding()
        {
            RegisterUser newUser = new RegisterUser()
            {
                UserName = "asd",
                Password = "asd",
                Email = "asd"
            };
            Assert.AreEqual("No", _userController.ChangePassword(new Guid(), newUser).Result);
        }
    }
}
