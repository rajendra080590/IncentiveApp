using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FBMICService.Models
{
    public class UserRoleMapping
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
