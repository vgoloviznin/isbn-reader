using System.ComponentModel.DataAnnotations;

namespace IsbnReader.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Books = new BooksViewModel();
        }
        public BooksViewModel Books { get; set; }

        [Required]
        public string Isbns { get; set; }
    }
}