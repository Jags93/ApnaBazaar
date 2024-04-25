using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggordine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItem_Articoli_ArticoloIdA",
                table: "WishlistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItem_Wishlists_WishlistIdW",
                table: "WishlistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem");

            migrationBuilder.RenameTable(
                name: "WishlistItem",
                newName: "WishlistItems");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItem_WishlistIdW",
                table: "WishlistItems",
                newName: "IX_WishlistItems_WishlistIdW");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItem_ArticoloIdA",
                table: "WishlistItems",
                newName: "IX_WishlistItems_ArticoloIdA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems",
                column: "IdWi");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_IdA",
                table: "Ordini",
                column: "IdA");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Articoli_IdA",
                table: "Ordini",
                column: "IdA",
                principalTable: "Articoli",
                principalColumn: "IdA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Articoli_ArticoloIdA",
                table: "WishlistItems",
                column: "ArticoloIdA",
                principalTable: "Articoli",
                principalColumn: "IdA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Wishlists_WishlistIdW",
                table: "WishlistItems",
                column: "WishlistIdW",
                principalTable: "Wishlists",
                principalColumn: "IdW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Articoli_IdA",
                table: "Ordini");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Articoli_ArticoloIdA",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Wishlists_WishlistIdW",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_IdA",
                table: "Ordini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems");

            migrationBuilder.RenameTable(
                name: "WishlistItems",
                newName: "WishlistItem");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItems_WishlistIdW",
                table: "WishlistItem",
                newName: "IX_WishlistItem_WishlistIdW");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItems_ArticoloIdA",
                table: "WishlistItem",
                newName: "IX_WishlistItem_ArticoloIdA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishlistItem",
                table: "WishlistItem",
                column: "IdWi");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItem_Articoli_ArticoloIdA",
                table: "WishlistItem",
                column: "ArticoloIdA",
                principalTable: "Articoli",
                principalColumn: "IdA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItem_Wishlists_WishlistIdW",
                table: "WishlistItem",
                column: "WishlistIdW",
                principalTable: "Wishlists",
                principalColumn: "IdW");
        }
    }
}
