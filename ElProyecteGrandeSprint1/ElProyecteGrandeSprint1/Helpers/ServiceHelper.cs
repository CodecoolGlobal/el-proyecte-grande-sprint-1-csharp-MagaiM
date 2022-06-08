using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Helpers
{
    public class ServiceHelper
    {
        public async Task<string> GetJsonStringFromApi(HttpRequestMessage request)
        {
            var client = new HttpClient();
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public List<T>? DeserializeData<T>(string data)
        {
            return JsonConvert.DeserializeObject<List<T>>(data);
        }

        public string SerializeData<T>(List<T> deserializedData)
        {
            return System.Text.Json.JsonSerializer.Serialize<List<T>>(deserializedData);
        }

        public List<Deal> MakeDealsObject(List<Deal>? deserializedData)
        {
            List<Deal> newListDeal = new List<Deal>();
            foreach (var element in deserializedData)
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

            return newListDeal;
        }
    }
}
