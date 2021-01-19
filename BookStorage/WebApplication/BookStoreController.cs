using System.Collections.Generic;
using System.Threading.Tasks;
using ContractLibrary;
using Microsoft.AspNetCore.Mvc;

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
        public async Task GetMoneyFromShop()
        {
            await _iDataService.GetMoney();
        }
        
        [HttpGet("books")]
        public async Task<List<Book>> GetBooks()
        {
            return await _iDataService.GetData();
        }

    }
}