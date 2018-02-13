using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
   public class BreakTime
    {
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int DayOfWeek { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string CreationDate { get; set; }
    }

    public class StaffBreakTime
    {
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string NameOfDay { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string CreationDate { get; set; }
    }
}
