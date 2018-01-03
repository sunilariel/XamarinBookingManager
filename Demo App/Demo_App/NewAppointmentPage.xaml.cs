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
    public partial class NewAppointmentPage : ContentPage
    {
        int EmpID;
        int ServiceID;
        int CustID;
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public BookAppointment objbookAppointment = null;
        public AddAppointments obj = null;
        public NewAppointmentPage(AddAppointments objAddAppointments, Customer Cust, string Day, DateTime DateOfBooking,Notes objNotes)
        {
            GetAllCustomerNotes();
            InitializeComponent();
            day = Day;
            dateOfBooking = DateOfBooking;
            EmpID = objAddAppointments.EmployeeId;
            ServiceID = objAddAppointments.ServiceId;
            CustID = Cust.Id;
            obj = new AddAppointments();
            obj.CompanyId = objAddAppointments.CompanyId;
            obj.ServiceId = objAddAppointments.ServiceId;
            obj.EmployeeId = objAddAppointments.EmployeeId;
            obj.EmployeeName = objAddAppointments.EmployeeName;
            obj.ServiceName = objAddAppointments.ServiceName;
            obj.Cost = objAddAppointments.Cost;
            obj.StartTime = objAddAppointments.StartTime;
            obj.EndTime = objAddAppointments.EndTime;
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            AppointmentDatelbl.Text = Day + ", " + DateOfBooking.ToString("dd-MMM-yyyy");
            CustName.Text = Cust.FirstName;
            CustEmail.Text = Cust.Email;
            CustPhoneNo.Text = Cust.TelephoneNo;
            if (objNotes != null)
            {
                AddComment.Text = objNotes.Description;
            }
            BindingContext = objAddAppointments;
            string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };
            for (var i = 0; i < Data.Length; i++)
            {
                newAppointmentsPicker.Items.Add(Data[i]);
            }
        }

        private void AddCommentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerCommentsForAppointmentPage(obj,objCust, day,dateOfBooking));
        }

        public void CreateAppointment()
        {
            string[] AppointmentDate = { };
            string[] TimeAppointment = { };
            string[] hours = { };
            string[] Endmins = { };
            string[] Endmin = { };
            //string date = AppointmentDatelbl.Text;
            string Time = AppointmentTime.Text;
            //if (date != null)
            //{
            //   AppointmentDate = date.Split(',');                
            //}
            if(Time != null)
            {
                TimeAppointment = Time.Split('-');
                hours = TimeAppointment[0].Split(':');
                Endmins = TimeAppointment[1].Split(':');
                Endmin= Endmins[1].Split(' ');
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
            objbookAppointment.Status = 0;

            var SerializedData = JsonConvert.SerializeObject(objbookAppointment);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/BookAppointment";
            var result = PostData("POST", SerializedData, apiUrl);

            dynamic data = JObject.Parse(result);
            var msg = Convert.ToString(data.Message);
            DisplayAlert("Success", msg,"ok");

            Navigation.PushAsync(new AddAppointmentsPage(objCust, objbookAppointment));
        }

        public List<Customer> GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            List<Customer> ListOfCustomer = JsonConvert.DeserializeObject<List<Customer>>(result); return ListOfCustomer;
        }

        public void GetAllCustomerNotes()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + CustID;
            var Method = "GET";

            var result = PostData(Method, "", Url);
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