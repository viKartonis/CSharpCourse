using System.Threading.Tasks;
using BookStorage.DataBase.Entities;
using BookStorage.Service;
using ContractLibrary;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _iBookService;
        
        public BookController(IBookService iBookService)
        {
            _iBookService = iBookService;
        }
        
        [HttpPost]
        public async Task CreateBook(Book bookRequest)
        {
            await _iBookService.CreateBook(bookRequest);
        }
        
        [HttpPut]
        public async Task UpdateBook(Book bookRequest)
        {
            await _iBookService.UpdateBook(bookRequest);
        }
        
        [HttpGet("{id}")]
        public async Task<EntityBook> GetBook(int id)
        {
            return await _iBookService.GetBook(id);
        }
        
        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            await _iBookService.DeleteBookFromShop(id);
        }
        
        [HttpGet]
        public async Task GetAllBooks()
        {
            await _iBookService.GetData();
        }
    }
}