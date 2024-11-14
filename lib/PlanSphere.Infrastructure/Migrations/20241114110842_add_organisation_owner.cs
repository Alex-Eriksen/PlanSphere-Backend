using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_organisation_owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OwnerId",
                table: "Organisations",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_OwnerId",
                table: "Organisations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Users_OwnerId",
                table: "Organisations",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Users_OwnerId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_OwnerId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Organisations");
        }
    }
}
