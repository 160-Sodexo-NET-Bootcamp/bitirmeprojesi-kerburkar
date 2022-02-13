using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class offerfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Products_ProductId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Users_OfferedUserId",
                table: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_ProductId",
                table: "Offers",
                newName: "IX_Offers_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_OfferedUserId",
                table: "Offers",
                newName: "IX_Offers_OfferedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Products_ProductId",
                table: "Offers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Users_OfferedUserId",
                table: "Offers",
                column: "OfferedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Products_ProductId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Users_OfferedUserId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ProductId",
                table: "Offer",
                newName: "IX_Offer_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_OfferedUserId",
                table: "Offer",
                newName: "IX_Offer_OfferedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Products_ProductId",
                table: "Offer",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Users_OfferedUserId",
                table: "Offer",
                column: "OfferedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
