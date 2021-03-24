using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.DataAccess.Migrations
{
    public partial class FBM_InitalDBSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<string>(nullable: true),
                    ApprovalLevel = table.Column<int>(nullable: true),
                    ApproverId = table.Column<int>(nullable: true),
                    ApprovalStage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchMaster",
                columns: table => new
                {
                    SXId = table.Column<string>(nullable: false),
                    BranchId = table.Column<string>(nullable: false),
                    Division = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(maxLength: 300, nullable: false),
                    Addess = table.Column<string>(maxLength: 500, nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    StProv = table.Column<string>(nullable: true),
                    ZipPostal = table.Column<string>(nullable: true),
                    BranchManager = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    TollFree = table.Column<string>(nullable: true),
                    HoursOfOperation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    FBMDate = table.Column<string>(nullable: true),
                    DateOpen = table.Column<string>(nullable: true),
                    DateClose = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    MetroDistrict = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    RegionalVP = table.Column<string>(nullable: true),
                    DistrictManager = table.Column<string>(nullable: true),
                    OprtaionalManager = table.Column<string>(nullable: true),
                    OtherKeyContact = table.Column<string>(nullable: true),
                    KelmarContact = table.Column<string>(nullable: true),
                    DSIContact = table.Column<string>(nullable: true),
                    E_19Link = table.Column<string>(nullable: true),
                    SafetyCordinator = table.Column<string>(nullable: true),
                    HRGeneralist = table.Column<string>(nullable: true),
                    ADPCo = table.Column<string>(nullable: true),
                    SubsidiaryName = table.Column<string>(nullable: true),
                    Asset_Stock = table.Column<string>(nullable: true),
                    Legacy = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UpdateFix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchMaster", x => x.SXId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMaster",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: false),
                    FBMId = table.Column<string>(nullable: true),
                    SupervisorId = table.Column<int>(nullable: false),
                    PrimaryHCN = table.Column<string>(nullable: true),
                    PrimaryJob = table.Column<string>(nullable: true),
                    PrimaryRegion = table.Column<string>(nullable: true),
                    PrimaryDepartment = table.Column<string>(nullable: true),
                    PayGroup = table.Column<string>(nullable: true),
                    PayFrequency = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMaster", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "FBMConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FBMKey = table.Column<string>(nullable: true),
                    FBMValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FBMConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FBMUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EmailId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FBMUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FormDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormDetailId = table.Column<string>(nullable: true),
                    FormHeaderId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    IncentiveAmount = table.Column<double>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormHeaderId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    WeekId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    ApproverId = table.Column<int>(nullable: true),
                    ApprovedBy = table.Column<int>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    TotalEmployee = table.Column<int>(nullable: true),
                    Incentivised = table.Column<int>(nullable: true),
                    TotalAmount = table.Column<double>(nullable: true),
                    IncentiveWeekDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormHeaderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormHeaderId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ApproverUserId = table.Column<int>(nullable: true),
                    ApproverEmailId = table.Column<string>(nullable: true),
                    ApprovalStage = table.Column<string>(nullable: true),
                    SubmittedOn = table.Column<DateTime>(nullable: true),
                    SubmittedBy = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHeaderDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayGroup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayGroupId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayGroup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StatusMaster",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMaster", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BranchUserMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    BranchId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchUserMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchUserMapping_FBMUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FBMUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchUserMapping_UserId",
                table: "BranchUserMapping",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApproverMaster");

            migrationBuilder.DropTable(
                name: "BranchMaster");

            migrationBuilder.DropTable(
                name: "BranchUserMapping");

            migrationBuilder.DropTable(
                name: "EmployeeMaster");

            migrationBuilder.DropTable(
                name: "FBMConfiguration");

            migrationBuilder.DropTable(
                name: "FormDetails");

            migrationBuilder.DropTable(
                name: "FormHeader");

            migrationBuilder.DropTable(
                name: "FormHeaderDetails");

            migrationBuilder.DropTable(
                name: "PayGroup");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "StatusMaster");

            migrationBuilder.DropTable(
                name: "FBMUsers");
        }
    }
}
