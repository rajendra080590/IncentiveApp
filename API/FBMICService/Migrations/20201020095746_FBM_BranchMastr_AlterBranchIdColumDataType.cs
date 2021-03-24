using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_BranchMastr_AlterBranchIdColumDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK_BranchMaster", table: "BranchMaster");
            migrationBuilder.AlterColumn<string>(
                name: "PrimaryHCN",
                table: "EmployeeMaster",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SXId",
                table: "BranchMaster",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PrimaryHCN",
                table: "EmployeeMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SXId",
                table: "BranchMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
