using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_InputFormTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormDetails",
                columns: table => new
                {
                    FormDetailId = table.Column<int>(nullable: false),
                    FormHeaderId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    IncentiveAmount = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(nullable: false),
                    SubmittedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDetails", x => x.FormDetailId);
                });

            migrationBuilder.CreateTable(
                name: "FormHeader",
                columns: table => new
                {
                    FormHeaderId = table.Column<string>(nullable: false),
                    BranchId = table.Column<string>(nullable: true),
                    WeekId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    ApproverId = table.Column<int>(nullable: false),
                    ApprovedBy = table.Column<int>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHeader", x => x.FormHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "FormHeaderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormHeaderId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ApproverId = table.Column<int>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHeaderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormHeaderDetails_FormHeader_FormHeaderId",
                        column: x => x.FormHeaderId,
                        principalTable: "FormHeader",
                        principalColumn: "FormHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormHeaderDetails_FormHeaderId",
                table: "FormHeaderDetails",
                column: "FormHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDetails");

            migrationBuilder.DropTable(
                name: "FormHeaderDetails");

            migrationBuilder.DropTable(
                name: "FormHeader");
        }
    }
}
