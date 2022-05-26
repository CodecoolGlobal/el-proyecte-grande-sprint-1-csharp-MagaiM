using System.Diagnostics;
using System.Text.Json.Nodes;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    public class DealsController : Controller
    {
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
                            SteamRatingPercent = element.SteamRatingPercent,
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

