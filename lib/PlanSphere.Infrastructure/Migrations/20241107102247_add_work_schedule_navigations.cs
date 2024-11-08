using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_work_schedule_navigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamSettings_DefaultRoleId",
                table: "TeamSettings");

            migrationBuilder.DropIndex(
                name: "IX_TeamSettings_DefaultWorkScheduleId",
                table: "TeamSettings");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationSettings_DefaultRoleId",
                table: "OrganisationSettings");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationSettings_DefaultWorkScheduleId",
                table: "OrganisationSettings");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSettings_DefaultRoleId",
                table: "DepartmentSettings");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSettings_DefaultWorkScheduleId",
                table: "DepartmentSettings");

            migrationBuilder.DropIndex(
                name: "IX_CompanySettings_DefaultRoleId",
                table: "CompanySettings");

            migrationBuilder.DropIndex(
                name: "IX_CompanySettings_DefaultWorkScheduleId",
                table: "CompanySettings");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSettings_DefaultRoleId",
                table: "TeamSettings",
                column: "DefaultRoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamSettings_DefaultWorkScheduleId",
                table: "TeamSettings",
                column: "DefaultWorkScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationSettings_DefaultRoleId",
                table: "OrganisationSettings",
                column: "DefaultRoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationSettings_DefaultWorkScheduleId",
                table: "OrganisationSettings",
                column: "DefaultWorkScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSettings_DefaultRoleId",
                table: "DepartmentSettings",
                column: "DefaultRoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSettings_DefaultWorkScheduleId",
                table: "DepartmentSettings",
                column: "DefaultWorkScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_DefaultRoleId",
                table: "CompanySettings",
                column: "DefaultRoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_DefaultWorkScheduleId",
                table: "CompanySettings",
                column: "DefaultWorkScheduleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamSettings_DefaultRoleId",
                table: "TeamSettings");

            migrationBuilder.DropIndex(
                name: "IX_TeamSettings_DefaultWorkScheduleId",
                table: "TeamSettings");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationSettings_DefaultRoleId",
                table: "OrganisationSettings");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationSettings_DefaultWorkScheduleId",
                table: "OrganisationSettings");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSettings_DefaultRoleId",
                table: "DepartmentSettings");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSettings_DefaultWorkScheduleId",
                table: "DepartmentSettings");

            migrationBuilder.DropIndex(
                name: "IX_CompanySettings_DefaultRoleId",
                table: "CompanySettings");

            migrationBuilder.DropIndex(
                name: "IX_CompanySettings_DefaultWorkScheduleId",
                table: "CompanySettings");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSettings_DefaultRoleId",
                table: "TeamSettings",
                column: "DefaultRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSettings_DefaultWorkScheduleId",
                table: "TeamSettings",
                column: "DefaultWorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationSettings_DefaultRoleId",
                table: "OrganisationSettings",
                column: "DefaultRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationSettings_DefaultWorkScheduleId",
                table: "OrganisationSettings",
                column: "DefaultWorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSettings_DefaultRoleId",
                table: "DepartmentSettings",
                column: "DefaultRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSettings_DefaultWorkScheduleId",
                table: "DepartmentSettings",
                column: "DefaultWorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_DefaultRoleId",
                table: "CompanySettings",
                column: "DefaultRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_DefaultWorkScheduleId",
                table: "CompanySettings",
                column: "DefaultWorkScheduleId");
        }
    }
}
