using Newtonsoft.Json;

namespace Services.Books.Models
{
    public class BookAuthor
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
