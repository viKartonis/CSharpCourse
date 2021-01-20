using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStorage.DataBase;
using ContractLibrary;
using WebApplication.DTO;

namespace WebApplication
{
    public interface IDataService
    { 
        Task<List<Book>> GetData(DateTimeOffset now);
        Task<List<Book>> GetData();
        Task AddData(List<Book> bookRequest);
        Task<int> CheckNeedToOrder();
        int CountMonthNotSoldBooks(DateTimeOffset dateTime);
        Task<decimal> GetMoney();
        Task DeleteBookFromShop(int bookId);
        Task UpdateBooksPrices(DiscountId discountId);
        Task AddDiscount(DiscountRequest discount);
    }
}