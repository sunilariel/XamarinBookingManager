using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo_App.Model
{
    public class Service
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
        public string Colour { get; set; }
        public int Buffer { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
    }


    public class AssignedServicetoStaff : INotifyPropertyChanged
    {
        bool _isAssigned;
        bool _AllAssigned;
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int DurationInMinutes { get; set; }
        public int DurationInHours { get; set; }
        public double Cost { get; set; }
        public string Currency { get; set; }
        public string Colour { get; set; }
        public int Buffer { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
        //public bool isAssigned { get; set; }
        public string ServiceDetails { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public bool AllAssigned
        {
            get
            {
                return _AllAssigned;
            }
            set
            {
                if (_AllAssigned != value)
                {
                    _AllAssigned = value;
                    OnPropertyChanged("AllAssigned");
                }
            }
        }

        public bool isAssigned
        {
            get
            {
                return _isAssigned;
            }
            set
            {
                if (_isAssigned != value)
                {
                    _isAssigned = value;
                    OnPropertyChanged("isAssigned");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ServiceDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
        public string DurationInMinutes { get; set; }
        public string BufferTimeInMinutes { get; set; }
        public string ServiceProviders { get; set; }
        public string Cost { get; set; }
    }

    public class ServicesAllocatedToCategory
    {
        public string AllocatedServiceCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
