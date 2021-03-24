using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class FormDetails
    {
        [Key]
        public int Id { get; set; }

        public string FormDetailId { get; set; }

        public string FormHeaderId { get; set; }

        public int EmployeeId { get; set; }

        public Double IncentiveAmount { get; set; }

        public string Comments { get; set; }

        public string FileName { get; set; }



    }
}
