using FBMICService.Interfaces;
using FBMICService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Data.Repo
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext _dc;

        public BranchRepository(DataContext dc)
        {
            this._dc = dc;
        }
        public void AddBranch(Branch branch)
        {
            _dc.Branches.AddAsync(branch);        }

        public void deleteBranch(int id)
        {
            var branch = _dc.Branches.Find(id);
            _dc.Branches.Remove(branch);
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync()
        {
            return await _dc.Branches.ToListAsync();
        }

        //public async Task<bool> SaveAsync()
        //{
        //    return await _dc.SaveChangesAsync() > 0;
        //}
    }
}
