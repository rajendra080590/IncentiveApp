using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        public int TotalEmployee { get; set; }

        public bool Status { get; set; }
    }
}
