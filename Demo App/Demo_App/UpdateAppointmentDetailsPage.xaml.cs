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
	public partial class UpdateAppointmentDetailsPage : ContentPage
	{
        int EmpID;
        int ServiceID;
        int CustID;
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public UpdateAppointments obj = null;
        public UpdateBookAppointment UpdatebookAppointment = null;
        int CategoryId;
        public UpdateAppointmentDetailsPage (Customer Cust, AddAppointments appointment,string Day, DateTime DateOfBooking, Notes objNotes)
		{

            InitializeComponent();
            day = Day;
            dateOfBooking = DateOfBooking;
            EmpID = appointment.EmployeeId;
            CustID = Cust.Id;
            ServiceID = appointment.ServiceId;
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
            string strttime = appointment.StartTime.Split('-')[0];
            string endtime = appointment.StartTime.Split('-')[1];
            obj = new UpdateAppointments();           
            obj.EmployeeId = appointment.EmployeeId;
            obj.ServiceId = appointment.ServiceId;
            obj.EmployeeName = appointment.EmployeeName;
            obj.ServiceName = appointment.ServiceName;
            obj.DurationInHours = appointment.DurationInHours;
            obj.DurationInMinutes = appointment.DurationInMinutes;
            obj.Cost = appointment.Cost;
            obj.Currency = appointment.Currency;           
            obj.StartTime = strttime;
            obj.EndTime = endtime;
            obj.BookingDate = day+","+ dateOfBooking.ToString("dd-MMM-yyyy");           
            //obj.CommentNotes = appointment.CommentNotes;
            obj.TimePeriod = appointment.StartTime;
            BindingContext = obj;
            string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };
            for (var i = 0; i < Data.Length; i++)
            {
                AppointmentsPicker.Items.Add(Data[i]);
            }
        }

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory(CategoryId, objCust, "EditServiceForAppointment"));
        }


        //private void EditStaffForAppointmentClick(object sender,EventArgs e)
        //{
        //    Navigation.PushAsync(new SelectStaffForAppointmentPage(objservice, objCust, "EditStaffForAppointment"));
        //}      

        public void UpdateAppointments()
        {
            string[] AppointmentDate = { };
            string[] TimeAppointment = { };
            string[] hours = { };
            string[] Endmins = { };
            string[] Endmin = { };           
            string Time = UpdateAppointmentTime.Text;           
            if (Time != null)
            {
                TimeAppointment = Time.Split('-');
                hours = TimeAppointment[0].Split(':');
                Endmins = TimeAppointment[1].Split(':');
                Endmin = Endmins[1].Split(' ');
            }
            var GetAllCustomerData = GetAllCustomer();
            List<int> custIDs = GetAllCustomerData.Select(z => z.Id).ToList();
            UpdatebookAppointment = new UpdateBookAppointment();
            UpdatebookAppointment.Id = Application.Current.Properties["BookingID"].ToString();
            UpdatebookAppointment.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            UpdatebookAppointment.EmployeeId = EmpID;
            UpdatebookAppointment.ServiceId = ServiceID;
            UpdatebookAppointment.CustomerIdsCommaSeperated = CustID.ToString();
            UpdatebookAppointment.StartHour = Convert.ToInt32(hours[0]);
            UpdatebookAppointment.StartMinute = 0;
            UpdatebookAppointment.EndHour = 0;
            UpdatebookAppointment.EndMinute = Convert.ToInt32(Endmin[0]);
            UpdatebookAppointment.IsAdded = true;
            UpdatebookAppointment.Message =UpdateComment.Text;
            UpdatebookAppointment.Notes = UpdateComment.Text;
            UpdatebookAppointment.CustomerIds = custIDs;
            UpdatebookAppointment.Start = dateOfBooking;
            UpdatebookAppointment.End = dateOfBooking;
            UpdatebookAppointment.Status = 0;

            var SerializedData = JsonConvert.SerializeObject(UpdatebookAppointment);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/UpdateBooking";
            var result = PostData("POST", SerializedData, apiUrl);

            dynamic data = JObject.Parse(result);
            var msg = Convert.ToString(data.Message);
            DisplayAlert("Success", msg, "ok");

            //Navigation.PushAsync(new AddAppointmentsPage(objCust, UpdatebookAppointment));
        }

        public List<Customer> GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            List<Customer> ListOfCustomer = JsonConvert.DeserializeObject<List<Customer>>(result); return ListOfCustomer;
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