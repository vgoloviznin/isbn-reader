using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;

namespace Services.Books
{
    public class BookService : IBookService
    {
        #region
        private readonly Context _context;

        public BookService(Context context)
        {
            _context = context;
        }
        #endregion

        public async Task<bool> ReadAsync(long isbn, bool read)
        {
            var book = await _context.Books.FindAsync(isbn);

            if (book == null)
            {
                return false;
            }

            if (book.IsRead == read)
            {
                return true;
            }

            book.IsRead = read;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAsync(IList<Book> books)
        {
            if (!books.Any())
            {
                return await Task.FromResult(false);
            }

            _context.Books.AddRange(books);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<Book>> GetAsync(IEnumerable<long> isbns)
        {
            return await _context.Books.Where(w => isbns.Contains(w.Isbn)).ToListAsync();
        }

        public async Task<IList<Book>> GetAsync()
        {
            return await _context.Books.ToListAsync();
        }
    }
}
