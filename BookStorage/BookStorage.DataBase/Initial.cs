using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStorage.DataBase
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbname");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "dbname",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title  = table.Column<string>(nullable: false),
                    Genre = table.Column<string>(),
                    Price = table.Column<decimal>(nullable: false),
                    IsNew = table.Column<bool>(),
                    DateOfDelivery = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "dbname");
        }
    }
}