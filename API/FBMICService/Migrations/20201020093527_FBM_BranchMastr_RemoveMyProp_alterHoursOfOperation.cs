using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_BranchMastr_RemoveMyProp_alterHoursOfOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseOfOperation",
                table: "BranchMaster");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "HoursOfOperation",
                table: "BranchMaster",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursOfOperation",
                table: "BranchMaster");

            migrationBuilder.AddColumn<string>(
                name: "HouseOfOperation",
                table: "BranchMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "BranchMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
