using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class articolordine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Articoli_IdA",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_IdA",
                table: "Ordini");

            migrationBuilder.CreateTable(
                name: "OrdineAticolo",
                columns: table => new
                {
                    IdOa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdO = table.Column<int>(type: "int", nullable: false),
                    IdA = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdineAticolo", x => x.IdOa);
                    table.ForeignKey(
                        name: "FK_OrdineAticolo_Articoli_IdA",
                        column: x => x.IdA,
                        principalTable: "Articoli",
                        principalColumn: "IdA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdineAticolo_Ordini_IdO",
                        column: x => x.IdO,
                        principalTable: "Ordini",
                        principalColumn: "IdO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdineAticolo_IdA",
                table: "OrdineAticolo",
                column: "IdA");

            migrationBuilder.CreateIndex(
                name: "IX_OrdineAticolo_IdO",
                table: "OrdineAticolo",
                column: "IdO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdineAticolo");

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
    }
}
