using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
namespace ElProyecteGrandeSprint1.Controllers
{
    public class HomeController : Controller
    {
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
            string apiUrl = $"https://www.cheapshark.com/api/1.0/deals?pageSize={pageSize}";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    List<Deal> deserialisation = JsonConvert.DeserializeObject<List<Deal>>(data);
                    List<Deal> newListDeal = new List<Deal>();
                    foreach (var element in deserialisation)
                    {
                        newListDeal.Add(new Deal
                        {
                            StoreName = ((Stores)Int32.Parse(element.StoreName)).ToString(),
                            Title = element.Title,
                            SalePrice = element.SalePrice,
                            NormalPrice = element.NormalPrice,
                            IsOnSale = element.IsOnSale,
                            MetacriticScore = element.MetacriticScore,
                            SteamRatingCount = element.SteamRatingCount,
                            SteamRatingPercent  = element.SteamRatingPercent,
                            SteamRatingText = element.SteamRatingText,
                            DealRating = element.DealRating,
                            Image = element.Image,

                        });
                    }
                    string newdata = System.Text.Json.JsonSerializer.Serialize<List<Deal>>(newListDeal);
                    return newdata;

                }


            }

            return null;
        }

        public async Task<string> RecentFreeToPlayGames()
        {
            var client = new HttpClient();
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
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<FreeGame> deserialisation = JsonConvert.DeserializeObject<List<FreeGame>>(body);
                string newdata = System.Text.Json.JsonSerializer.Serialize<List<FreeGame>>(deserialisation);
                return newdata;
            }

            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}