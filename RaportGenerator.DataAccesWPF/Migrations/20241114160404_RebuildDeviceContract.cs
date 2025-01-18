using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaportGenerator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDeviceContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Contracts_ValidContractId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Contracts_ContractId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ContractId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ValidContractId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ValidContractId",
                table: "Devices");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssociatedDate",
                table: "ContractDevices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "ContractDevices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociatedDate",
                table: "ContractDevices");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "ContractDevices");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValidContractId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ContractId",
                table: "Reports",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ValidContractId",
                table: "Devices",
                column: "ValidContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Contracts_ValidContractId",
                table: "Devices",
                column: "ValidContractId",
                principalTable: "Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Contracts_ContractId",
                table: "Reports",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");
        }
    }
}
