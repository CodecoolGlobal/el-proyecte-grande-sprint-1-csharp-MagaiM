using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using ElProyecteGrandeSprint1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace TestBackend;

public class UserServiceTests : DatabaseMockInSetup
{
    private readonly UserServiceHelper _serviceHelper = new();
    private EmailSender _mockedEmailSender; 
    private UserService _service;

    [SetUp]
    public  void Setup()
    {
        var mockedEmailSender = NSubstitute.Substitute.For<EmailSender>();
        _mockedEmailSender = mockedEmailSender;
        _service = new UserService(_context, _serviceHelper, _mockedEmailSender);

    }

    [Test]
    public void TestGetUsernameMethodInApplicationDbContext()
    {
        var admin =  _service.GetUserByName("Admin").Result;
        var adminPassword = new Password { UserPassword = "Admin" };
        Assert.AreSame("Admin", admin.UserName);
        Assert.AreEqual(adminPassword.UserPassword, admin.Password.UserPassword);
        Assert.AreSame("Admin@Admin.Admin", admin.Email);
        Assert.AreEqual(5000, admin.Reputation);
        var rolesForAdmin = _context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet();
        Assert.AreEqual(rolesForAdmin, admin.Roles);

    }
    [Test]
    public void TestGetUserDataFromDataBaseMethodInApplicationDbContext()
    {
        var admin = _service.GetUserDataFromDataBase(1).Result;
        var adminPassword = new Password { UserPassword = "Admin" };
        Assert.AreSame("Admin", admin.UserName);
        Assert.AreEqual(adminPassword.UserPassword, admin.Password.UserPassword);
        Assert.AreSame("Admin@Admin.Admin", admin.Email);
        Assert.AreEqual(5000, admin.Reputation);
        var rolesForAdmin = _context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet();
        Assert.AreEqual(rolesForAdmin, admin.Roles);

    }

    [Test]
    public async Task TestGetAllUserDataFromDataBaseMethodInApplicationDbContext()
    {
        var allUsers = await _service.GetAllUserDataFromDataBase();
        Assert.AreEqual(_context.Users, allUsers);
    }

    [Test]
    public async Task TestGetUserByEmailMethodInApplicationDbContext()
    {
        var userByEmail = await _service.GetUserByEmail("Admin@Admin.Admin");
        Assert.IsNotNull(userByEmail);
        Assert.AreEqual("Admin", userByEmail.UserName);
        var rolesForAdmin = _context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToHashSet();
        Assert.AreEqual(rolesForAdmin, userByEmail.Roles);

    }

    [Test]
    public void TestGetEmailFromGuidMethodInApplicationDbContext()
    {
        var emailGuid = _service.GetEmailFromGuid(_guidId);
        var emailGuid2 = _service.GetEmailFromGuid(Guid.NewGuid());

        Assert.IsNull(emailGuid2);
        Assert.AreEqual("Admin@Admin.Admin", emailGuid.Email);
    }

    [Test]
    public async Task TestDeleteMethodInApplicationDbContext()
    {
        await _service.DeleteUser(1);
        await _service.DeleteUser(5);
        Assert.AreEqual(3, _context.Users.Count());
        Assert.Throws<InvalidOperationException>(() => _service.GetUserByName("Admin"),
            "Sequence contains no matching element");
    }


    [TestCase(1, "Admin", "Admin123", ExpectedResult = "\"Your profile was Changed successfully\"")]
    [TestCase(1, "Admin", "", ExpectedResult = "\"Your profile was not Changed successfully\"")]
    [TestCase(70,"", "", ExpectedResult = "\"Sequence contains no matching element\"")]
    [TestCase(3,"asdasd", "Admin", ExpectedResult = "\"Your profile was Changed successfully\"")]
    public async Task<string> TestGetChangeUserProfileMethodInApplicationDbContext(int id, string username, string password)
    {
        RegisterUser newUserData = new RegisterUser
        {
            UserName = username,
            Password = password,
        };
        return await _service.ChangeUserProfile(id, newUserData, Guid.Empty);
    }

    [Test]
    public void TestSendForgotPasswordEmailMethodInApplicationDbContext()
    {
        Guid NewGuidEditor = Guid.NewGuid();
        Guid NewGuidAdmin = Guid.NewGuid();
        _service.SendForgotPasswordEmail("Editor@Editor.Editor", NewGuidEditor);
        Assert.AreEqual("Editor@Editor.Editor", _service.GetEmailFromGuid(NewGuidEditor).Email);
        Assert.AreEqual("Admin@Admin.Admin", _service.GetEmailFromGuid(_guidId).Email);
        _service.SendForgotPasswordEmail("Admin@Admin.Admin", NewGuidAdmin);
        Assert.AreEqual("Admin@Admin.Admin", _service.GetEmailFromGuid(NewGuidAdmin).Email);
        Assert.AreEqual(null, _service.GetEmailFromGuid(_guidId));
        _mockedEmailSender.Received().SendConfirmationEmail("Admin", "Admin@Admin.Admin", "forgor", NewGuidAdmin);
    }

