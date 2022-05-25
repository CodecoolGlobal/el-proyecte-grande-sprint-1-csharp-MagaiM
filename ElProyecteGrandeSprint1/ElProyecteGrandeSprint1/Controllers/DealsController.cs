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
            
            var deals = GetDeals();
            Console.WriteLine((Stores)7);
            ViewBag.Deals = deals.Result;
            return View();
        }

        public static async Task<List<Deal>> GetDeals()
        {
            var client = new HttpClient();
            var body = "";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://www.cheapshark.com/api/1.0/deals"),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(body);
            }

            List<Deal> deserialisation = JsonConvert.DeserializeObject<List<Deal>>(body);
            return deserialisation;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

