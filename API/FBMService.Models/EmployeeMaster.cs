using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class EmployeeMaster
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        public string FBMId { get; set; }

        public int SupervisorId { get; set; }

        public string PrimaryHCN { get; set; }

        public string PrimaryJob { get; set; }

        public string PrimaryRegion { get; set; }

        public string PrimaryDepartment { get; set; }

        public string PayGroup { get; set; }

        public string PayFrequency { get; set; }

        public  string Status { get; set; }

    }
}
