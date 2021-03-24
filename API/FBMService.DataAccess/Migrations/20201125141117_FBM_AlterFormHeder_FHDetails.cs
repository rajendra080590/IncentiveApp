using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AlterFormHeder_FHDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayGroupName",
                table: "PayGroup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "FormHeaderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "FormHeader",
                nullable: true);


            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FormHeader",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrimaryApproverExpiryDate",
                table: "FormHeader",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayGroupName",
                table: "PayGroup");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FormHeaderDetails");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "FormHeader");

            
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FormHeader");

            migrationBuilder.DropColumn(
                name: "PrimaryApproverExpiryDate",
                table: "FormHeader");
        }
    }
}
