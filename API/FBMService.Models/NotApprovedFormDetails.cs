using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class NotApprovedFormDetails
    {
        public string FormHeaderId { get; set; }

        public string WeekId { get; set; }

        public string BranchId { get; set; }

        public string PrimaryApproverEmailId { get; set; }

        public string SecondaryApproverEmailId { get; set; }

        public int SecondaryApproverUserId { get; set; }


    }
}
