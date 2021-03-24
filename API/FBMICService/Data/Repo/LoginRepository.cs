using FBMICService.Interfaces;
using FBMICService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Data.Repo
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext dc;
        public IEnumerable<FBMUsers> fbmUsers { get; set; }

        public LoginRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public  Task<IEnumerable<FBMUsers>> ValidUser(string Username, string Password)
        {
            var isValidUser = dc.FBMUsers.FirstOrDefault(u => u.UserName == Username && u.Password == Password);
            if (isValidUser == null)
            {
                return null;
            }
            //fbmUsers = IEnumerable<FBMUsers>isValidUser;
            return  null;
        }
    }
}
