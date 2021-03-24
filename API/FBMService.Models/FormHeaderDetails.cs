using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class FormHeaderDetails
    {
        [Key]
        public int Id { get; set; }
        public string FormHeaderId { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }

        public int? ApproverUserId { get; set; }

        public string ApproverEmailId { get; set; }

        public string ApprovalStage { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public int? SubmittedBy { get; set; }

        public string Comments { get; set; }

        public int? IsActive { get; set; }
    }
}
