using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Services.Books
{
    public interface IBookService
    {
        Task<bool> ReadAsync(long isbn, bool read);
        void AddAsync(IList<Book> books);
        Task<IList<Book>> GetAsync(IEnumerable<long> isbns);
    }
}