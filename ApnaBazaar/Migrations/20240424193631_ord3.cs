using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class ord3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Articoli_ArticoliIdA",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_ArticoliIdA",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "ArticoliIdA",
                table: "Ordini");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Articoli_IdA",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_IdA",
                table: "Ordini");

            migrationBuilder.AddColumn<int>(
                name: "ArticoliIdA",
                table: "Ordini",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_ArticoliIdA",
                table: "Ordini",
                column: "ArticoliIdA");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Articoli_ArticoliIdA",
                table: "Ordini",
                column: "ArticoliIdA",
                principalTable: "Articoli",
                principalColumn: "IdA",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
