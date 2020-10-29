using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    [Route("api")]
    public class BookStoreController : ControllerBase
    {
        private IProxy _proxy;
        public BookStoreController(IProxy proxy)
        {
            _proxy = proxy;
        }
        
        [HttpGet("book/{count}")]
        public Task<List<BookResponse>> GetBooks(int count)
        {
            return _proxy.GetBooksFromRemoteServer(count);
        }
    }
}