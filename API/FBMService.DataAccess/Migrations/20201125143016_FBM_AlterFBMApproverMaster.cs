using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AlterFBMApproverMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionCode",
                table: "FBMApproverMaster",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "FBMApproverMaster");
        }
    }
}
