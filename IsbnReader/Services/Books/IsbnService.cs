using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Services.Serializers;

namespace Services.Books
{
    public class IsbnService : IIsbnService
    {
        private readonly RestClient _client;
        private static readonly string SaxoKey = ConfigurationManager.AppSettings["SaxoKey"];

        public IsbnService()
        {
            _client = new RestClient("http://api.saxo.com/");

        }

        public async Task<IsbnBook> Get(long isbn)
        {
            const string path = "v1/products/products.json?key={key}&isbn={isbn}";
            var cancellationTokenSource = new CancellationTokenSource();

            var request = new RestRequest(path, Method.GET)
            {
                JsonSerializer = new CustomJsonSerializer()
            };
            request.AddUrlSegment("key", SaxoKey);
            request.AddUrlSegment("isbn", isbn.ToString(CultureInfo.InvariantCulture));

            IRestResponse response = await _client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            var books = JObject.Parse(response.Content)["products"].ToObject<IsbnBook[]>();

            return books.Any() ? books[0] : null;
        }
    }
}
