using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class FormHeader
    {
        [Key]
        public int Id { get; set; }
        public string FormHeaderId { get; set; }

        public string BranchId { get; set; }

        public string WeekId { get; set; }

        public int StatusId { get; set; }

        public int? ApproverId { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? TotalEmployee { get; set; }

        public int? Incentivised { get; set; }

        public double? TotalAmount { get; set; }

        public DateTime? IncentiveWeekDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? PrimaryApproverExpiryDate { get; set; }

        public string Comments { get; set; }

        public string FileName { get; set; }

        public int? IsPrimaryApproverActive { get; set; }
    }
}
