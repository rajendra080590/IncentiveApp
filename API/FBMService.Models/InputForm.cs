using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FBMICService.Models
{
    public class InputForm
    {
        public string FormHeaderId { get; set; }

        public string FormDetailId { get; set; }

        public string BranchId { get; set; }

        public string WeekId { get; set; }

        public int StatusId { get; set; }

        public int ApproverId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

        public int EmployeeId { get; set; }

        public Double IncentiveAmount { get; set; }

        public int UserId { get; set; }

        public string Comments { get; set; }

        public string FileName { get; set; }

        public string FBMId { get; set; }

        public string EmployeeName { get; set; }

        public string Location { get; set; }

        public string PayGroup { get; set; }

        public int TotalEmployee { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime IncentiveWeekDate { get; set; }

        public string Attachment { get; set; }

        public string FormHeaderComments { get; set; }

        public int RoleId { get; set; }

        public int ToDelete { get; set; }

        public int CreatedBy { get; set; }

        public string PrimaryJob { get; set; }
    }
}
