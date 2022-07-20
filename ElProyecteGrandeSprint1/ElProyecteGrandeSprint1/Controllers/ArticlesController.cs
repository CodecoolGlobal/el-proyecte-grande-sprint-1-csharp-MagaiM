using ElProyecteGrandeSprint1.Auth;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeWithToken("Admin,User")]
    //[Authorize(Roles = "Admin")]
    public class ArticlesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("/articles")]
        public Task<List<Article>> GetArticles()
        {
            return _context.GetArticles();
        }
    }
}
