using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FBMICService.Models
{
    public class IncentiveAppCalendar
    {
        [Key]
        public int Id { get; set; }
        public string PayrollWeek { get; set; }

        public DateTime PayrollDate { get; set; }

        public int WeekNumber { get; set; }

        public int Year { get; set; }

        public string WeekId { get; set; }

        public string MonthName { get; set; }

        public DateTime EmailReminder { get; set; }

        public DateTime IncentiveStart { get; set; }

        public DateTime IncentiveComplete { get; set; }


    }
}
