using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class ApproverMaster
    {
        [Key]
        public int Id { get; set; }

        public string BranchId { get; set; }

        public int? ApprovalLevel  { get; set; }

        public int? ApproverId { get; set; }

        public string ApprovalStage { get; set; }

    }
}
