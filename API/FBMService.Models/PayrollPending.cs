using System;
using System.Collections.Generic;
using System.Text;

namespace FBMICService.Models
{
    public class PayrollPending
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string Region { get; set; }

        public string PayFrequency { get; set; }

        public float IncentiveAmount { get; set; }

        public float EarnAmt { get; set; }

        public string FormDetailId { get; set; }

        public string FormHeaderId { get; set; }

        public int StatusId { get; set; }

        public string BranchId { get; set; }

        public int UserId { get; set; }

        public string Commission { get; set; }

        public string PayGroupCode { get; set; }

        public string LocationCode { get; set; }

        public string CompanyCode { get; set; }

        public string EarnCode { get; set; }

        public string EmptyColumn { get; set; }

        public string BlankColumn { get; set; }

        public string WeekId { get; set; }

        public int? Option { get; set; }

        public int? RegionCode { get; set; }

        public string StateCode { get; set; }
    }
}
