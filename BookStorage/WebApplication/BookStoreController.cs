using System.Collections.Generic;
using BookStorage;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    [Route("api/books")]
    public class BookStoreController
    {
        private BookStore _bookStore;
        
        [HttpGet]
        public IReadOnlyList<Book> getBooks()
        {
            return _bookStore.Books;
        }
    }
}