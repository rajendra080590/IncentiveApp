using Microsoft.EntityFrameworkCore.Migrations;

namespace FBMICService.Migrations
{
    public partial class FBM_Branch_EmployeeMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchMaster",
                columns: table => new
                {
                    SXId = table.Column<int>(nullable: false),
                    BranhcId = table.Column<string>(nullable: false),
                    Division = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(maxLength: 50, nullable: false),
                    Addess = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    StProv = table.Column<string>(nullable: true),
                    ZipPostal = table.Column<string>(nullable: true),
                    BranchManager = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    TollFree = table.Column<string>(nullable: true),
                    HouseOfOperation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    FBMDate = table.Column<int>(nullable: false),
                    DateOpen = table.Column<int>(nullable: false),
                    DateClose = table.Column<int>(nullable: false),
                    RegionCode = table.Column<int>(nullable: false),
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
                    Update_Fix = table.Column<string>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false)
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
                    FBMId = table.Column<int>(nullable: false),
                    SupervisorId = table.Column<int>(nullable: false),
                    PrimaryHCN = table.Column<int>(nullable: false),
                    PrimaryJob = table.Column<string>(nullable: true),
                    PrimaryRegion = table.Column<string>(nullable: true),
                    PrimaryDepartment = table.Column<string>(nullable: true),
                    PayGroup = table.Column<string>(nullable: true),
                    PayFrequency = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMaster", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "FBMUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EmailId = table.Column<string>(nullable: false),
                    BranchId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FBMUsers", x => x.UserId);
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
                name: "BranchMaster");

            migrationBuilder.DropTable(
                name: "BranchUserMapping");

            migrationBuilder.DropTable(
                name: "EmployeeMaster");

            migrationBuilder.DropTable(
                name: "FBMUsers");
        }
    }
}
