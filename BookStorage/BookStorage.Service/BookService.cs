using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStorage.DataBase;
using BookStorage.DataBase.Entities;
using ContractLibrary;
using WebApplication;

namespace BookStorage.Service
{
    public class BookService : IBookService
    {
        private readonly BookContextDbFactory _dbContextFactory;
        private readonly int _shopId;
        
        public BookService(BookContextDbFactory dbContextFactory, int shopId)
        {
            _dbContextFactory = dbContextFactory;
            _shopId = shopId;
            using (var context = _dbContextFactory.GetContext())
            {
                context.CreateShopIfNotExists(shopId);
            }
        }
        public async Task CreateBook(Book bookRequest)
        {
            var context = _dbContextFactory.GetContext();
            var shop = await context.Set<EntityShop>().FindAsync(_shopId);
            var money = shop.Money;
            var book = await TypesConverter.RequestToEntityBook(bookRequest, context);
            money -= book.Price * shop.SupplyPercent/100.0m;
            if (money < 0)
            {
                return;
            }
            await context.Set<EntityBook>()
                .AddAsync(book);
            shop.Money = money;
            await context.AddBookCurrentNumber(1, _shopId);
            await context.SaveChangesAsync();
        }
        
        public async Task<EntityBook> GetBook(int bookId)
        {
            var context = _dbContextFactory.GetContext();
            return await context.Set<EntityBook>().FindAsync(bookId);
        }
        
        public async Task UpdateBook(Book bookRequest)
        {
            var context = _dbContextFactory.GetContext();
            var genreId = await context.GetGenreId(bookRequest.Genre);

            var book = await context.Set<EntityBook>().FindAsync(bookRequest.Id);
            book.GenreId = genreId;
            book.Price = bookRequest.Price;
            book.Title = bookRequest.Title;
            book.IsNew = bookRequest.IsNew;
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteBookFromShop(int bookId)
        {
            var context = _dbContextFactory.GetContext();
            var entity = await context.Set<EntityBook>().FindAsync(bookId);
            context.Remove(entity);
            await context.SaveChangesAsync();
            
            var shop = await context.FindAsync<EntityShop>(_shopId);
            shop.Money += entity.Price;
            await context.SaveChangesAsync();
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
    }
}