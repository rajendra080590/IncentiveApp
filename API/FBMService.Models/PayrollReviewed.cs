using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FBMICService.Models
{
    public class PayrollReviewed
    {
        [Key]
        public int Id { get; set; } 

        public string FormHeaderId { get; set; }

        public string  WeekId { get; set; }

        public string BranchId { get; set; }

        public DateTime PayrollDate { get; set; }

        public string Comments { get; set; }

        public int? SubmittedBy { get; set; }

        public DateTime SubmittedOn { get; set; }

        public int? StatusId { get; set; }
    }
}
