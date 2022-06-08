using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Nodes;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealsController : Controller
    {
        private readonly ApiController _apiController = new ApiController();
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{pageSize}")]
        public async Task<string> GetDeals(int pageSize)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={pageSize}")
            };
            var deals = await _apiController.GetDeals(request);
            return deals;
        }
    }
}

