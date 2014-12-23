using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Services.Books.Models;

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

        public async Task<IList<IsbnBook>> Get(IEnumerable<long> isbns)
        {
            const string path = "v1/products/products.json?key={key}&isbn={isbn}";
            const int maxbookIsbnCountInOneRequest = 20;

            //dividing all isbns into list containing not more than maxbookIsbnCountInOneRequest elements
            var dividedIsbns = isbns.Select((isbn, index) => new { isbn, index })
                .GroupBy(x => x.index / maxbookIsbnCountInOneRequest)
                .Select(groupped => groupped.Select(z => z.isbn)).ToList();

            var tasks = dividedIsbns.Select(isbnList => Task.Run(() => Get(path, isbnList)));

            var books = await Task.WhenAll(tasks);

            return books.SelectMany(isbnBooks => isbnBooks).ToList();
        }

        private async Task<IList<IsbnBook>> Get(string path, IEnumerable<long> isbns)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            string actualPath = path.Replace("{isbn}", string.Join(",", isbns.Select(isbn => isbn.ToString(CultureInfo.InvariantCulture))));

            var request = new RestRequest(actualPath, Method.GET);
            request.AddUrlSegment("key", SaxoKey);

            IRestResponse response = await _client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            var books = JObject.Parse(response.Content)["products"].ToObject<IsbnBook[]>();

            return books.Distinct(new IsbnBookComparer()).ToList();
        }
    }
}