    [TestCase("Admin@Admin.Admin", ExpectedResult = true)]
    [TestCase("User@Admin.admin", ExpectedResult = false)]
    [TestCase("Admin", ExpectedResult = false)]
    public async Task<bool> TestValidateEmailForPasswordMethodInApplicationDbContext(string email)
    {
        return await _service.ValidateEmailForPassword(email);
    }

    [TestCase("Admin", "Admin", ExpectedResult = true)]
    [TestCase("User", "User", ExpectedResult = true)]
    [TestCase("Admin", "User", ExpectedResult = false)]
    public async Task<bool> TestValidateLoginMethodInApplicationDbContext(string username, string password)
    {
        var loginUser = new LoginUser()
        {
            UserName = username,
            Password = password
        };
        return await _service.ValidateLogin(loginUser);
    }

    [Test]
    public void TestSendSuccessfulPasswordChangeEmailMethodInApplicationDbContext()
    {
        _service.SendSuccessfulPasswordChangeEmail(_guidId);
        _mockedEmailSender.Received().SendConfirmationEmail("Admin", "Admin@Admin.Admin", "success", _guidId);
        Assert.IsNull(_service.GetEmailFromGuid(_guidId));

    }

    [Test]
    public async Task TestLoginMethodWithGoodInputsInApplicationDbContext()
    {
        LoginUser newUserData = new LoginUser
        {
            UserName = "Admin",
            Password = "Admin",
        };
        string response = await _service.Login(newUserData);
        ValidatedUser deserializedResponse = JsonConvert.DeserializeObject<ValidatedUser>(response);
        var rolesForAdmin = _context.Roles.Where(r => r.Name == "User" || r.Name == "Admin").ToList();
        Assert.IsNotNull(deserializedResponse.AccessToken);
        Assert.AreEqual(1, deserializedResponse.Id);
        Assert.AreEqual(rolesForAdmin[0].Name, deserializedResponse.Roles[0]);
        Assert.AreEqual(rolesForAdmin[1].Name, deserializedResponse.Roles[1]);
        Assert.AreEqual(5000, deserializedResponse.Reputation);
        Assert.AreEqual("Admin@Admin.Admin", deserializedResponse.Email);
        Assert.AreEqual(newUserData.UserName, deserializedResponse.UserName);

    }


    [TestCase("Admin", "Admin123", ExpectedResult = "\"false\"")]
    [TestCase("User", "", ExpectedResult = "\"false\"")]
    [TestCase("", "User", ExpectedResult = "\"false\"")]
    [TestCase("", "", ExpectedResult = "\"false\"")]
    [TestCase("asdasd", "Admin", ExpectedResult = "\"false\"")]
    public async Task<string> TestLoginMethodWithbadInputsInApplicationDbContext(string username, string password)
    {
        LoginUser newUserData = new LoginUser
        {
            UserName = username,
            Password = password,
        };
        return await _service.Login(newUserData);

    }

    [TestCase("Something", "Something123", "Something@gmail.com", ExpectedResult = "\"Registered Successfully\"")]
    [TestCase("User", "asdasd", "user@gmail.com", ExpectedResult = "\"This Username is already taken\"")]
    [TestCase("valami", "User", "Admin@Admin.Admin", ExpectedResult = "\"This Email is already in use!\"")]
    [TestCase("bela", "admin", "bela@gmail.com", ExpectedResult = "\"The password must contain minimum 1 upper letter\"")]
    [TestCase("asdasd", "Adm", "asdasd@gmail.com", ExpectedResult = "\"The password must be longer than 5 characters\"")]
    public async Task<string> TestRegisterUserMethodInApplicationDbContext(string username, string password, string email)
    {
        RegisterUser newRegisterUser = new RegisterUser()
        {
            UserName = username,
            Password = password,
            Email = email
        };
        return await _service.RegisterUser(newRegisterUser);
    }

    [TestCase("Something", "Something123", "Something@gmail.com", ExpectedResult = "\"Registered Successfully\"")]
    public async Task<string> TestCheckIfSendConfirmationEmailGetsCalledInRegisterUserMethod(string username, string password, string email)
    {
        RegisterUser newRegisterUser = new RegisterUser()
        {
            UserName = username,
            Password = password,
            Email = email
        };
        var response = await _service.RegisterUser(newRegisterUser);
        _mockedEmailSender.Received().SendConfirmationEmail("Something", "Something@gmail.com", "registration", Guid.Empty);
        return response;
    }
}
