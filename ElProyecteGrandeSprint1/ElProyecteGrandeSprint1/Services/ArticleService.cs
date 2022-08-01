using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ElProyecteGrandeSprint1.Services
{
    public class ArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        public ArticleService(ApplicationDbContext context, UserService userservice)
        {
            _context = context;
            _userService = userservice;
        }

        public async Task<List<Article>> GetArticles() => await _context.Articles.Include(a => a.Author).ToListAsync();

        public async Task<string> UploadArticle(NewArticle article)
        {
            Article newArticle = await MakeArticleFromNewArticle(article);
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return JsonSerializer.Serialize("True");

        }

        public async Task<string> ChangeArticle(long id, NewArticle article)
        {
            Article selectedArticle = _context.Articles.First(a => a.ID == id);
            _context.Articles.Remove(selectedArticle);
            Article changedArticle = await MakeArticleFromNewArticle(article);
            _context.Articles.Add(changedArticle);
            await _context.SaveChangesAsync();
            return JsonSerializer.Serialize("True");

        }

        private async Task<Article> MakeArticleFromNewArticle(NewArticle article)
        {
            if (article == null) throw new Exception("Article was null");
            var newArticle = new Article()
            {
                Title = article.Title,
                Description = article.Description,
                Author = await _userService.GetUserByName(article.Author),
                Theme = article.Theme,
                ArticleText = article.ArticleText
            };
            return newArticle;
        }
    }
}
