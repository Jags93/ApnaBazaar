using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdU",
                table: "Ordini");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ordini");

            migrationBuilder.AddColumn<int>(
                name: "IdU",
                table: "Ordini",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
