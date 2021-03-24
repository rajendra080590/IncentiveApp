using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class FormDetails
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FormDetailId { get; set; }

        public int FormHeaderId { get; set; }

        public int EmployeeId { get; set; }

        public Double IncentiveAmount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public int? SubmittedBy { get; set; }
    }
}
