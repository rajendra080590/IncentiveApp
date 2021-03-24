using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class ReminderDetails
    {
        public string BranchId { get; set; }

        public string BranchName { get; set; }

        public string PreparerEmailId { get; set; }

        public string PreparerName { get; set; }

        public int PreparerUserId { get; set; }

        public string EmailBody { get; set; }

        public int BranchApproverUserId { get; set; }

        public string BranchApproverName { get; set; }

        public string BranchApproverEmail { get; set; }
    }
}
