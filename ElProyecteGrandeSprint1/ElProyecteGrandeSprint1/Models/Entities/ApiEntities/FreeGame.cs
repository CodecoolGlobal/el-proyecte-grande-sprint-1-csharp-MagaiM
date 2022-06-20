using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Models.Entities.ApiEntities;

public class FreeGame
{
    [JsonProperty("id")]
    public int id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("thumbnail")]
    public string Thumbnail { get; set; }
    [JsonProperty("short_description")]
    public string ShortDescription { get; set; }
    [JsonProperty("game_url")]
    public string GameUrl { get; set; }
    [JsonProperty("genre")]
    public string Genre { get; set; }
    [JsonProperty("platform")]
    public string Platform { get; set; }
    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; }
}