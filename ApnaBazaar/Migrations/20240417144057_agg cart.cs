using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prezzo",
                table: "ItemCarrello");

            migrationBuilder.AddColumn<decimal>(
                name: "TotPrezzo",
                table: "Carrello",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotQuantita",
                table: "Carrello",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrello_IdA",
                table: "ItemCarrello",
                column: "IdA");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCarrello_Articoli_IdA",
                table: "ItemCarrello",
                column: "IdA",
                principalTable: "Articoli",
                principalColumn: "IdA",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCarrello_Articoli_IdA",
                table: "ItemCarrello");

            migrationBuilder.DropIndex(
                name: "IX_ItemCarrello_IdA",
                table: "ItemCarrello");

            migrationBuilder.DropColumn(
                name: "TotPrezzo",
                table: "Carrello");

            migrationBuilder.DropColumn(
                name: "TotQuantita",
                table: "Carrello");

            migrationBuilder.AddColumn<decimal>(
                name: "Prezzo",
                table: "ItemCarrello",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
