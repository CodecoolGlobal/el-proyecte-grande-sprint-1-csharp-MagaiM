using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestBackend
{
    public class AdminContorlerTest : DatabaseMockInSetup
    {
        private AdminController _adminController;
        private EmailSender _mockedEmailSender;
        private UserService _service;
        private UserServiceHelper _serviceHelper = new UserServiceHelper();

        [SetUp]
        public void Setup()
        {
            var mockedEmailSender = NSubstitute.Substitute.For<EmailSender>();
            _mockedEmailSender = mockedEmailSender;
            _service = new UserService(_context, _serviceHelper, _mockedEmailSender);
            _adminController = new AdminController(_service);
        }

        [Test]
        public void TestGetAllUser()
        {
            Assert.AreEqual(_context.Users, _adminController.GetUsers().Result);
        }
    }
}
