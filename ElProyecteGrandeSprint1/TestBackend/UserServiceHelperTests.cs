using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using NUnit.Framework;

namespace TestBackend
{
    internal class UserServiceHelperTests : DatabaseMockInSetup
    {
        private UserServiceHelper _serviceHelper = new();

        [Test]
        public void TestApplicationDbContextHelperValidateUsernameMethod()
        {
            Assert.IsFalse(_serviceHelper.ValidateUsername("Moderator", _context.Users));
            Assert.IsTrue(_serviceHelper.ValidateUsername("Moderator@Moderator.Mderator", _context.Users));
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
            return _serviceHelper.ValidatePassword(newUserWithCorrectPassword);
        }

        [TestCase("Moderator@Moderator.Moderator", "Moderator", 3, ExpectedResult = true)]
        [TestCase("Admin@Admin.Admin", "Admin", 1, ExpectedResult = true)]
        [TestCase("Editor@Editor.Editor", "Editor", 2, ExpectedResult = true)]
        public bool TestApplicationDbContextHelperJWTGeneratorMethod(string email, string name, long id)
        {
            var generatedJWT = _serviceHelper.JWTGenerator(email, name, id);
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
    }
}
