using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStorage.DataBase;
using BookStorage.DataBase.Entities;
using ContractLibrary;
using WebApplication.DTO;

namespace WebApplication
{
    public class DataService : IDataService
    {
        private readonly BookContextDbFactory _dbContextFactory;
        private readonly int _shopId;
        
        public DataService(BookContextDbFactory dbContextFactory, int shopId)
        {
            _dbContextFactory = dbContextFactory;
            _shopId = shopId;
            using (var context = _dbContextFactory.GetContext())
            {
                context.CreateShopIfNotExists(shopId);
            }
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
            await using (var context = _dbContextFactory.GetContext())
            {
                var shop = await context.Set<EntityShop>().FindAsync(_shopId);
                var money = shop.Money;
                foreach (var book in bookRequest)
                {
                    var entity = await TypesConverter.RequestToEntityBook(book,
                        context);
                    money -= book.Price * shop.SupplyPercent/100.0m;
                    if (money < 0)
                    {
                        money += book.Price * shop.SupplyPercent / 100.0m;
                        break;
                    }
                    await context.AddBook(entity);
                }
                shop.Money = money;
                await context.SaveChangesAsync();
                await context.AddBookCurrentNumber(bookRequest.Count, _shopId);
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

        public async Task DeleteBookFromShop(Book bookRequest)
        {
            var context = _dbContextFactory.GetContext();
            var entity = await TypesConverter.RequestToEntityBook(bookRequest, context);
            context.Remove(entity);
            await context.SaveChangesAsync();
            
            var shop = await context.FindAsync<EntityShop>(_shopId);
            shop.Money += entity.Price;
            await context.SaveChangesAsync();
        }

        public async Task<int> CheckNeedToOrder()
        {
            var context = _dbContextFactory.GetContext();
            var shop = await context.FindAsync<EntityShop>(_shopId);
            if (!(shop.CurrentBookCount <= shop.StoreCapacity * shop.MinimumBookCountPercent/100.0m))
            {
                return 0;
            }
            var books = context.GetBooks(_shopId);
            if (books == null || books.Count == 0)
            {
                return shop.StoreCapacity;
            }
            return shop.CurrentBookCount - (int)(shop.StoreCapacity * shop.MinimumBookCountPercent/100.0m);
        }
    }
}