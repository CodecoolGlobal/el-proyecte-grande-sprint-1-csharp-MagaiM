using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Models.Entities.ApiEntities
{
    public class Deal
    {
        [JsonProperty("storeId")]
        public String StoreName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("salePrice")]
        public float SalePrice { get; set; }
        [JsonProperty("normalPrice")]
        public float NormalPrice { get; set; }
        [JsonProperty("isOnSale")]
        public int IsOnSale { get; set; }
        [JsonProperty("metacriticScore")]
        public int MetacriticScore { get; set; }
        [JsonProperty("steamRatingText")]
        public string SteamRatingText { get; set; }
        [JsonProperty("steamRatingPercent")]
        public int SteamRatingPercent { get; set; }
        [JsonProperty("steamRatingCount")]
        public int SteamRatingCount { get; set; }
        [JsonProperty("dealRating")]
        public float DealRating { get; set; }
        [JsonProperty("thumb")]
        public string Image { get; set; }
    }
}
