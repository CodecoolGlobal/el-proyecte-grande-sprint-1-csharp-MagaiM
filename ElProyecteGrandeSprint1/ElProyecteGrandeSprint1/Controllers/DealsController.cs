using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Nodes;
using ElProyecteGrandeSprint1.Auth;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly ApiService _apiHelper;
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger, ApiService apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        [AuthorizeWithToken("User")]
        [HttpGet] 
        public async Task<string> GetDeals(string? sortBy = "Deal Rating", int pageSize = 60, int desc = 0, string? storeId="")
        {
            Uri requestUri;
            if (storeId == "" || storeId == null)
            {
                requestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={pageSize}&desc={desc}&sortBy={sortBy}");
            }
            else
            {
                requestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={pageSize}&desc={desc}&sortBy={sortBy}&storeID={storeId}");
            }
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
               
                RequestUri = requestUri
        };
            var deals = await _apiHelper.GetDeals(request);
            return deals;
        }
    }
}

