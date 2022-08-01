using ElProyecteGrandeSprint1.Auth;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Services;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet("/articles")]
        public Task<List<Article>> GetArticles()
        {
            return _articleService.GetArticles();
        }

        [AuthorizeWithToken("Admin,User")]
        [HttpPost("/upload")]
        public async Task<string> UploadArticle([FromBody] NewArticle article)
        {
            return await _articleService.UploadArticle(article);
        }

        [AuthorizeWithToken("Admin,Editor")]
        [HttpPut("/change/{id}")]
        public async Task<string> ChangeArticle([FromBody] NewArticle article, long id)
        {
            return await _articleService.ChangeArticle(id, article);
        }
    }
}