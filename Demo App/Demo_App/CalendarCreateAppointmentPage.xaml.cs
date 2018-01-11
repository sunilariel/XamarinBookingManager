using Demo_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarCreateAppointmentPage : ContentPage
	{
        #region GloblesFields       
        string day = "";
        DateTime dateOfBooking;       
        //public BookAppointment objbookAppointment = null;
        //public AddAppointments obj = null;
        Dictionary<string, int> Data = null;
        int StatusId;
        int CategoryId;
        #endregion

        public CalendarCreateAppointmentPage (Customer objCust,AddAppointments objAddAppointment)
		{
			InitializeComponent ();
            CustName.Text = objCust.FirstName;
            CustEmail.Text = objCust.Email;
            CustPhoneNo.Text = objCust.TelephoneNo;
            BindingContext = objAddAppointment;
            Data = new Dictionary<string, int>
            {
               { "No Label", 0 }, { "Pending", 1 }, { "Confirmed", 2 }, { "Done", 3 },
               { "No-Show", 4}, { "Paid", 5 },{ "Running Late", 6 }, { "Custom Label", 7 },
            };

            foreach (var item in Data.Keys)
            {
                newAppointmentsPicker.Items.Add(item);
            }
            newAppointmentsPicker.SelectedIndex = 0;
        }
	}
}