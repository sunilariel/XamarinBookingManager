using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
	public partial class CalendarCreateAppointmentPage : ContentPage
	{
        #region GloblesFields  
        int EmpID;
        string empName = "";
        int ServiceID;
        string ServiceName = "";
        int CustID;
        double Cost;
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public BookAppointment objbookAppointment = null;
        public AssignedServicetoStaff objdata = null;
        Dictionary<string, int> Data = null;
        int StatusId;
        int CategoryId;
        #endregion

        public CalendarCreateAppointmentPage (AddAppointments objAddAppointment)
		{
			InitializeComponent ();
            GetSelectedCustomerById();
            objdata = new AssignedServicetoStaff();
            objdata.Id= objAddAppointment.ServiceId;
            objdata.Name= objAddAppointment.ServiceName;
            objdata.Cost = objAddAppointment.Cost;
            objdata.DurationInHours = objAddAppointment.DurationInHours;
            objdata.DurationInMinutes = objAddAppointment.DurationInMinutes;
            dateOfBooking = Convert.ToDateTime(objAddAppointment.DateOfBooking.Split(',')[1]);
            EmpID = objAddAppointment.EmployeeId;
            empName = objAddAppointment.EmployeeName;
            ServiceID = objAddAppointment.ServiceId;
            ServiceName = objAddAppointment.ServiceName;
            if (objCust != null)
            {
                CustID = objCust.Id;
                CustName.Text = objCust.FirstName;
                CustEmail.Text = objCust.Email;
                CustPhoneNo.Text = objCust.TelephoneNo;
            }
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

            }

        }

        public void CreateAppointment()
        {           
            string[] TimeAppointment = { };
            string[] hours = { };
            string[] Endmins = { };
            string[] Endmin = { };          
            string Time = AppointmentTime.Text;           
            if (Time != null)
            {
                TimeAppointment = Time.Split('-');
                hours = TimeAppointment[0].Split(':');
                Endmins = TimeAppointment[1].Split(':');
                Endmin = Endmins[1].Split(' ');
            }
            if (newAppointmentsPicker.SelectedItem != null)
            {
                string selectedValue = (newAppointmentsPicker.SelectedItem).ToString();
                Data.TryGetValue(selectedValue, out StatusId);
            }
            var GetAllCustomerData = GetAllCustomer();
            List<int> custIDs = GetAllCustomerData.Select(z => z.Id).ToList();
            objbookAppointment = new BookAppointment();
            objbookAppointment.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            objbookAppointment.EmployeeId = EmpID;
            objbookAppointment.ServiceId = ServiceID;
            objbookAppointment.CustomerIdsCommaSeperated = CustID.ToString();
            objbookAppointment.StartHour = Convert.ToInt32(hours[0]);
            objbookAppointment.StartMinute = 0;
            objbookAppointment.EndHour = 0;
            objbookAppointment.EndMinute = Convert.ToInt32(Endmin[0]);
            objbookAppointment.IsAdded = true;
            objbookAppointment.Message = AddComment.Text;
            objbookAppointment.Notes = AddComment.Text;
            objbookAppointment.CustomerIds = custIDs;
            objbookAppointment.Start = dateOfBooking;
            objbookAppointment.End = dateOfBooking;
            objbookAppointment.Status = StatusId;

            var SerializedData = JsonConvert.SerializeObject(objbookAppointment);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/BookAppointment";
            var result = PostData("POST", SerializedData, apiUrl);

            dynamic data = JObject.Parse(result);
            var msg = Convert.ToString(data.Message);
            DisplayAlert("Success", msg, "ok");
            //Context context = getApplicationContext();  
           // Toast.MakeText(getApplicationContext(), msg, ToastLength.Short).Show();
            
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);           
            Navigation.PopAsync();
        }

        public List<Customer> GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            List<Customer> ListOfCustomer = JsonConvert.DeserializeObject<List<Customer>>(result); return ListOfCustomer;
        }

        private void EditServiceForAppointmentClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new GetAllocateServiceForEmployeePage());
        }

        private void EditAppointmentByBookingDate(object sender,EventArgs e)
        {
            Navigation.PushAsync(new CalendarTimeSlotsPage(objdata, "CalandarAppointment"));
        }

        private void AddCommentClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new AddNotesPage());
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

                if (SerializedData !=
                    "")
                {
                    var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                    streamWriter.Write(SerializedData);
                    streamWriter.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var StreamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return result = StreamReader.ReadToEnd();
                    //var SuccessMsz=StreamReader.
                }

            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}