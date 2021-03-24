using FBMICService.DataAccess.Data;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.DataAccess.Repository
{
    public class BranchRepository : Repository<BranchMaster>, IBranchRepository
    {
        public readonly ApplicationDbContext _db;
        public BranchRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
