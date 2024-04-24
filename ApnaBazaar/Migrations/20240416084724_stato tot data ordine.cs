using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaBazaar.Migrations
{
    /// <inheritdoc />
    public partial class statototdataordine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Ordini",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stato",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Totale",
                table: "Ordini",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "Stato",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "Totale",
                table: "Ordini");
        }
    }
}
