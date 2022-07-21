using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElProyecteGrandeSprint1.Controllers
{
    [EnableCors]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;


        public UserController(ApplicationDbContext context)
        {
            _context = context;
 
        }
        [HttpGet]
        public Task<List<User>> GetAllUserData()
        {
            return _context.GetAllUserDataFromDataBase();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserData(int id)
        {
            return await _context.GetUserDataFromDataBase(id);
        }

        [HttpGet("/name/{name}")]
        public async Task<User> GetUserData(string name)
        {
            return await _context.GetUserByName(name);
        }

        [HttpPost("/{email}")]
        public async Task<bool> ValidateEmail(string email)
        {
            return await _context.ValidateEmailForPassword(email);
        }

        [HttpPost("/login")]
        public async Task<string> Login(LoginUser user)
        {
            return await _context.Login(user);
        }

        [HttpPost("/register")]
        public async Task<string> Register([FromBody] RegisterUser user)
        {
            return await _context.RegisterUser(user);
        }

        [HttpPut("{id}")]
        public Task<string> ChangeUserData(int id, [FromBody] RegisterUser user)
        {
            return _context.ChangeUserProfile(id, user, Guid.Empty);
        }

        [HttpDelete("{id}")]
        public Task<string> DeleteUserProfile(int id)
        {
            return _context.DeleteUser(id);
        }

        [HttpPost("/send")]
        public void SendEmail([FromBody] RegisterUser user)
        { 
            Guid guid = Guid.NewGuid();
            _context.SendForgotPasswordEmail(user.Email, guid);
        }

        [HttpPost("/password/{emailId}")]
        public Task<string> ChangePassword(Guid emailId, [FromBody] RegisterUser user)
        {
            var email =_context.getEmailFromGuid(emailId);
            long userId = _context.GetUserByEmail(email.Email).Result.ID;
            var result = _context.ChangeUserProfile(userId, user, email.Guid);
            if (result.Result == "\"Your profile was Changed successfully\"")
            {
                _context.SendSuccessfulPasswordChangeEmail(emailId);
                return result;
            }
            return result;
        }

    }
}
