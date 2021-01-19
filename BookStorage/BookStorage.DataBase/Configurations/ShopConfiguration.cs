using BookStorage.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStorage.DataBase.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<EntityShop>
    {
        public void Configure(EntityTypeBuilder<EntityShop> builder)
        {
            builder.ToTable(nameof(EntityShop), BookContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
            builder.Property(x => x.Money).IsRequired().HasDefaultValue(10000);
            builder.Property(x => x.DiscountId).IsRequired();
            builder.Property(x => x.StoreCapacity).IsRequired().HasDefaultValue(1000);
            builder.Property(x => x.SupplyPercent).IsRequired().HasDefaultValue(10);
            builder.Property(x => x.CurrentBookCount).IsRequired();
            builder.Property(x => x.MinimumBookCountPercent).IsRequired().HasDefaultValue(5);
            builder.Property(x => x.CountMonthNotSoldBooksPercent).IsRequired();
            
            builder.HasMany(x => x.Books)
                .WithOne(b => b.Shop).HasForeignKey(b => b.ShopId)
                .IsRequired();
        }
    }
}