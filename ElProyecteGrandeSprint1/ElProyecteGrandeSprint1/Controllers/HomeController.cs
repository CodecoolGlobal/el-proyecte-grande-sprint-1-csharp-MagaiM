using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ApiController _apiController = new ApiController();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> GetRecentFreeToPlayGames()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://free-to-play-games-database.p.rapidapi.com/api/games?sort-by=release-date"),
                Headers =
                {
                    { "X-RapidAPI-Host", "free-to-play-games-database.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "44c0067568mshcd96a9adc6db887p13ebcfjsnc1d0ab0bf882" },
                },
            };
            return _apiController.GetDataFromApi<FreeGame>(request);
        }
    }
}