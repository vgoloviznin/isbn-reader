using Newtonsoft.Json;

namespace Services.Books
{
    public class IsbnBook
    {
        [JsonProperty("isbn13")]
        public long Isbn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}
