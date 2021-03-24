using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class NotifyUserDetails
    {
        public string BranchId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string EmailId { get; set; }

        public string UserType { get; set; }

        public string WeekId { get; set; }
    }
}
