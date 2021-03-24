using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class AuditTrail
    {
        public string FormHeaderId  { get; set; }

        public int UserId { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string ApproverEmailId { get; set; }
    }
}
