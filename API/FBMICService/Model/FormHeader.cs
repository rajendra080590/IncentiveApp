using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class FormHeader
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FormHeaderId { get; set; }

        public string BranchId { get; set; }

        public string WeekId { get; set; }

        public int StatusId { get; set; }

        public int? ApproverId { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ApproverLevel { get; set; }

        public int? RejectionLevel { get; set; }

    }
}
