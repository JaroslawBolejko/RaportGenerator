using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaportGenerator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrintCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackPrintsAmount",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ColorPrintsAmount",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BlackWhitePrintCounter",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ColorPrintCounter",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlackPrintsAmount",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorPrintsAmount",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlackWhitePrintCounter",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorPrintCounter",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
