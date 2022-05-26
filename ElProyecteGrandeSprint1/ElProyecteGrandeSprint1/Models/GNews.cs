using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Models
{
    public class GNews
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        [JsonProperty("url")]
        public string Link { get; set; }

        public string Image { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime Date { get; set; }

        public Source Source { get; set; }

    }
}
