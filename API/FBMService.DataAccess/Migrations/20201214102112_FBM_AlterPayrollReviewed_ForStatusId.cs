using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AlterPayrollReviewed_ForStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PayrollReviewed",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PayrollReviewed");
        }
    }
}
