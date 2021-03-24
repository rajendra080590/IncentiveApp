using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_AddTableFBMApproverMaster_UserRoleMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FBMApproverMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    PreparerUserId = table.Column<int>(nullable: false),
                    PreparerName = table.Column<string>(nullable: true),
                    PreparerEmailId = table.Column<string>(nullable: true),
                    PrimaryApproverUserId = table.Column<int>(nullable: true),
                    PrimaryApproverName = table.Column<string>(nullable: true),
                    PrimaryApproverEmailId = table.Column<string>(nullable: true),
                    SecondaryApproverUserId = table.Column<int>(nullable: true),
                    SecondaryApproverName = table.Column<string>(nullable: true),
                    SecondaryApproverEmailId = table.Column<string>(nullable: true),
                    PayrollApproverUserId = table.Column<int>(nullable: true),
                    PayrollApproverName = table.Column<string>(nullable: true),
                    PayrollApproverEmailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FBMApproverMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMapping", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FBMApproverMaster");

            migrationBuilder.DropTable(
                name: "UserRoleMapping");
        }
    }
}
