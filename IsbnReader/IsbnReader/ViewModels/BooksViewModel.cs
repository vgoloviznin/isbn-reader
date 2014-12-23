using System.Collections.Generic;

namespace IsbnReader.ViewModels
{
    public class BooksViewModel
    {
        public BooksViewModel()
        {
            Books = new List<BookViewModel>();
        }
        public IList<BookViewModel> Books { get; set; }
        public string ErrorMessage { get; set; }
    }
}