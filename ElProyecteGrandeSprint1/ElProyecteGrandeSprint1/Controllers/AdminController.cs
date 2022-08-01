using ElProyecteGrandeSprint1.Auth;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Services;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElProyecteGrandeSprint1.Controllers
{

    [AuthorizeWithToken("Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/admin/users")]
        public Task<List<User>> GetUsers()
        {
            return _userService.GetAllUserDataFromDataBase();
        }
    }
}
