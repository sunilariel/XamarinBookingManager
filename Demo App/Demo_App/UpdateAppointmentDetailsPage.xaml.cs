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
        #region globles
        int EmpID;
        string empName = "";
        int ServiceID;
        string ServiceName = "";
        int CustID;
        double Cost;
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public Service service = null;
        public Notes objNotes = null;
        public UpdateAppointments obj = null;
        public AddAppointments addAppointments = null;
        public UpdateBookAppointment UpdatebookAppointment = null;
        int CategoryId;
        int StatusId;
        Dictionary<string, int> Data = null;

        #endregion

        public UpdateAppointmentDetailsPage (Customer Cust, AddAppointments appointment,string Day, DateTime DateOfBooking, Notes objNotes)
		{

            InitializeComponent();
            day = Day;
            dateOfBooking = DateOfBooking;
            EmpID = appointment.EmployeeId;
            empName = appointment.EmployeeName;
            Cost = appointment.Cost;
            CustID = Cust.Id;
            ServiceID = appointment.ServiceId;
            ServiceName = appointment.ServiceName;
            service = new Service();
            service.Id = Convert.ToInt32(appointment.ServiceId);
            service.Name = appointment.ServiceName;
            service.Cost = appointment.Cost;
            addAppointments = new AddAppointments();
            addAppointments.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            addAppointments.EmployeeId = EmpID;
            addAppointments.EmployeeName = empName;
            addAppointments.ServiceId = ServiceID;
            addAppointments.ServiceName = ServiceName;
            addAppointments.Cost = Cost;
            addAppointments.StartTime = appointment.StartTime;
            addAppointments.EndTime = appointment.EndTime;
            addAppointments.TimePeriod = appointment.StartTime; ;
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
            obj.CommentNotes = objNotes == null ? "": objNotes.Description; 
            obj.TimePeriod = appointment.StartTime;
            BindingContext = obj;
            //string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };

            Data = new Dictionary<string, int>
            {
               { "No Label", 1 }, { "Pending", 2 }, { "Confirmed", 3 }, { "Done", 4 },
               { "No-Show", 5}, { "Paid", 6 },{ "Running Late", 7 }, { "Custom Label", 8 },
            };
           
            foreach (var item in Data.Keys)
            {
                AppointmentsPicker.Items.Add(item);
            }

            
           // var id = Data.TryGetValue("No Label", out StatusId);
            
        }

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory(CategoryId, objCust, "EditAppointment",objNotes));
        }


        private void UpdateAppointmentbyBookingDateClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceID, ServiceName, EmpID, empName, objCust, Cost, "EditAppointment",objNotes));
        }

        private void EditCommentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerCommentsForAppointmentPage(addAppointments, objCust, day, dateOfBooking, "EditAppointment"));
        }

        private void UpdateAppointmentbyStaffClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, objCust, "EditAppointment",objNotes));
        }

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
            if(AppointmentsPicker.SelectedItem!=null)
            { 
            string selectedValue = (AppointmentsPicker.SelectedItem).ToString();
            Data.TryGetValue(selectedValue, out StatusId);
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
            UpdatebookAppointment.Status = StatusId;

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