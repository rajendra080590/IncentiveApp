using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Interfaces
{
    public interface IUnitOfWork2
    {
        IBranchRepository BranchRepository { get; }

        ILoginRepository LoginRepository { get; }
        Task<bool> SaveAync();
    }
}
