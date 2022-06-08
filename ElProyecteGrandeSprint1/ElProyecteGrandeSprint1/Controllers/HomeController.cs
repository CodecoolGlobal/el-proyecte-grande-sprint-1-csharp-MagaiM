using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
namespace ElProyecteGrandeSprint1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiController _apiController = new ApiController();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<string> GetApi(int pageSize)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={pageSize}")
            };
            var deals = await _apiController.GetDeals(request);
            return deals;
        }

        public async Task<string> RecentFreeToPlayGames()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}