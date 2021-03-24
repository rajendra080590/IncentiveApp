using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class AuditTrail
    {
        public string FormHeaderId { get; set; }

        public string ApprovalStage { get; set; }

        public string FormStatus { get; set; }

        public string SubmittedBy { get; set; }

        public int UserId { get; set; }
        public DateTime SubmittedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }
        public string Comments { get; set; }
        public string ApproverEmailId { get; set; }

        public string BranchId { get; set; }
    }
}
