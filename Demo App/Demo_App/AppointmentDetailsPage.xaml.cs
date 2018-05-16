using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        #region GlobalVariables
        public Customer objCust = null;
        public AppointmentDetails obj = null;
        public Service service = null;
        public AddAppointments addAppointments = null;
        public Notes objNotes = null;
        int CategoryId;
        int ServiceID;
        //int Status;
        string ServiceName = "";
        int EmpID;
        int DurationInMinutes;
        int DurationInHours;
        string empName = "";
        double Cost;
        string Day = "";
        DateTime DateOfBooking;
        Dictionary<string, int> Data = null;
        int StatusId;
        #endregion

        public AppointmentDetailsPage(AppointmentDetails appointment)
        {
            try
            {
                InitializeComponent();
                GetSelectedCustomerById();
                objNotes = new Notes();
                objNotes.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                if (objCust != null)
                {
                    objNotes.CustomerId = objCust.Id;
                }
                objNotes.Description = CommentNotes.Text;
                Application.Current.Properties["BookingID"] = appointment.BookingId;

                DateOfBooking = Convert.ToDateTime(appointment.BookingDate);
                Day = DateOfBooking.DayOfWeek.ToString();
                ServiceID = Convert.ToInt32(appointment.ServiceId);
                ServiceName = appointment.ServiceName;
                EmpID = Convert.ToInt32(appointment.EmployeeId);
                empName = appointment.EmployeeName;
                Cost = appointment.Cost;
                DurationInHours = appointment.DurationInHours;
                DurationInMinutes = appointment.DurationInMinutes;
                DateTime startTime = Convert.ToDateTime(appointment.StartTime);
                string TimeStart = startTime.ToShortTimeString();
                DateTime endTime = Convert.ToDateTime(appointment.EndTime);
                string TimeEnd = endTime.ToShortTimeString();
                string TimePeriod = TimeStart + "-" + TimeEnd;
                StatusId = appointment.status;
                addAppointments = new AddAppointments();
                addAppointments.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                addAppointments.BookingId = appointment.BookingId;
                addAppointments.EmployeeId = EmpID;
                addAppointments.EmployeeName = empName;
                addAppointments.ServiceId = ServiceID;
                addAppointments.ServiceName = ServiceName;
                addAppointments.Cost = Cost;
                addAppointments.DurationInHours = DurationInHours;
                addAppointments.DurationInMinutes = DurationInMinutes;
                addAppointments.StartTime = appointment.StartTime;
                addAppointments.EndTime = appointment.EndTime;
                addAppointments.TimePeriod = TimePeriod;
                addAppointments.DateOfBooking = appointment.BookingDate;

                service = new Service();
                service.Id = Convert.ToInt32(appointment.ServiceId);
                service.Name = appointment.ServiceName;
                service.Cost = appointment.Cost;
                service.DurationInHours = appointment.DurationInHours;
                service.DurationInMinutes = appointment.DurationInMinutes;
                if (objCust != null)
                {
                    AppointmentCustomerName.Text = objCust.FirstName;
                    AppointmentCustomerEmail.Text = objCust.Email;
                    AppointmentCustomerMobNo.Text = objCust.TelephoneNo;
                }
                obj = new AppointmentDetails();
                obj.CustomerName = objCust.FirstName;
                obj.CustomerId = objCust.Id;
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
                obj.TimePeriod = appointment.TimePeriod;
                BindingContext = obj;

                Data = new Dictionary<string, int>
            {
               { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
               { "No-Show",5}, { "Paid",6},{ "Running Late",7}, { "Custom Label",8},
            };
                foreach (var item in Data.Keys)
                {
                    AppointmentsPicker.Items.Add(item);

                }
                obj.status = Convert.ToInt32(obj.status) - 1;
                AppointmentsPicker.SelectedIndex = obj.status;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void GetSelectedCustomerById()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetCustomerById?id=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objCust = JsonConvert.DeserializeObject<Customer>(result);
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(DateOfBooking).ToString();
            Navigation.PushAsync(new SelectServiceCategory("EditAppointment", DateofBooking,StatusId));
        }

        private void UpdateAppointmentbyBookingDateClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(DateOfBooking).ToString();
            //var DateofBooking = Convert.ToDateTime(selectedD).ToString();
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceID, ServiceName, EmpID, empName, Cost, DurationInHours, DurationInMinutes, "EditAppointment", DateofBooking, StatusId));
        }

        private void UpdateAppointmentbyStaffClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(DateOfBooking).ToString();
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, "EditAppointment", DateofBooking,StatusId));
        }

        private void EditCommentClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new CustomerCommentsForAppointmentPage(addAppointments, objCust, Day, DateOfBooking, "EditAppointment"));
            Navigation.PushAsync(new AddNotesPage());
        }

        public string DeleteAppointment()
        {
            string apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/DeleteBooking?bookingId=" + obj.BookingId;

            var result = PostData("DELETE", "", apiUrl);
            return result;
        }

        public string SetStatusOfAppointment()
        {
            try
            {
                string selectedValue = (AppointmentsPicker.SelectedItem).ToString();
                Data.TryGetValue(selectedValue, out StatusId);
                string apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/SetStatus?status=" + StatusId + "&bookingId=" + Application.Current.Properties["BookingID"];
                string result = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));
                httpWebRequest.ContentLength = 0;

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                //var DateofBooking = Convert.ToDateTime(DateOfBooking);
                //Navigation.PushAsync(new UpdateAppointmentDetailsPage(addAppointments,Day, DateofBooking));
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
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

                if (SerializedData != "")
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