using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class ReviewerAction
    {
        public string FormHeaderId { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string BranchId { get; set; }

        public string WeekId { get; set; }

        public string Comments { get; set; }

        public string SubmittedBy { get; set; }

        public string SubmitterEmail { get; set; }

        public int CreatedBy { get; set; }
    }
}
