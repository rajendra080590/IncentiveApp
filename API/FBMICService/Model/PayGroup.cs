using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class PayGroup
    {
        public int id { get; set; }
        public int PayGroupId { get; set; }

        public string Description { get; set; }

        public string PayGroupName { get; set; }
    }
}
