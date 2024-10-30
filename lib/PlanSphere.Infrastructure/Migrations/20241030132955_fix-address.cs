using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ZipCodes_ZipCodePostalCode",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ZipCodePostalCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ZipCodePostalCode",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PostalCode",
                table: "Addresses",
                column: "PostalCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ZipCodes_PostalCode",
                table: "Addresses",
                column: "PostalCode",
                principalTable: "ZipCodes",
                principalColumn: "PostalCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ZipCodes_PostalCode",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PostalCode",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCodePostalCode",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ZipCodePostalCode",
                table: "Addresses",
                column: "ZipCodePostalCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ZipCodes_ZipCodePostalCode",
                table: "Addresses",
                column: "ZipCodePostalCode",
                principalTable: "ZipCodes",
                principalColumn: "PostalCode");
        }
    }
}
