using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractLibrary;

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
    }
}