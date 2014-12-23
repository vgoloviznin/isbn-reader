using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Data.Models;
using IsbnReader.ViewModels;
using Services.Books;
using Services.Books.Models;

namespace IsbnReader.Controllers
{
    public class HomeController : Controller
    {
        #region Init

        private readonly IBookService _bookService;
        private readonly IIsbnService _isbnService;
        public HomeController(
            IBookService bookService,
            IIsbnService isbnService
            )
        {
            _bookService = bookService;
            _isbnService = isbnService;
        }
        #endregion

        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> Read(long isbn, bool read)
        {
            bool result = await _bookService.ReadAsync(isbn, read);
            return Json(result);
        }

        public async Task<PartialViewResult> Books(string isbns)
        {
            var allIsbns = isbns.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var parsedIsbns = new List<long>(allIsbns.Length);
            var invalidIsbns = new List<string>();
            string errorMessage = string.Empty;

            foreach (var isbn in allIsbns)
            {
                long parsedIsbn;

                if (long.TryParse(isbn, out parsedIsbn))
                {
                    parsedIsbns.Add(parsedIsbn);
                }
                else
                {
                    invalidIsbns.Add(isbn);
                }
            }

            if (!parsedIsbns.Any())
            {
                return PartialView("_Books", new BooksViewModel{ErrorMessage = "No valid ISBNs were found"});
            }

            var existingTask = _bookService.GetAsync(parsedIsbns);

            IList<IsbnBook> bookInformation = new List<IsbnBook>();
            IList<Book> existing = new List<Book>();
            
            try
            {
                //getting all book information
                bookInformation = await _isbnService.Get(parsedIsbns);

                existing = await existingTask;

                var nonexistingIsbns = parsedIsbns.Except(existing.Select(book => book.Isbn)).Select(isbn => (isbn)).ToList();

                if (nonexistingIsbns.Any())
                {
                    var newBooks = bookInformation.Where(w => nonexistingIsbns.Contains(w.Isbn))
                    .Select(info => new Book { IsRead = false, Isbn = info.Isbn }).ToList();

                    await _bookService.AddAsync(newBooks);
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            var books =(from bookInfo in bookInformation
                        join exist in existing on bookInfo.Isbn equals exist.Isbn into existingBooks
                        from existingBook in existingBooks.DefaultIfEmpty()
                        select new BookViewModel
                        {
                            Authors = bookInfo.Authors,
                            ImageUrl = bookInfo.ImageUrl,
                            Isbn = bookInfo.Isbn,
                            Title = bookInfo.Title,
                            Url = bookInfo.Url,
                            IsRead = existingBook != null && existingBook.IsRead
                        }).ToList();

            var model = new BooksViewModel
            {
                Books = books,
                ErrorMessage = errorMessage
            }; 

            return PartialView("_Books", model);
        }
    }
}