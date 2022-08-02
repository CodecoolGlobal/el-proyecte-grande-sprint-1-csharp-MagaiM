using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElProyecteGrandeSprint1.Controllers
{
    [EnableCors]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;


        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserData(int id)
        {
            return await _userService.GetUserDataFromDataBase(id);
        }

        [HttpGet("/name/{name}")]
        public async Task<User> GetUserData(string name)
        {
            return await _userService.GetUserByName(name);
        }

        [HttpPost("/{email}")]
        public async Task<bool> ValidateEmail(string email)
        {
            return await _userService.ValidateEmailForPassword(email);
        }

        [HttpPost("/login")]
        public async Task<string> Login(LoginUser user)
        {
            return await _userService.Login(user);
        }

        [HttpPost("/register")]
        public async Task<string> Register([FromBody] RegisterUser user)
        {
            return await _userService.RegisterUser(user);
        }

        [HttpPut("{id}")]
        public Task<string> ChangeUserData(int id, [FromBody] RegisterUser user)
        {
            return _userService.ChangeUserProfile(id, user, Guid.Empty);
        }

        [HttpDelete("{id}")]
        public Task<string> DeleteUserProfile(int id)
        {
            return _userService.DeleteUser(id);
        }

        [HttpPost("/send")]
        public void SendEmail([FromBody] RegisterUser user)
        { 
            Guid guid = Guid.NewGuid();
            _userService.SendForgotPasswordEmail(user.Email, guid);
        }

        [HttpPost("/password/{emailId}")]
        public Task<string> ChangePassword(Guid emailId, [FromBody] RegisterUser user)
        {
            var email = _userService.
                GetEmailFromGuid(emailId);
            if (email == null)
            {
                return Task.FromResult("No");
            }
            long userId = _userService.GetUserByEmail(email.Email).Result.ID;
            var result = _userService.ChangeUserProfile(userId, user, email.Guid);
            if (result.Result == "\"Your profile was Changed successfully\"")
            {
                _userService.SendSuccessfulPasswordChangeEmail(emailId);
                return result;
            }
            return result;
        }

    }
}
