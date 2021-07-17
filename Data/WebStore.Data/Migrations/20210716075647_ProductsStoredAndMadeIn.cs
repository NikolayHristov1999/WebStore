using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class ProductsStoredAndMadeIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MadeIn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredInCountry",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MadeIn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoredInCountry",
                table: "Products");
        }
    }
}
