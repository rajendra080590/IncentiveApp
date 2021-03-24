using FBMICService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<StatusMaster> StatusMaster { get; set; }

        public DbSet<RoleMaster> RoleMaster { get; set; }

        public DbSet<PayGroup> PayGroup { get; set; }

        public DbSet<FBMConfiguration> FBMConfiguration { get; set; }

        public DbSet<ApproverMaster> ApproverMaster { get; set; }

        public DbSet<BranchMaster> BranchMaster { get; set; } 

        public DbSet<BranchUserMapping> BranchUserMapping { get; set; }

        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }

        public DbSet<FormHeader> FormHeader { get; set; }

        public DbSet<FormHeaderDetails> FormHeaderDetails { get; set; }

        public DbSet<FormDetails> FormDetails { get; set; }

        public DbSet<FBMUsers> FBMUsers { get; set; }
    }
}
