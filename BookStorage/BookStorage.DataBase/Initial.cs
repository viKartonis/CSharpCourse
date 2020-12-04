using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStorage.DataBase
{
    #warning как ты получила эту миграцию? EF обычно создаёт папку Migrations, все миграции добавляет туда, а так же создаёт полный snapshot базы
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