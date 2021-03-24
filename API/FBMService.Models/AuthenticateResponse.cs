using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string BranchId { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(FBMUsers user, string token)
        {
            Id = user.UserId;
            RoleId = Convert.ToInt32(user.RoleId);
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            BranchId = user.BranchId;
            Token = token;
        }
    }
}
