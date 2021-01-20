using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractLibrary;
using WebApplication.DTO;

namespace WebApplication
{
    public interface IDataService
    {
        Task<int> CheckNeedToOrder();
        Task<decimal> GetMoney();
        Task UpdateBooksPrices(DiscountId discountId);
        Task AddDiscount(DiscountRequest discount);
        Task RollbackBooksPrices(DiscountId discountId);
    }
}