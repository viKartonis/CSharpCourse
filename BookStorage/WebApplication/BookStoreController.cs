using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    [Route("api")]
    public class BookStoreController : ControllerBase
    {
        private IProxy _proxy;      
        private readonly BookContextDbFactory _dbContextFactory;
        public BookStoreController(IProxy proxy, BookContextDbFactory dbContextFactory)
        {
            _proxy = proxy;
            _dbContextFactory = dbContextFactory;
        }
        
        [HttpGet("book/{count}")]
        public async Task AddBooks(int count)
        {
            List<EntityBook> books = _proxy.GetBooksFromRemoteServer(count).Result;
            using (var context = _dbContextFactory.GetContext())
            {
                for (int i = 0; i < count; ++i)
                {
                    context.AddBook(books[i]);
                    await context.SaveChangesAsync();
                }
            }
        }
        
    }
}