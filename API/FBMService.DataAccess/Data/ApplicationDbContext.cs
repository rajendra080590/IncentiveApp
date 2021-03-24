using FBMICService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<Branch> Branches { get; set; }

        public DbSet<StatusMaster> StatusMaster { get; set; } 

        public DbSet<RoleMaster> RoleMaster { get; set; }

        public DbSet<PayGroup> PayGroup { get; set; }

        public DbSet<FBMConfiguration> FBMConfiguration { get; set; }

        public DbSet<ApproverMaster> ApproverMaster { get; set; }

        public DbSet<BranchMaster> BranchMaster { get; set; }

        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }

        public DbSet<FormHeader> FormHeader { get; set; }

        public DbSet<FormHeaderDetails> FormHeaderDetails { get; set; }

        public DbSet<FormDetails> FormDetails { get; set; }

        public DbSet<FBMUsers> FBMUsers { get; set; }

        public DbSet<BranchUserMapping> BranchUserMapping { get; set; }

        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

        public DbSet<FBMApproverMaster> FBMApproverMaster { get; set; }

        public DbSet<IncentiveAppCalendar> IncentiveAppCalendar { get; set; }

        public DbSet<PayrollReviewed> PayrollReviewed { get; set; }
    }
}
