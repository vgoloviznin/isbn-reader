using System.Collections.Generic;

namespace Services.Books.Models
{
    public class IsbnBookComparer : IEqualityComparer<IsbnBook>
    {
        public bool Equals(IsbnBook x, IsbnBook y)
        {
            return x.Isbn == y.Isbn;
        }

        public int GetHashCode(IsbnBook obj)
        {
            return obj.Isbn.GetHashCode();
        }
    }
}
