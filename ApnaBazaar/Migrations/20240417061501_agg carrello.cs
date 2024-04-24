using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggcarrello : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrello",
                columns: table => new
                {
                    IdC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdU = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrello", x => x.IdC);
                });

            migrationBuilder.CreateTable(
                name: "ItemCarrello",
                columns: table => new
                {
                    IdI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdA = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarrelloIdC = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCarrello", x => x.IdI);
                    table.ForeignKey(
                        name: "FK_ItemCarrello_Carrello_CarrelloIdC",
                        column: x => x.CarrelloIdC,
                        principalTable: "Carrello",
                        principalColumn: "IdC");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrello_CarrelloIdC",
                table: "ItemCarrello",
                column: "CarrelloIdC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCarrello");

            migrationBuilder.DropTable(
                name: "Carrello");
        }
    }
}
