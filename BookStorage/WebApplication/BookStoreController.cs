using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    #warning лучше тут прописать роут api/book, а для каждого метода уже добавлять необходимое значение
    #warning это же совсем корень твоего контроллера получается, если добавится ещё какой-то, то ты не сможешь и к нему обращаться по /api 
    [Route("api")]
    public class BookStoreController : ControllerBase
    {
        #warning readonly
        private IProxy _proxy;      
        private readonly BookContextDbFactory _dbContextFactory;
        public BookStoreController(IProxy proxy, BookContextDbFactory dbContextFactory)
        {
            _proxy = proxy;
            _dbContextFactory = dbContextFactory;
        }
        
        #warning ну а тут оставить только {count}
        #warning ну и странно, что у тебя get метод,который ничего не возвращает, а только запрашивает и складывает в базу
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
        
        #warning нет метода, которым можно получить все книги
        
    }
}