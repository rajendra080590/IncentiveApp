using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AlterPayrollReviewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmittedBy",
                table: "PayrollReviewed",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                table: "PayrollReviewed",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "PayrollReviewed");

            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                table: "PayrollReviewed");
        }
    }
}
