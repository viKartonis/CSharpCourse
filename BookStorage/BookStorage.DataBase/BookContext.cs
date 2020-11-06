using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStorage.DataBase
{
    public class BookContext : DbContext
    {
        public const string DefaultSchemaName = "dbname";
        public BookContext(DbContextOptions options) : base(options)
        {
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

        public void AddBook(EntityBook book)
        {
            Set<EntityBook>().Add(book);
        }
    }
}