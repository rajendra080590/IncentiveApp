using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AddPayrollReviewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayrollReviewed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormHeaderId = table.Column<string>(nullable: true),
                    WeekId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    PayrollDate = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollReviewed", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayrollReviewed");
        }
    }
}
