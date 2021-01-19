using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;
using ContractLibrary;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;

namespace WebApplication
{
    [ApiController]
    [Route("api")]
    public class BookStoreController : ControllerBase
    {
        private readonly IDataService _iDataService;
        public BookStoreController(IDataService iDataService)
        {
            _iDataService = iDataService;
        }

        [HttpGet("store/money")]
        public async Task<decimal> GetMoneyFromShop()
        {
            return await _iDataService.GetMoney();
        }
        
        [HttpDelete("buyBook")]
        public async Task DeleteBookFromShop(Book bookRequest)
        {
            await _iDataService.DeleteBookFromShop(bookRequest);
        }
        
        [HttpGet("books")]
        public async Task<List<Book>> GetBooks()
        {
            return await _iDataService.GetData();
        }

    }
}