using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStorage.DataBase;
using ContractLibrary;

namespace WebApplication
{
    public class DataService : IDataService
    {
        private readonly BookContextDbFactory _dbContextFactory;
        private int _shopId;
        
        public DataService(BookContextDbFactory dbContextFactory, int shopId)
        {
            _dbContextFactory = dbContextFactory;
            _shopId = shopId;
        }

        public async Task<List<Book>> GetData(DateTimeOffset now)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                var entityBooks = await context.GetBooks();
                var books = new List<Book>();
                foreach (var entity in entityBooks)
                {
                    books.Add(TypesConverter.EntityToResponse(entity));
                }
                return books;
            }
        }


        public async Task<List<Book>> GetData()
        {
            using (var context = _dbContextFactory.GetContext())
            {
                var entityBooks = await context.GetBooks();
                return entityBooks
                    .Select(TypesConverter.EntityToResponse)
                    .ToList();
            }
        }

        public async Task AddData(List<Book> bookRequest)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                foreach (var book in bookRequest)
                {
                    var entity = TypesConverter.RequestToEntityBook(book,
                        context);
                    await context.AddBook(entity);
                }
            }
        }

        public int CountMonthNotSoldBooks(DateTimeOffset dateTime)
        {
            var context = _dbContextFactory.GetContext();
            var books = context.GetBooks(_shopId);
            var count = 0;
            foreach (var book in books)
            {
                if (!(dateTime.Month - dateTime.Month < 1))
                {
                    count++;
                }
            }

            return count;
        }

        public async Task<decimal> GetMoney()
        {
            return await _dbContextFactory.GetContext().GetMoney(_shopId);
        }

        public int CheckNeedToOrder(DateTimeOffset now)
        {
            var context = _dbContextFactory.GetContext();
            var currentBooksCount = context.GetCurrentBooksCount(_shopId);
            if (currentBooksCount == -1)
            {
                context.CreateShop(_shopId);
                currentBooksCount = context.GetCurrentBooksCount(_shopId);
            }
            
            var storeCapacity = context.GetStoreCapacity(_shopId);
            var minimumBookCountPercent = context.GetMinimumBookCountPercent(_shopId);
            var countMonthNotSoldBooksPercent = context.GetCountMonthNotSoldBooksPercent(_shopId);

            if (!(currentBooksCount <= storeCapacity * minimumBookCountPercent/100.0m))
            {
                return 0;
            }
            var books = context.GetBooks(_shopId);
            if (books == null || books.Count == 0)
            {
                return storeCapacity;
            }
            return CountMonthNotSoldBooks(now) - (int) (storeCapacity * countMonthNotSoldBooksPercent);
        }
    }
}