using BookStorage.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStorage.DataBase.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<EntityDiscounts>
    {
        public void Configure(EntityTypeBuilder<EntityDiscounts> builder)
        {
            builder.ToTable(nameof(EntityDiscounts), BookContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Value).IsRequired();
        }
    }
}