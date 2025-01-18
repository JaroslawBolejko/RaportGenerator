using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaportGenerator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeServiceDateToreportOfMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfDokument",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "DateOfService",
                table: "Reports",
                newName: "DateOfDocument");

            migrationBuilder.AddColumn<string>(
                name: "ReportForMonth",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportForMonth",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "DateOfDocument",
                table: "Reports",
                newName: "DateOfService");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDokument",
                table: "Reports",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
