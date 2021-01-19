﻿// <auto-generated />
using System;
using BookStorage.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStorage.DataBase.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20210119134801_AddBookWithShopId2")]
    partial class AddBookWithShopId2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("bookshop")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateOfDelivery")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsNew")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("ShopId");

                    b.ToTable("EntityBook", "bookshop");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityDiscounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("EntityDiscounts", "bookshop");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityGenre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.ToTable("EntityGenre", "bookshop");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<decimal>("CountMonthNotSoldBooksPercent")
                        .HasColumnType("numeric");

                    b.Property<int>("CurrentBookCount")
                        .HasColumnType("integer");

                    b.Property<int>("DiscountId")
                        .HasColumnType("integer");

                    b.Property<decimal>("MinimumBookCountPercent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(5m);

                    b.Property<decimal>("Money")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(10000m);

                    b.Property<int>("StoreCapacity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1000);

                    b.Property<decimal>("SupplyPercent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(10m);

                    b.HasKey("Id");

                    b.ToTable("EntityShop", "bookshop");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityBook", b =>
                {
                    b.HasOne("BookStorage.DataBase.Entities.EntityGenre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStorage.DataBase.Entities.EntityShop", "Shop")
                        .WithMany("Books")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityShop", b =>
                {
                    b.HasOne("BookStorage.DataBase.Entities.EntityDiscounts", "Discounts")
                        .WithMany("Shops")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discounts");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityDiscounts", b =>
                {
                    b.Navigation("Shops");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityGenre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStorage.DataBase.Entities.EntityShop", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
