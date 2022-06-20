using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApiHelper _apiController;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApiHelper apiController)
        {
            _logger = logger;
            _apiController = apiController;
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