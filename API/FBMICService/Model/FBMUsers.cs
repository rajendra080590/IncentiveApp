using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class FBMUsers
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int? RoleId { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string BranchId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Status { get; set; }

    }
}
