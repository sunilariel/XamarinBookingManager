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

    public class ProviderWorkingHours
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int NameOfDay { get; set; }
        public string NameOfDayAsString { get; set; }
        public bool IsOffAllDay { get; set; }
        public string CreationDate { get; set; }
        public string EntityStatus { get; set; }
    }

    public class BussinessHours
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string NameOfDay { get; set; }
        public bool IsOffAllDay { get; set; }
    }

    public class CompanyBusinessHours
    {
        //public TimeSpan Start { get; set; }
        //public TimeSpan End { get; set; }
        //public string NameOfDay { get; set; }
        //public bool IsOffAllDay { get; set; }
        public List<CompanyWorkingHours> CompanyWorkingHours { get; set; }
    }
    public class CompanyWorkingHours
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string NameOfDay { get; set; }        
        public bool IsOffAllDay { get; set; }
        public string CreationDate { get; set; }
        public string EntityStatus { get; set; }        
    }

    public class CWorkingHours
    {
        public string Url { get; set; }

        public List<CReqWorkingHours> CReqWorkingHours { get; set; }
    }
    public class CReqWorkingHours
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string NameOfDay { get; set; }
        public bool IsOffAllDay { get; set; }
        public string CreationDate { get; set; }
    }
}
