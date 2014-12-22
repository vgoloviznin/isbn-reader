using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Data.Models;
using IsbnReader.ViewModels;
using Services.Books;

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

        // GET: Home
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
                return PartialView("_Books", new List<BookViewModel>());
            }

            IEnumerable<Task<IsbnBook>> tasks = parsedIsbns.Select(s => _isbnService.Get(s));

            var existing = (List<Book>)(await _bookService.GetAsync(parsedIsbns));
            var newBooks = parsedIsbns.Except(existing.Select(s => s.Isbn)).Select(s => new Book {Isbn = s, IsRead = false}).ToList();
            _bookService.AddAsync(newBooks);

            existing.AddRange(newBooks);

            IsbnBook[] bookResult = await Task.WhenAll(tasks);

            var model = bookResult.Where(w => w != null)
                .Join(existing, sel => sel.Isbn, inner => inner.Isbn, (isbnBook, book) => new { isbnBook, book })
                .Select(s => new BookViewModel
                {
                    ImageUrl = s.isbnBook.ImageUrl,
                    Title = s.isbnBook.Title,
                    Isbn = s.isbnBook.Isbn,
                    IsRead = s.book.IsRead
                }).ToList();

            return PartialView("_Books", model);
        }
    }
}