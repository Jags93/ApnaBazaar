using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggwish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    IdW = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.IdW);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItem",
                columns: table => new
                {
                    IdWi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdA = table.Column<int>(type: "int", nullable: false),
                    ArticoloIdA = table.Column<int>(type: "int", nullable: false),
                    WishlistIdW = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItem", x => x.IdWi);
                    table.ForeignKey(
                        name: "FK_WishlistItem_Articoli_ArticoloIdA",
                        column: x => x.ArticoloIdA,
                        principalTable: "Articoli",
                        principalColumn: "IdA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItem_Wishlists_WishlistIdW",
                        column: x => x.WishlistIdW,
                        principalTable: "Wishlists",
                        principalColumn: "IdW");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItem_ArticoloIdA",
                table: "WishlistItem",
                column: "ArticoloIdA");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItem_WishlistIdW",
                table: "WishlistItem",
                column: "WishlistIdW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistItem");

            migrationBuilder.DropTable(
                name: "Wishlists");
        }
    }
}
