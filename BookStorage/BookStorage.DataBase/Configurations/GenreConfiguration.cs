using BookStorage.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStorage.DataBase.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<EntityGenre>
    {
        public void Configure(EntityTypeBuilder<EntityGenre> builder)
        {
            builder.ToTable(nameof(EntityGenre), BookContext.DefaultSchemaName);
            builder.HasKey(x => x.GenreId);
            builder.Property(x => x.GenreId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Books)
                .WithOne(b => b.Genre).HasForeignKey(b => b.GenreId)
                .IsRequired();
        }
    }
}