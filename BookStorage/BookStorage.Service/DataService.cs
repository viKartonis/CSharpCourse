using System.Threading.Tasks;
using BookStorage.DataBase;
using BookStorage.DataBase.Entities;
using WebApplication.DTO;

namespace BookStorage.Service
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

        public async Task<decimal> GetMoney()
        {
            return await _dbContextFactory.GetContext().GetMoney(_shopId);
        }

        public async Task UpdateBooksPrices(DiscountId discountId)
        {
            var context = _dbContextFactory.GetContext();
            var discount = await context.Set<EntityDiscounts>().FindAsync(discountId.Id);
            var books = await context.GetBooks();
            foreach (var book in books)
            {
                if (book.GenreId == discount.GenreId)
                {
                    book.Price *= (100 - discount.Value) / 100.0m;
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task AddDiscount(DiscountRequest discount)
        {
            var context = _dbContextFactory.GetContext();
            await context.Set<EntityDiscounts>().AddAsync(new EntityDiscounts()
            {
                Value = discount.Value,
                ShopId = _shopId
            });
            await context.SaveChangesAsync();
        }

        public async Task RollbackBooksPrices(DiscountId discountId)
        {
            var context = _dbContextFactory.GetContext();
            var discount = await context.Set<EntityDiscounts>().FindAsync(discountId.Id);
            var books = await context.GetBooks();
            foreach (var book in books)
            {
                if (book.GenreId == discount.GenreId)
                {
                    book.Price *= (100 - discount.Value) / 100.0m;
                }
            }
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