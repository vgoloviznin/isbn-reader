using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsbnReader.ViewModels
{
    public class BookViewModel
    {
        public long Isbn { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; }
    }
}