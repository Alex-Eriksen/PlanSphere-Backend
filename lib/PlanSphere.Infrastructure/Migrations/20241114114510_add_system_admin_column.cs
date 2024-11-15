using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_system_admin_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SystemAdministrator",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemAdministrator",
                table: "Users");
        }
    }
}
