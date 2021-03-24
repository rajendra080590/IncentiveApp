using FBMICService.DataAccess.Data;
using FBMICService.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Login = new LoginRepository(_db);
            Branch = new BranchRepository(_db);
            Approver = new ApproverRepository(_db);
            FBMApprover = new FBMApproverRepository(_db);
            FBMConfiguration = new FBMConfigurationRepository(_db);
            FormHeader = new FormHeaderRepository(_db);
            NewForm = new NewFormRepository(_db);
            Dashboard = new DashboardRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ILoginRepository Login { get; private set; }
        public IBranchRepository Branch { get; private set; }

        public IFormHeaderRepository FormHeader { get; private set; }

        public IApproverRepository Approver { get; private set; }

        public IFBMApproverRepository FBMApprover { get; private set; }

        public IFBMConfigurationRepository FBMConfiguration { get; private set; }
        public INewFormRepository NewForm { get; private set; }
        public IDashboardRepository Dashboard { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
