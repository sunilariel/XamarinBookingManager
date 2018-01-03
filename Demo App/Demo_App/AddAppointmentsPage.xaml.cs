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
        int CategoryId;
        string PageName = "";
        public Customer objCust = null;
        public AppointmentDetails obj = null;
        public BookAppointment objBookAppointment = null;
        ObservableCollection<AppointmentDetails> ListofAppointment = new ObservableCollection<AppointmentDetails>();
        public AddAppointmentsPage(Customer Cust, BookAppointment objAppointment)
        {
            InitializeComponent();
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            BindingContext = objCust;
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
            //var listofAppointment = GetAppointmentDetails();
           // CustomerAppoimentList.ItemsSource = listofAppointment;
        }
        private void AppointmentsdetailsClick(object sender, SelectedItemChangedEventArgs e)
        {
            var data=e.SelectedItem as AppointmentDetails;
            obj = new AppointmentDetails();
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
            obj.BookingDate = data.BookingDate;
            obj.Colour = (data.Colour) == null ? "" : data.Colour;
            obj.DurationHrsMin = data.DurationHrsMin;
            obj.AppointmentDetail = data.AppointmentDetail;
            obj.CommentNotes = data.CommentNotes;
            Navigation.PushAsync(new AppointmentDetailsPage(objCust, obj));
        }


        private void AddAppointmentsClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory(CategoryId, objCust, PageName));
        }

        public ObservableCollection<AppointmentDetails> GetAppointmentDetails()
        {
            try
            {
                //if (objBookAppointment != null)
                //{
                var startDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(-1)).ToString("dd-MM-yyyy");

                var endDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(1)).ToString("dd-MM-yyyy");


                string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetBookingsForCustomerByIdBetweenDates?customerId=" + objCust.Id + "&startDate=" + startDate + "&endDate=" + endDate;
                //string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetAllBookingForCustomer?customerId=" + objBookAppointment.CustomerIdsCommaSeperated + "&dateOfBooking=" + objBookAppointment.Start;

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
                    string DateOFbooking = appointment.BookingDate.ToString("dd-MMM-yyyy");
                    string detail = appointment.Employee.FirstName + "," + appointment.Service.Name + "," + Duration + "," + appointment.Service.Cost;
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
                    obj.BookingDate = DateOFbooking;
                    obj.Colour = (appointment.Service) == null ? "" : appointment.Service.Colour;
                    obj.DurationHrsMin = Duration;
                    obj.AppointmentDetail = detail;
                    obj.CommentNotes = appointment.Notes;

                    ListofAppointment.Add(obj);

                }

                //}
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