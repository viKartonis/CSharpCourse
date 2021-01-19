using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStorage.DataBase.Migrations
{
    public partial class AddAllShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookshop");

            migrationBuilder.CreateTable(
                name: "EntityGenre",
                schema: "bookshop",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityGenre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "EntityShop",
                schema: "bookshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreCapacity = table.Column<int>(type: "integer", nullable: false, defaultValue: 250),
                    CurrentBookCount = table.Column<int>(type: "integer", nullable: false),
                    Money = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 10000m),
                    MinimumBookCountPercent = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 5m),
                    CountMonthNotSoldBooksPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    SupplyPercent = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 10m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityShop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityBook",
                schema: "bookshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityBook_EntityGenre_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "bookshop",
                        principalTable: "EntityGenre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityBook_EntityShop_ShopId",
                        column: x => x.ShopId,
                        principalSchema: "bookshop",
                        principalTable: "EntityShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityDiscounts",
                schema: "bookshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityDiscounts_EntityShop_ShopId",
                        column: x => x.ShopId,
                        principalSchema: "bookshop",
                        principalTable: "EntityShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityBook_GenreId",
                schema: "bookshop",
                table: "EntityBook",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityBook_ShopId",
                schema: "bookshop",
                table: "EntityBook",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDiscounts_ShopId",
                schema: "bookshop",
                table: "EntityDiscounts",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityBook",
                schema: "bookshop");

            migrationBuilder.DropTable(
                name: "EntityDiscounts",
                schema: "bookshop");

            migrationBuilder.DropTable(
                name: "EntityGenre",
                schema: "bookshop");

            migrationBuilder.DropTable(
                name: "EntityShop",
                schema: "bookshop");
        }
    }
}
