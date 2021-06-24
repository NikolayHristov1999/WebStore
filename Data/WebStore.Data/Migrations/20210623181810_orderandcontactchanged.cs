using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class orderandcontactchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Orders",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                newName: "IX_Orders_CartId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contacts",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "ShippingMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cart_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cart_CartId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                newName: "IX_Orders_ProductId");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Contacts",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
