using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class FormHeaderDetails
    {
        [Key]
        public int Id { get; set; }
        public string FormHeaderId { get; set; }
        [ForeignKey("FormHeaderId")]

        public FormHeader FormHeader { get; set; }

        public int UserId { get; set; }

        public int? ApproverId { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? ApproverLevel { get; set; }

        public int? RejectionLevel { get; set; }
    }
}
