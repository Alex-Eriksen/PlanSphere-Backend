using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixblockedjobtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamBlockedJobTitles_JobTitleId",
                table: "TeamBlockedJobTitles");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentBlockedJobTitles_JobTitleId",
                table: "DepartmentBlockedJobTitles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBlockedJobTitles_JobTitleId",
                table: "CompanyBlockedJobTitles");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBlockedJobTitles_JobTitleId",
                table: "TeamBlockedJobTitles",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBlockedJobTitles_JobTitleId",
                table: "DepartmentBlockedJobTitles",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBlockedJobTitles_JobTitleId",
                table: "CompanyBlockedJobTitles",
                column: "JobTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamBlockedJobTitles_JobTitleId",
                table: "TeamBlockedJobTitles");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentBlockedJobTitles_JobTitleId",
                table: "DepartmentBlockedJobTitles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBlockedJobTitles_JobTitleId",
                table: "CompanyBlockedJobTitles");

            migrationBuilder.CreateIndex(
                name: "IX_TeamBlockedJobTitles_JobTitleId",
                table: "TeamBlockedJobTitles",
                column: "JobTitleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentBlockedJobTitles_JobTitleId",
                table: "DepartmentBlockedJobTitles",
                column: "JobTitleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBlockedJobTitles_JobTitleId",
                table: "CompanyBlockedJobTitles",
                column: "JobTitleId",
                unique: true);
        }
    }
}
