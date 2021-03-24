using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_BranchMastr_BranchID_Alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranhcId",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "BranchMaster",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "BranhcId",
                table: "BranchMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
