using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Books.Models;

namespace Services.Books
{
    public interface IIsbnService
    {
        Task<IList<IsbnBook>> Get(IEnumerable<long> isbns);
    }
}