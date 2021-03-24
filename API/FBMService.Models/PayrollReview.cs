using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class PayrollReview
    {
        public int Option { get; set; }
        public string BranchId { get; set; }

        public string BranchName { get; set; }

        public string FormHeaderId { get; set; }

        public int TotalEmployee { get; set; }

        public int Incentivized { get; set; }

        public float TotalAmount { get; set; }

        public string  WeekId { get; set; }

        public string StateCode { get; set; }

        public int? RegionCode { get; set; }

        public string Region { get; set; }

        public string SubmittedBy { get; set; }

        public string SubmittedOn { get; set; }

        public string PayGroup { get; set; }

        public int? StatusId { get; set; }

        public int CreatedBy { get; set; }
    }
}
