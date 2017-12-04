using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
    public class StaffWorkingHours
    {      
            public int Id { get; set; }
            public int CompanyId { get; set; }
            public int EmployeeId { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int NameOfDay { get; set; }
            public string NameOfDayAsString { get; set; }
            public bool IsOffAllDay { get; set; }
            public string CreationDate { get; set; }
            public string EntityStatus { get; set; }
    }

   
}
