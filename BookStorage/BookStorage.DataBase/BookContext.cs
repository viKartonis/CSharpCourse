using System.Collections.Generic;
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
        public async Task AddBookCurrentNumber(int currentNumber,int shopId)
        {
            var shop = await Set<EntityShop>().FindAsync(shopId);
            shop.CurrentBookCount += currentNumber;
            await SaveChangesAsync();
        }
        public async Task<int> GetGenreId(string genreName)
        {
            var genre = await Set<EntityGenre>().FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                genre = new EntityGenre
                {
                    Name = genreName
                };
                await Set<EntityGenre>().AddAsync(genre);
                await SaveChangesAsync();
            }
            return genre.GenreId;
        }

        public async Task<decimal> GetMoney(int shopId)
        {
            return (await Set<EntityShop>().FindAsync(shopId))?.Money?? -1;
        }

        public void CreateShopIfNotExists(int shopId)
        {
            var shop = Set<EntityShop>().Find(shopId);
            if (shop == null)
            {
                Set<EntityShop>().Add(new EntityShop()
                {
                    Id = shopId,
                    Money = 0,
                    StoreCapacity = 250,
                    SupplyPercent = 10,
                    CurrentBookCount = 0,
                    MinimumBookCountPercent = 50,
                    CountMonthNotSoldBooksPercent = 5
                });
                SaveChanges();
            }
        }
    }
}