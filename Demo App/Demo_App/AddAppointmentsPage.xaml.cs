using Demo_App.Model;
using Newtonsoft.Json;
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
    public partial class AddAppointmentsPage : ContentPage
    {
        #region GloblesVariables
        int CategoryId;
        string PageName = "";
        public Customer objCust = null;
        public AppointmentDetails obj = null;
        public Notes objNotes = null;
        public BookAppointment objBookAppointment = null;
        ObservableCollection<AppointmentDetails> ListofAppointment = new ObservableCollection<AppointmentDetails>();
        #endregion

        public AddAppointmentsPage(BookAppointment objAppointment)
        {
            try
            {
                InitializeComponent();
                GetSelectedCustomerById();
                BindingContext = objCust;
                this.Title = objCust.FirstName + "'s" + " appointments";
                if (objAppointment != null)
                {
                    objBookAppointment = new BookAppointment();
                    objBookAppointment.CompanyId = objAppointment.CompanyId;
                    objBookAppointment.EmployeeId = objAppointment.EmployeeId;
                    objBookAppointment.ServiceId = objAppointment.ServiceId;
                    objBookAppointment.CustomerIdsCommaSeperated = objAppointment.CustomerIdsCommaSeperated.ToString();
                    objBookAppointment.StartHour = objAppointment.StartHour;
                    objBookAppointment.StartMinute = objAppointment.StartMinute;
                    objBookAppointment.EndHour = objAppointment.EndHour;
                    objBookAppointment.EndMinute = objAppointment.EndMinute;
                    objBookAppointment.IsAdded = objAppointment.IsAdded;
                    objBookAppointment.Message = objAppointment.Message;
                    objBookAppointment.Notes = objAppointment.Notes;
                    objBookAppointment.CustomerIds = objAppointment.CustomerIds;
                    objBookAppointment.Start = objAppointment.Start;
                    objBookAppointment.End = objAppointment.End;
                    objBookAppointment.Status = objAppointment.Status;
                }
                GetAppointmentDetails();
            }
            catch(Exception e)
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

        private void AppointmentsdetailsClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var data = e.SelectedItem as AppointmentDetails;
                string bookingdate = data.BookingDate.Split(',')[0];
                obj = new AppointmentDetails();
                DateTime startTime = Convert.ToDateTime(data.StartTime);
                string TimeStart = startTime.ToShortTimeString();
                DateTime endTime = Convert.ToDateTime(data.EndTime);
                string TimeEnd = endTime.ToShortTimeString();
                string TimePeriod = TimeStart + "-" + TimeEnd;
                obj.BookingId = data.BookingId;
                obj.EmployeeId = data.EmployeeId.ToString();
                obj.ServiceId = data.ServiceId.ToString();
                obj.EmployeeName = (data.EmployeeName) == null ? "" : data.EmployeeName;
                obj.ServiceName = (data.ServiceName) == null ? "" : data.ServiceName;
                obj.DurationInHours = (data.DurationInHours) == null ? 0 : data.DurationInHours;
                obj.DurationInMinutes = (data.DurationInMinutes) == null ? 0 : data.DurationInMinutes;
                obj.Cost = (data.Cost) == null ? 0 : data.Cost;
                obj.Currency = (data.Currency) == null ? "" : data.Currency;
                obj.status = data.status;
                obj.StartTime = data.StartTime;
                obj.EndTime = data.EndTime;
                obj.BookingDate = bookingdate;
                obj.Colour = (data.Colour) == null ? "" : data.Colour;
                obj.DurationHrsMin = data.DurationHrsMin;
                obj.AppointmentDetail = data.AppointmentDetail;
                obj.CommentNotes = data.CommentNotes;
                obj.TimePeriod = TimePeriod;
                Navigation.PushAsync(new AppointmentDetailsPage(obj));
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }


        private void AddAppointmentsClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory(PageName));
        }

        public ObservableCollection<AppointmentDetails> GetAppointmentDetails()
        {
            try
            {
                string[] StartTime = { };
                var startDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(-1)).ToString("dd-MM-yyyy");

                var endDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(1)).ToString("dd-MM-yyyy");


                string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetBookingsForCustomerByIdBetweenDates?customerId=" + objCust.Id + "&startDate=" + startDate + "&endDate=" + endDate;
                
                var result = PostData("GET", "", apiURL);

                ObservableCollection<AllAppointments> appointments = JsonConvert.DeserializeObject<ObservableCollection<AllAppointments>>(result);
                ListofAppointment = new ObservableCollection<AppointmentDetails>();
                foreach (var appointment in appointments)
                {
                    //string DurationHours = "0";
                    //if(appointment.Service.DurationInMinutes != null && appointment.Service.DurationInMinutes != 0)
                    string DurationHours = Convert.ToString(appointment.Service.DurationInMinutes == null ? 0 : appointment.Service.DurationInMinutes / 60);

                    string durmin = Convert.ToString(appointment.Service.DurationInMinutes == null ? 0 : appointment.Service.DurationInMinutes % 60); ;
                    string durhrs = DurationHours + "hrs";
                    string durationMins = durmin + "mins";
                    string Duration = durhrs + " " + durationMins;
                    var datebooking = appointment.Start; 
                     var DateOFbooking = Convert.ToDateTime(datebooking).ToString("dd-MMM-yyyy");
                    string detail = appointment.Employee.FirstName + "," + appointment.Service.Name + "," + Duration + "," + appointment.Service.Cost;
                    DateTime startTime = Convert.ToDateTime(appointment.Start);
                    string Time = startTime.ToShortTimeString();
                    var DateTimeofBooking = DateOFbooking + "," + Time;
                    obj = new AppointmentDetails();
                    obj.BookingId = appointment.Id;
                    obj.EmployeeId = appointment.EmployeeId.ToString();
                    obj.ServiceId = appointment.ServiceId.ToString();
                    obj.EmployeeName = (appointment.Employee) == null ? "" : appointment.Employee.FirstName;
                    obj.ServiceName = (appointment.Service) == null ? "" : appointment.Service.Name;
                    obj.DurationInHours = (appointment.Service) == null ? 0 : appointment.Service.DurationInHours;
                    obj.DurationInMinutes = (appointment.Service) == null ? 0 : appointment.Service.DurationInMinutes;
                    obj.Cost = (appointment.Service) == null ? 0 : appointment.Service.Cost;
                    obj.Currency = (appointment.Service) == null ? "" : appointment.Service.Currency;
                    obj.status = appointment.Status;
                    obj.StartTime = appointment.Start;
                    obj.EndTime = appointment.End;
                    obj.BookingDate = DateTimeofBooking;
                    obj.Colour = (appointment.Service) == null ? "" : appointment.Service.Colour;
                    obj.DurationHrsMin = Duration;
                    obj.AppointmentDetail = detail;
                    obj.CommentNotes = appointment.Notes;

                    ListofAppointment.Add(obj);

                }
               
            }
            catch (Exception e)
            {

            }

            CustomerAppoimentList.ItemsSource = ListofAppointment;
            return ListofAppointment;
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