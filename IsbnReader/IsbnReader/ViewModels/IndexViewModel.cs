using System.Collections.Generic;
using Data.Models;

namespace IsbnReader.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Books = new List<BookViewModel>();
        }
        public IList<BookViewModel> Books { get; set; }
    }
}