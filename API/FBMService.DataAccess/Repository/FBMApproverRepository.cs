using FBMICService.DataAccess.Data;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.DataAccess.Repository
{
    public class FBMApproverRepository : Repository<FBMApproverMaster>, IFBMApproverRepository
    {
        public readonly ApplicationDbContext _db;
        public FBMApproverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
