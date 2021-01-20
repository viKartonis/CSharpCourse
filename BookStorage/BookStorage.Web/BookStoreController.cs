using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        [HttpDelete("buyBook/{bookId}")]
        public async Task DeleteBookFromShop(int bookId)
        {
            await _iDataService.DeleteBookFromShop(bookId);
        }
        
        [HttpPut("discount/apply")]
        public async Task UpdateBooksPrices(DiscountId discountId)
        {
            await _iDataService.UpdateBooksPrices(discountId);
        }
        
        [HttpPost("discount/add")]
        public async Task AddDiscount([FromBody]DiscountRequest discountEntity)
        {
            await _iDataService.AddDiscount(discountEntity);
        }
        
        [HttpGet("books")]
        public async Task<List<Book>> GetBooks()
        {
            return await _iDataService.GetData();
        }

    }
}