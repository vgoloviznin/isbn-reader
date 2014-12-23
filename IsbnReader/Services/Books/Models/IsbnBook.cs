using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.Books.Models
{
    public class IsbnBook
    {
        [JsonProperty("isbn13")]
        public long Isbn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("contributors")]
        public IList<BookAuthor> Authors { get; set; }
    }
}
