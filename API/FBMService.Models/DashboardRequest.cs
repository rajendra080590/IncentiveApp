using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class DashboardRequest
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int Status { get; set; }

        public string BranchId { get; set; }
    }
}
