using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_jobtitles_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserJobTitle",
                columns: table => new
                {
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    JobTitleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobTitle", x => new { x.UserId, x.JobTitleId });
                    table.ForeignKey(
                        name: "FK_UserJobTitle_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJobTitle_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJobTitle_JobTitleId",
                table: "UserJobTitle",
                column: "JobTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserJobTitle");
        }
    }
}
