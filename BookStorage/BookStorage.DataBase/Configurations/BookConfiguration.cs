using BookStorage.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStorage.DataBase.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<EntityBook>
    {
        public void Configure(EntityTypeBuilder<EntityBook> builder)
        {
            builder.ToTable(nameof(EntityBook), BookContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.GenreId).IsRequired();
            builder.Property(x => x.ShopId).IsRequired();
        }
    }
}