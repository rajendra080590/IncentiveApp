using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class CreatorUserDetails
    {
        public int UserId { get; set; }

        public string SubmittedBy { get; set; }

        public string SubmitterEmail { get; set; }

        public int CreatedBy { get; set; }

        public string BranchId { get; set; }

        public string FormHeaderId { get; set; }

        public string WeekId { get; set; }
    }
}
