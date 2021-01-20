using System.Threading.Tasks;
using WebApplication.DTO;

namespace BookStorage.Service
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