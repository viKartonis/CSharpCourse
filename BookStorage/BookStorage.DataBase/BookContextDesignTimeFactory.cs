using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStorage.DataBase
{
    public sealed class BookContextDesignTimeFactory : IDesignTimeDbContextFactory<BookContext>
    {
        private const string DefaultConnectionString =
            "User ID=bookshop_user;Password=123;Host=localhost;Port=5432;Database=bookshop;";
            
        public static DbContextOptions<BookContext> GetSqlServerOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<BookContext>()
                .UseNpgsql(connectionString ?? DefaultConnectionString, x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", BookContext.DefaultSchemaName);
                })
                .Options;
        } 
        
        public BookContext CreateDbContext(string[] args)
        {
            return new BookContext(GetSqlServerOptions(DefaultConnectionString));
        }
    }
}