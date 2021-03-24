using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AddIncentiveAppCalendarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncentiveAppCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollWeek = table.Column<string>(nullable: true),
                    PayrollDate = table.Column<DateTime>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    WeekId = table.Column<string>(nullable: true),
                    MonthName = table.Column<string>(nullable: true),
                    EmailReminder = table.Column<DateTime>(nullable: false),
                    IncentiveStart = table.Column<DateTime>(nullable: false),
                    IncentiveComplete = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncentiveAppCalendar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncentiveAppCalendar");
        }
    }
}
