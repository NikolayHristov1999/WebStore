using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class AddedViewsOnProductsAndStatusOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SellerOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SellerOrder");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Contacts");
        }
    }
}
