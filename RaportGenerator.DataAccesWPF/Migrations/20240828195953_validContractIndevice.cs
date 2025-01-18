using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaportGenerator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class validContractIndevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Contracts_ContractId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Devices",
                newName: "ValidContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_ContractId",
                table: "Devices",
                newName: "IX_Devices_ValidContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Contracts_ValidContractId",
                table: "Devices",
                column: "ValidContractId",
                principalTable: "Contracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Contracts_ValidContractId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "ValidContractId",
                table: "Devices",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_ValidContractId",
                table: "Devices",
                newName: "IX_Devices_ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Contracts_ContractId",
                table: "Devices",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");
        }
    }
}
