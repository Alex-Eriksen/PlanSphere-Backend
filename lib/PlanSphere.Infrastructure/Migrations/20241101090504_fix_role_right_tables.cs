using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_role_right_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRoleRight",
                table: "TeamRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationRoleRight",
                table: "OrganisationRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentRoleRight",
                table: "DepartmentRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyRoleRight",
                table: "CompanyRoleRight");

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "TeamRoleRight",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrganisationRoleRight");
            
            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "OrganisationRoleRight",
                type: "decimal(20,0)",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "DepartmentRoleRight",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompanyRoleRight");

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "CompanyRoleRight",
                type: "decimal(20,0)",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRoleRight",
                table: "TeamRoleRight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationRoleRight",
                table: "OrganisationRoleRight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentRoleRight",
                table: "DepartmentRoleRight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyRoleRight",
                table: "CompanyRoleRight",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRoleRight_TeamId",
                table: "TeamRoleRight",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationRoleRight_OrganisationId",
                table: "OrganisationRoleRight",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRight_DepartmentId",
                table: "DepartmentRoleRight",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRoleRight_CompanyId",
                table: "CompanyRoleRight",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRoleRight",
                table: "TeamRoleRight");

            migrationBuilder.DropIndex(
                name: "IX_TeamRoleRight_TeamId",
                table: "TeamRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationRoleRight",
                table: "OrganisationRoleRight");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationRoleRight_OrganisationId",
                table: "OrganisationRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentRoleRight",
                table: "DepartmentRoleRight");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentRoleRight_DepartmentId",
                table: "DepartmentRoleRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyRoleRight",
                table: "CompanyRoleRight");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRoleRight_CompanyId",
                table: "CompanyRoleRight");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamRoleRight");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DepartmentRoleRight");

            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "OrganisationRoleRight",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Id",
                table: "CompanyRoleRight",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRoleRight",
                table: "TeamRoleRight",
                column: "TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationRoleRight",
                table: "OrganisationRoleRight",
                column: "OrganisationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentRoleRight",
                table: "DepartmentRoleRight",
                column: "DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyRoleRight",
                table: "CompanyRoleRight",
                column: "CompanyId");
        }
    }
}
