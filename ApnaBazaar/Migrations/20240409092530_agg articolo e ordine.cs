using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggarticoloeordine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articoli",
                columns: table => new
                {
                    IdA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    Img1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articoli", x => x.IdA);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    IdO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdA = table.Column<int>(type: "int", nullable: false),
                    IdU = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.IdO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articoli");

            migrationBuilder.DropTable(
                name: "Ordini");
        }
    }
}
