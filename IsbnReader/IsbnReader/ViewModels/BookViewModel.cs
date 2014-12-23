using System.Collections.Generic;
using Services.Books.Models;

namespace IsbnReader.ViewModels
{
    public class BookViewModel
    {

        public long Isbn { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public IList<BookAuthor> Authors { get; set; }

        public bool IsRead { get; set; }
    }
}