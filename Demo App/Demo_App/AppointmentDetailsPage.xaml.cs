using Demo_App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
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
        int CategoryId;
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

        private void EditServiceForAppointmentClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory(CategoryId, objCust,"EditServiceForAppointment"));
        }

        //private void EditStaffForAppointmentClick(object sender,EventArgs e)
        //{
        //    Navigation.PushAsync(new SelectStaffForAppointmentPage(objservice, objCust, "EditStaffForAppointment"));
        //}

        public string DeleteAppointment()
        {
            string apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/DeleteBooking?bookingId=" + obj.BookingId;

            var result = PostData("DELETE", "", apiUrl);
            return result;
        }

        public string PostData(string Method, string SerializedData, string Url)
        {
            try
            {
                var result = "";
                HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(Url);
                httpRequest.Method = Method;
                httpRequest.ContentType = "application/json";
                httpRequest.ProtocolVersion = HttpVersion.Version10;
                httpRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                if (SerializedData != null)
                {
                    var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                    streamWriter.Write(SerializedData);
                    streamWriter.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var StreamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return result = StreamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}