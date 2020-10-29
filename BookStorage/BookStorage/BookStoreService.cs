using System.Collections.Generic;

namespace BookStorage
{
    public class BookStoreService
    {
        private BookStore _bookStorage = new BookStore();

        public IReadOnlyList<Book> getBooks()
        {
            return _bookStorage.Books;
        }
    }
}