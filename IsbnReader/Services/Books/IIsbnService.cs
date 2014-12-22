using System.Threading.Tasks;

namespace Services.Books
{
    public interface IIsbnService
    {
        Task<IsbnBook> Get(long isbn);
    }
}