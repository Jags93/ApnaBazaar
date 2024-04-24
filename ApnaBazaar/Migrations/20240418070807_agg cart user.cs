using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class aggcartuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdU",
                table: "Carrello");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carrello",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carrello");

            migrationBuilder.AddColumn<int>(
                name: "IdU",
                table: "Carrello",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
