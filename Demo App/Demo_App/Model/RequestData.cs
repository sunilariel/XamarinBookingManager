using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo_App.Model
{
    public class AssignServiceToStaff
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public string CreationDate { get; set; }
    }

    public class TimeSchedule
    {
        public string Id { get; set; }
        public int DayOfWeek { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class EmployeeWorkingHours
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
    }

    public class AssignedServiceStatus
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int DurationInMinutes { get; set; }
        public int DurationInHours { get; set; }
        public double Cost { get; set; }
        public string Currency { get; set; }
        public string CreationDate { get; set; }
        public bool Confirmed { get; set; }
        public string AllocatedServiceCount { get; set; }
    }

    public class BookAppointment
    {       
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerIdsCommaSeperated { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public bool IsAdded { get; set; }
        public string Message { get; set; }
        public string Notes { get; set; }
        public List<int> CustomerIds { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Status { get; set; }
    }

    public class UpdateBookAppointment
    {
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerIdsCommaSeperated { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public bool IsAdded { get; set; }
        public string Message { get; set; }
        public string Notes { get; set; }
        public List<int> CustomerIds { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Status { get; set; }
    }

    public class AddAppointments
    {
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double Cost { get; set; }
        public string Currency { get; set; }
        public int DurationInMinutes { get; set; }
        public int DurationInHours { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }       
    }

    public class UpdateAppointments
    {
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double Cost { get; set; }
        public string Currency { get; set; }
        public int DurationInMinutes { get; set; }
        public int DurationInHours { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BookingDate { get; set; }
        public string TimePeriod { get; set; }
        public string CommentNotes { get; set; }
    }

    public class AppointmentDetails
    {
        public string BookingId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int DurationInMinutes { get; set; }
        public int DurationInHours { get; set; }
        public double Cost { get; set; }
        public string Currency { get; set; }
        public string Colour { get; set; }
        public int status { get; set; }
        public int CustomerId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DurationHrsMin { get; set; }
        public string BookingDate { get; set; }
        public string AppointmentDetail { get; set; }
        public string CommentNotes { get; set; }
        public string TimePeriod { get; set; }


    }

    public class Notes
    {
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string WhoAddedThis { get; set; }
        public string CreationDate { get; set; }
    }

    public class WorkingHoursofEmployee
    {
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public string DateOfBooking { get; set; }
        public string Day { get; set; }

    }

    public class AllAppointments
    {
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }
        public AssignProvider Employee { get; set; }

        public RequestAddService Service { get; set; }

        public List<int> CustomerIds { get; set; }
        public int Status { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string BookingDate { get; set; }
        public string Notes { get; set; }


    }

    public class RequestAddService
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public int DurationInMinutes { get; set; }

        public int DurationInHours { get; set; }

        public float Cost { get; set; }

        public string Currency { get; set; }

        public string Colour { get; set; }

        public string CreationDate { get; set; }
    }


    public class AssignProvider :INotifyPropertyChanged
    {
        bool _confirmed;
        bool _allconfirmed;


        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string TelephoneNo { get; set; }

        public string CreationDate { get; set; }

        public bool AllConfirmed
        {
            get
            {
                return _allconfirmed;
            }
            set
            {
                if (_allconfirmed != value)
                {
                    _allconfirmed = value;
                    OnPropertyChanged("AllConfirmed");
                }
            }
        }

        public bool confirmed{
            get
            {
                return _confirmed;
            }
            set
            {
                if (_confirmed != value)
                {
                    _confirmed = value;
                    OnPropertyChanged("confirmed");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }










    public class ReqWorkingHours
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string NameOfDay { get; set; }
        public bool IsOffAllDay { get; set; }
        public string CreationDate { get; set; }
    }


    public class Staff
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string TelephoneNo { get; set; }

        public string CreationDate { get; set; }

        //public List<StaffWorkingHours> WorkingHours { get; set; }

        //public int EntityStatus { get; set; }

    }

    public class Response
    {
        public string Success { get; set; }

        public string Message { get; set; }

        public string ReturnObject { get; set; }
    }

}
