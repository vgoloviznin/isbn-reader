using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Services.Books
{
    public interface IBookService
    {
        Task<bool> ReadAsync(long isbn, bool read);
        Task<bool> AddAsync(IList<Book> books);
        Task<IList<Book>> GetAsync(IEnumerable<long> isbns);
        Task<IList<Book>> GetAsync();
    }
}