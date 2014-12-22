using System.Data.Entity;
using Data.Models;

namespace Data
{
    public class Context : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
