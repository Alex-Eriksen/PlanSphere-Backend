using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_blocked_role_navigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamBlockedRoles_RoleId",
                table: "TeamBlockedRoles");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentBlockedRoles_RoleId",
                table: "DepartmentBlockedRoles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBlockedRoles_RoleId",
                table: "CompanyBlockedRoles");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBlockedRoles_RoleId",
                table: "TeamBlockedRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBlockedRoles_RoleId",
                table: "DepartmentBlockedRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBlockedRoles_RoleId",
                table: "CompanyBlockedRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamBlockedRoles_RoleId",
                table: "TeamBlockedRoles");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentBlockedRoles_RoleId",
                table: "DepartmentBlockedRoles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBlockedRoles_RoleId",
                table: "CompanyBlockedRoles");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBlockedRoles_RoleId",
                table: "TeamBlockedRoles",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBlockedRoles_RoleId",
                table: "DepartmentBlockedRoles",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBlockedRoles_RoleId",
                table: "CompanyBlockedRoles",
                column: "RoleId",
                unique: true);
        }
    }
}
