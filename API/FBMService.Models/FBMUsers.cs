using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Models
{
    public class FBMUsers
    {
        [Key]
        public int UserId { get; set; }

        
        public int? RoleId { get; set; }

        [Required]
        [MaxLength(40)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        
        public string EmailId { get; set; }

        
        public string BranchId { get; set; }

        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Status { get; set; }

    }
}
