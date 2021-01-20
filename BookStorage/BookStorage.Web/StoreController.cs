using System.Threading.Tasks;
using BookStorage.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{    
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private readonly IDataService _dataService;
        
        public StoreController(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpGet("money")]
        public async Task<decimal> GetMoneyFromShop()
        {
            return await _dataService.GetMoney();
        }
    }
}