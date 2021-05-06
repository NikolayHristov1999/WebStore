using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class AddProductUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AddedByUserId",
                table: "Products",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_AddedByUserId",
                table: "Products",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_AddedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AddedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Products");
        }
    }
}
