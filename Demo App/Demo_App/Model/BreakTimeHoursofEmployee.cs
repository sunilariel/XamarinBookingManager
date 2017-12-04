using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
    public class BreakTimeHoursofEmployee
    {
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int DayOfWeek { get; set; }
        public string Day { get; set; }
        public List<TimeSchedule> StartEndTime { get; set; }
        public bool Available { get; set; }
        public string CreationDate { get; set; }
    }
}
