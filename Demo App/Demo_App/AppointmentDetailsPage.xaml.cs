using Demo_App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentDetailsPage : ContentPage
	{
        public Customer objCust = null;
        public AppointmentDetails obj = null;
        public AppointmentDetailsPage (Customer Cust, AppointmentDetails appointment)
		{
			InitializeComponent ();
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            AppointmentCustomerName.Text = objCust.FirstName;
            AppointmentCustomerEmail.Text = objCust.Email;
            AppointmentCustomerMobNo.Text = objCust.TelephoneNo;
            obj = new AppointmentDetails();
            obj.BookingId = appointment.BookingId;
            obj.EmployeeId = appointment.EmployeeId;
            obj.ServiceId = appointment.ServiceId;
            obj.EmployeeName = appointment.EmployeeName;
            obj.ServiceName = appointment.ServiceName;
            obj.DurationInHours = appointment.DurationInHours;
            obj.DurationInMinutes = appointment.DurationInMinutes;
            obj.Cost = appointment.Cost;
            obj.Currency = appointment.Currency;
            obj.status = appointment.status;
            obj.StartTime = appointment.StartTime;
            obj.EndTime = appointment.EndTime;
            obj.BookingDate = appointment.BookingDate;
            obj.Colour = appointment.Colour;
            obj.DurationHrsMin = appointment.DurationHrsMin;
            obj.AppointmentDetail = appointment.AppointmentDetail;
            obj.CommentNotes = appointment.CommentNotes;
            BindingContext = obj;
            string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };
            for (var i = 0; i < Data.Length; i++)
            {
                AppointmentsPicker.Items.Add(Data[i]);
            }
        }
	}
}