using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Globalization;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCustomerPage : ContentPage
	{
		public NewCustomerPage ()
		{
			InitializeComponent ();
		}
        private void AddressClick(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new AddressPage());
        }


        public void AddCustomer()
        {                    
            var CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            Customer obj = new Customer();
            obj.Id = 0;
            obj.CompanyId = CompanyId;
            obj.UserName = CustomerEmail.Text;
            obj.Password = "123456";
            obj.FirstName = CustomerName.Text;
            obj.LastName = "";
            obj.Address = CustomerAddress.Text;
            obj.Email = CustomerEmail.Text;
            obj.TelephoneNo = CustomerPhoneNumber.Text;
            obj.CreationDate = Convert.ToString(DateTime.Now);
            var data = JsonConvert.SerializeObject(obj);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/clientreservation/CreateCustomer";
            PostData("POST", data, apiUrl);
        }

        public string DeleteCustomer(string CompanyId, string CustomerId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/DeleteCustomer?companyId=" + "CompanyId" + "&customerId=" + "CustomerId";
            var result = PostData("DELETE", "", apiUrl);
            return result;
        }


        public string GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            return result;
        }

        public string UpdateCustomer(Customer customer)
        {
            var CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);

            Customer obj = new Customer();
            obj.Id = 0;
            obj.CompanyId = CompanyId;
            obj.UserName = "customer2@gmail.com";
            obj.Password = "123456";
            obj.FirstName = "customer2";
            obj.LastName = "";
            obj.Address = "";
            obj.Email = "customer2@gmail.com";
            obj.TelephoneNo = "123456";
            obj.CreationDate = Convert.ToString(DateTime.Now);


            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/Update";
            var jsonString = JsonConvert.SerializeObject(obj);
            var result = PostData("POST", jsonString, apiURL);

            return result;
        }

        public string AddAppointment(BookAppointment appointment)
        {
            try
            {
                DateTime obj = DateTime.Parse(appointment.Start);
                appointment.Start = obj.ToString("yyyy-MM-dd T HH:mm:ss");
                appointment.End = obj.AddMinutes(appointment.EndMinute).ToString("yyyy-MM-dd T HH:mm:ss");

                var StartTime = DateTime.Parse(appointment.StartHour, CultureInfo.InvariantCulture);
                var Time = StartTime.ToString("HH:mm").Split(':');

                appointment.StartHour = Time[0];
                appointment.StartMinute = Time[1];

                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/booking/BookAppointment";
                var jsonString = JsonConvert.SerializeObject(appointment);

                var result = PostData("POST", jsonString, apiURL);               
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public void UpdateAppointment(UpdateBookAppointment appointment)
        {
            
                DateTime obj = DateTime.Parse(appointment.Start);
                appointment.Start = obj.ToString("yyyy-MM-dd T HH:mm:ss");
                appointment.End = obj.AddMinutes(appointment.EndMinute).ToString("yyyy-MM-dd T HH:mm:ss");

                var StartTime = DateTime.Parse(appointment.StartHour, CultureInfo.InvariantCulture);
                var Time = StartTime.ToString("HH:mm").Split(':');

                appointment.StartHour = Time[0];
                appointment.StartMinute = Time[1];

                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/booking/UpdateBooking";
                var jsonString = JsonConvert.SerializeObject(appointment);

                PostData("POST", jsonString, apiURL);

              
            
            
        }

        public string GetSelectedService(string ServiceId)
        {
          
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceById?id=" + ServiceId;
            var result = PostData("GET", "", apiURL);            
            return result;
        }

        public string GetCompanyDetails(string companyId)
        {
            // int Id = Convert.ToInt32(CompanyId);
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyDetails?companyId=" + companyId;
            var result = PostData("GET", "", apiURL);           
            return result;
        }

        public string GetAppointmentWorkinghours(string EmployeeId)
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/staff/GetWorkingHours?employeeId=" + EmployeeId;
            var result = PostData("GET", "", apiURL);           
            return result;
        }

        public string GetFreeBookingSlotsForEmployee(WorkingHoursofEmployee dataObj)
        {          
                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/booking/GetFreeBookingSlotsForEmployee?companyId=" + dataObj.CompanyId + "&serviceId=" + dataObj.ServiceId + "&employeeId=" + dataObj.EmployeeId + "&dateOfBooking=" + dataObj.DateOfBooking + "&day=" + dataObj.Day;

                var result = PostData("GET", "", apiURL);              
                return result;          
        }

        public string GetAppointmentDetails(string CustomerId)
        {
            
                var startDate = DateTime.Now.Date.AddYears(-1).ToShortDateString().Replace("/", "-");

                var endDate = DateTime.Now.Date.AddYears(1).ToShortDateString().Replace("/", "-");

                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/booking/GetBookingsForCustomerByIdBetweenDates?customerId=" + CustomerId + "&startDate=" + startDate + "&endDate=" + endDate;

                var result = PostData("GET", "", apiURL);            

                List<AllAppointments> appointments = JsonConvert.DeserializeObject<List<AllAppointments>>(result);
                List<AppointmentDetails> ListofAppointment = new List<AppointmentDetails>();
                foreach (var appointment in appointments)
                {
                    AppointmentDetails obj = new AppointmentDetails();
                    obj.BookingId = appointment.Id;
                    obj.EmployeeId = appointment.EmployeeId.ToString();
                    obj.ServiceId = appointment.ServiceId.ToString();
                    obj.EmployeeName = (appointment.Employee) == null ? "" : appointment.Employee.FirstName;
                    obj.ServiceName = appointment.Service.Name;
                    obj.DurationInHours = appointment.Service.DurationInHours;
                    obj.DurationInMinutes = appointment.Service.DurationInMinutes;
                    obj.Cost = appointment.Service.Cost;
                    obj.Currency = appointment.Service.Currency;
                    obj.status = appointment.Status;
                    obj.StartTime = appointment.Start;
                    obj.EndTime = appointment.End;
                    obj.Colour = appointment.Service.Colour;

                    ListofAppointment.Add(obj);

                }

                var jsondata = JsonConvert.SerializeObject(ListofAppointment);

                return jsondata;
          
        }

        public string SetStatusOfAppointment(string status, string BookingId)
        {         
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/SetStatus?status=" + status + "&bookingId=" + BookingId;
                var result = PostData("POST", "", apiUrl);               
                return result;     
        }

        public string UpdateBooking(BookAppointment appointment)
        {            
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/UpdateBooking";
                var JsonString = JsonConvert.SerializeObject(appointment);

                var result = PostData("POST", JsonString, apiUrl);                          
                return result;                  
        }

        public string DeleteAppointment(string BookingId)
        {
            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/DeleteBooking?bookingId=" + BookingId;

            var result = PostData("DELETE", "", apiUrl);
            return result;
        }


        public string GetCustomerStats(string CompanyId, string CustomerId, string Year, string Month)
        {
                   
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/GetCustomerStats?companyId=" + CompanyId + "&customerId=" + CustomerId + "&year=" + Year + "&month=" + Month;
                var result = PostData("GET", "", apiUrl);               
                return result;
           
        }

        public string AddCustomerNote(Notes notesdetail)
        {
         
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/AddNote";
                var JsonString = JsonConvert.SerializeObject(notesdetail);
                var result = PostData("POST", JsonString, apiUrl);             
                return result;
            
        }

        public string DeleteCustomerNote(string CompanyId, string CustomerNoteId)
        {           
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/DeleteCustomerNote?companyId=" + CompanyId + "&customerNoteId=" + CustomerNoteId;
                var result = PostData("DELETE", "", apiUrl);             
                return result;           
        }

        public string GetCustomerNotes(string CompanyId, string CustomerId)
        {           
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + CustomerId;
                var result = PostData("GET","", apiUrl);              
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