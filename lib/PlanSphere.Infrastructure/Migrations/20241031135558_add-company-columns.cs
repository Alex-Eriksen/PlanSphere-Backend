using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcompanycolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CareOf",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareOf",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Companies");
        }
    }
}
