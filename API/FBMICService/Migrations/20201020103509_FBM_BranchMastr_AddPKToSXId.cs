using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_BranchMastr_AddPKToSXId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Update_Fix",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "UpdateFix",
                table: "BranchMaster",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchMaster",
                table: "BranchMaster",
                columns: new[] { "SXId" })
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateFix",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "Update_Fix",
                table: "BranchMaster",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
