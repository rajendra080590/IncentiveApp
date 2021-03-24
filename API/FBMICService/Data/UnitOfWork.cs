using FBMICService.Data.Repo;
using FBMICService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Data
{
    public class UnitOfWork : IUnitOfWork2
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }
        public IBranchRepository BranchRepository =>
            new BranchRepository(dc);

        public ILoginRepository LoginRepository =>
            new LoginRepository(dc);
        public async Task<bool> SaveAync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
