namespace ElProyecteGrandeSprint1.Models.Entities.ApiEntities
{
    public class News
    {
        //[JsonProperty("title")]
        public string Title { get; set; }

        //[JsonProperty("date")]
        public DateTime Date { get; set; }

        //[JsonProperty("description")]
        public string Description { get; set; }

        //[JsonProperty("image")]
        public string Image { get; set; }

        //[JsonProperty("link")]
        public string Link { get; set; }
    }
}
