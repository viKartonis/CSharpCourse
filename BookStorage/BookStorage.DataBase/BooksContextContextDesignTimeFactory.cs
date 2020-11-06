using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStorage.DataBase
{
    public sealed class BookContextDbFactoryDesignTime : IDesignTimeDbContextFactory<BookContext>
    {
        private const string DefaultConnectionString =
            "server=(local);Database=dbname; User Id=newuser; Password=password;";
        public static DbContextOptions<BookContext> GetSqlServerOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<BookContext>()
                .UseSqlServer(connectionString ?? DefaultConnectionString, x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", BookContext.DefaultSchemaName);
                })
                .Options;
        }
        public BookContext CreateDbContext(string[] args)
        {
            return new BookContext(GetSqlServerOptions(null));
        }
    }
}