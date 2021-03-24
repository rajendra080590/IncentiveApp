using FBMICService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetBranchesAsync();

        void AddBranch(Branch branch);

        void deleteBranch(int id);

        //Task<bool> SaveAsync();
    }
}
