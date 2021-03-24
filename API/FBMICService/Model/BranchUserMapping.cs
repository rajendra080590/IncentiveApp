using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class BranchUserMapping
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public FBMUsers FBMUsers { get; set; }

        public string BranchId { get; set; }
    }
}
