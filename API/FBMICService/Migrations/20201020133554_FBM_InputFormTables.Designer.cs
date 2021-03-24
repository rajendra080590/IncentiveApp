﻿// <auto-generated />
using System;
using FBMICService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FBMICService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201020133554_FBM_InputFormTables")]
    partial class FBM_InputFormTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FBMICService.Model.ApproverMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApprovalLevel")
                        .HasColumnType("int");

                    b.Property<int>("ApproverId")
                        .HasColumnType("int");

                    b.Property<string>("BranchId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApproverMaster");
                });

            modelBuilder.Entity("FBMICService.Model.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TotalEmployee")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("FBMICService.Model.BranchMaster", b =>
                {
                    b.Property<string>("SXId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ADPCo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Addess")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Asset_Stock")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchManager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DSIContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateClose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOpen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistrictManager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("E_19Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FBMDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HRGeneralist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoursOfOperation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KelmarContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Legacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetroDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OprtaionalManager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherKeyContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionalVP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SafetyCordinator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StProv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubsidiaryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TollFree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateFix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipPostal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SXId");

                    b.ToTable("BranchMaster");
                });

            modelBuilder.Entity("FBMICService.Model.BranchUserMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BranchUserMapping");
                });

            modelBuilder.Entity("FBMICService.Model.EmployeeMaster", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FBMId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayFrequency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryHCN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("EmployeeMaster");
                });

            modelBuilder.Entity("FBMICService.Model.FBMConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FBMKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FBMValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FBMConfiguration");
                });

            modelBuilder.Entity("FBMICService.Model.FBMUsers", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("UserId");

                    b.ToTable("FBMUsers");
                });

            modelBuilder.Entity("FBMICService.Model.FormDetails", b =>
                {
                    b.Property<int>("FormDetailId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("FormHeaderId")
                        .HasColumnType("int");

                    b.Property<double>("IncentiveAmount")
                        .HasColumnType("float");

                    b.Property<int>("SubmittedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FormDetailId");

                    b.ToTable("FormDetails");
                });

            modelBuilder.Entity("FBMICService.Model.FormHeader", b =>
                {
                    b.Property<string>("FormHeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ApprovedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApproverId")
                        .HasColumnType("int");

                    b.Property<string>("BranchId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("WeekId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormHeaderId");

                    b.ToTable("FormHeader");
                });

            modelBuilder.Entity("FBMICService.Model.FormHeaderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApproverId")
                        .HasColumnType("int");

                    b.Property<string>("FormHeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormHeaderId");

                    b.ToTable("FormHeaderDetails");
                });

            modelBuilder.Entity("FBMICService.Model.PayGroup", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PayGroupId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PayGroup");
                });

            modelBuilder.Entity("FBMICService.Model.RoleMaster", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("RoleMaster");
                });

            modelBuilder.Entity("FBMICService.Model.StatusMaster", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("StatusMaster");
                });

            modelBuilder.Entity("FBMICService.Model.BranchUserMapping", b =>
                {
                    b.HasOne("FBMICService.Model.FBMUsers", "FBMUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FBMICService.Model.FormHeaderDetails", b =>
                {
                    b.HasOne("FBMICService.Model.FormHeader", "FormHeader")
                        .WithMany()
                        .HasForeignKey("FormHeaderId");
                });
#pragma warning restore 612, 618
        }
    }
}
