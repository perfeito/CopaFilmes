using Newtonsoft.Json;

namespace MovieCup.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("titulo")]
        public string Title { get; set; }
        [JsonProperty("ano")]
        public int Year { get; set; }
        [JsonProperty("nota")]
        public float Rating { get; set; }
    }
}
