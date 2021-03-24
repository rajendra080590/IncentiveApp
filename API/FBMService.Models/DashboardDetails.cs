using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class DashboardDetails
    {
        public string FormHeaderId { get; set; }
        public string BranchId { get; set; }

        public string BranchName { get; set; }

        public string WeekId { get; set; }

        public string SubmittedBy { get; set; }

        public string SubmitterEmail { get; set; }

        public int TotalEmployee { get; set; }

        public int Incentivised { get; set; }

        public double TotalAmount { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ApproverEmailId { get; set; }

        public string ApprovalStage { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime ApprovedOn { get; set; }

        public int RejectionLevel { get; set; }

        public string RejectedBy { get; set; }

        public DateTime RejectedOn { get; set; }

        public string Comments { get; set; }

        public int CreatedBy { get; set; }

    }
}
