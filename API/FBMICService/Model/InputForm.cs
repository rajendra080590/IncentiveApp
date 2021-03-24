using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Model
{
    public class InputForm
    {
        public string FBMId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Location { get; set; }

        public string PayGroup { get; set; }

        public string FormHeaderId { get; set; }

        public string WeekId { get; set; }

        public double IncentiveAmount { get; set; }
    }
}
