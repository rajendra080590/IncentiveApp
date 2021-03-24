using FBMICService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Interfaces
{
    public interface ILoginRepository
    {
        Task<IEnumerable<FBMUsers>> ValidUser(string Username, string Password);
    }
}
