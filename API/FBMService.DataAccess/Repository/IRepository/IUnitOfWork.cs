using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ILoginRepository Login { get; }

        IBranchRepository Branch { get; }

        IDashboardRepository Dashboard { get; }

        IApproverRepository Approver { get; }

        IFBMApproverRepository FBMApprover { get; }

        IFBMConfigurationRepository FBMConfiguration { get; }

        IFormHeaderRepository FormHeader { get; }

        ISP_Call SP_Call { get; }

        void Save();
    }
}
