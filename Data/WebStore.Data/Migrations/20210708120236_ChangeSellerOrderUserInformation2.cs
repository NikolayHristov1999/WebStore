using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class ChangeSellerOrderUserInformation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrder_AspNetUsers_UserId",
                table: "SellerOrder");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SellerOrder",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrder_UserId",
                table: "SellerOrder",
                newName: "IX_SellerOrder_SellerId");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "SellerOrder",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerOrder_BuyerId",
                table: "SellerOrder",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrder_AspNetUsers_BuyerId",
                table: "SellerOrder",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrder_AspNetUsers_SellerId",
                table: "SellerOrder",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrder_AspNetUsers_BuyerId",
                table: "SellerOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrder_AspNetUsers_SellerId",
                table: "SellerOrder");

            migrationBuilder.DropIndex(
                name: "IX_SellerOrder_BuyerId",
                table: "SellerOrder");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "SellerOrder");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "SellerOrder",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrder_SellerId",
                table: "SellerOrder",
                newName: "IX_SellerOrder_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrder_AspNetUsers_UserId",
                table: "SellerOrder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
