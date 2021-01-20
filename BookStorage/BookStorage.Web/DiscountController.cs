using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;

namespace WebApplication
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly IDataService _dataService;
        public DiscountController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPut("apply")]
        public async Task UpdateBooksPrices(DiscountId discountId)
        {
            await _dataService.UpdateBooksPrices(discountId);
        }
        [HttpPut("rollback")]
        public async Task RollbackBooksPrices(DiscountId discountId)
        {
            await _dataService.RollbackBooksPrices(discountId);
        }
        
        [HttpPost("add")]
        public async Task AddDiscount([FromBody]DiscountRequest discountEntity)
        {
            await _dataService.AddDiscount(discountEntity);
        }

    }
}