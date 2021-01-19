using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStorage.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStorage.DataBase
{
    public class BookContext : DbContext
    {
        public const string DefaultSchemaName = "bookshop";
        public BookContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }
        
        public async Task<List<EntityBook>> GetBooks()
        {
            return await Set<EntityBook>().ToListAsync();
        }

        public List<EntityBook> GetBooks(int shopId)
        {
            return Set<EntityShop>().Find(shopId)?.Books;
        }
        
        public async Task AddBook(EntityBook book)
        {
            await Set<EntityBook>().AddAsync(book);
            await SaveChangesAsync();
        }

        public int GetGenreId(string genreName)
        {
            var genre = Set<EntityGenre>().FirstOrDefault(g => g.Name == genreName);
            if (genre == null)
            {
                genre = new EntityGenre
                {
                    Name = genreName
                };
                Set<EntityGenre>().Add(genre);
                SaveChanges();
            }
            return genre.GenreId;
        }

        public void CreateShop(int shopId)
        {
            Set<EntityShop>().Add(new EntityShop()
            {
                Id = shopId,
                Money = 0,
                StoreCapacity = 1000,
                SupplyPercent = 10,
                CurrentBookCount = 0,
                MinimumBookCountPercent = 50,
                CountMonthNotSoldBooksPercent = 5,
                DiscountId = 1,
                Discounts = new EntityDiscounts()
                {
                    Id = 1,
                    Value = 20
                }
            });
            SaveChanges();
        }

        public int GetCurrentBooksCount(int shopId)
        {
            return Set<EntityShop>().Find(shopId)?.CurrentBookCount?? -1;
        }
        
        public int GetStoreCapacity(int shopId)
        {
            return Set<EntityShop>().Find(shopId)?.StoreCapacity?? -1;
        }
        
        public decimal GetMinimumBookCountPercent(int shopId)
        {
            return Set<EntityShop>().Find(shopId)?.MinimumBookCountPercent?? -1;
        }
        
        public decimal GetCountMonthNotSoldBooksPercent(int shopId)
        {
            return Set<EntityShop>().Find(shopId)?.CountMonthNotSoldBooksPercent?? -1;
        }

        public async Task<decimal> GetMoney(int shopId)
        {
            return await Task.FromResult(Set<EntityShop>().Find(shopId)?.Money?? -1);
        }
    }
}