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

        public static bool isCalenderPageOpen = false;
        int EmpID;
        string EmpName;
        int ServiceID;
        int EndHour;
        int EndMinute;
        string StartHour;
        string StartMinutes;
        string ServiceName = "";
        int CustID;
        //double Cost;
        //string day = "";
        DateTime dateOfBooking;
        string book;
        //string Booking;
        //string TimePeriods;
        public Customer objCust = null;
        int CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
        public Customer aaa = null;
        public BookAppointment objbookAppointment = null;
        public AddAppointments objdata = null;
        Dictionary<string, int> Data = null;
        int StatusId;
        int CategoryId = Convert.ToInt32(Application.Current.Properties["SelectedCustomerId"]);
        string CategoryName;
        #endregion

        public CalendarCreateAppointmentPage(AddAppointments objAddAppointment)
        {
            InitializeComponent();
            try
            {

                GetSelectedCustomerById();

                StartHour = objAddAppointment.StartTime;
                StartMinutes = objAddAppointment.EndTime;
                //TimePeriods = objAddAppointment.TimePeriod;
                EmpID = objAddAppointment.EmployeeId;
                //EmpName = objAddAppointment.EmployeeName;
                ServiceID = objAddAppointment.ServiceId;
                EndHour = objAddAppointment.DurationInHours;
                EndMinute = objAddAppointment.DurationInMinutes;
                //ServiceName = objAddAppointment.ServiceName;
                 


                objdata = new AddAppointments();
                objdata.CompanyId = objAddAppointment.CompanyId;
                objdata.Cost = objAddAppointment.Cost;
                objdata.Currency = objAddAppointment.Currency;
                objdata.DateOfBooking = objAddAppointment.DateOfBooking;
                objdata.DurationInHours = objAddAppointment.DurationInHours;
                objdata.DurationInMinutes = objAddAppointment.DurationInMinutes;
                objdata.EmployeeId = objAddAppointment.EmployeeId;
                objdata.EmployeeName = objAddAppointment.EmployeeName;
                objdata.EndTime = objAddAppointment.EndTime;
                objdata.ServiceId = objAddAppointment.ServiceId;                
                objdata.ServiceName = objAddAppointment.ServiceName;
                objdata.StartTime = objAddAppointment.StartTime;
                objdata.TimePeriod = objAddAppointment.TimePeriod;
                
                dateOfBooking = Convert.ToDateTime(objAddAppointment.DateOfBooking.Split(',')[1]);
                book = objAddAppointment.DateOfBooking;
                
                AppointmentTime.Text = objAddAppointment.TimePeriod;
                
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
                { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
               { "No-Show",5}, { "Paid",6},{ "Running Late",7},
               //{ "Custom Label",8}
            };

                foreach (var item in Data.Keys)
                {
                    newAppointmentsPicker.Items.Add(item);
                }
                newAppointmentsPicker.SelectedIndex = 0;
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
                var Url = Application.Current.Properties["DomainUrl"] + "/api/customer/GetCustomerById?id=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objCust = JsonConvert.DeserializeObject<Customer>(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    
        public void CreateAppointment()
        {
            try
            {
                
                DependencyService.Get<IProgressInterface>().Show();
                
                //InitializeComponent();
                BookAppointment objbookAppointment = new BookAppointment();
                objbookAppointment.CompanyId = CompanyId;                
                objbookAppointment.ServiceId = ServiceID;                
                objbookAppointment.EmployeeId = EmpID;
                objbookAppointment.CustomerIdsCommaSeperated = CustID.ToString();           
                objbookAppointment.StartHour = StartHour;                
                objbookAppointment.StartMinute = StartMinutes;                          
                objbookAppointment.EndHour = EndHour;                
                objbookAppointment.EndMinute = EndMinute;    
                
                objbookAppointment.IsAdded = true;                
                objbookAppointment.Message = "";
                var GetAllCustomerData = GetAllCustomer();
                List<int> custIDs = GetAllCustomerData.Select(x => x.Id).ToList();
                custIDs = custIDs.Where(x => x == CustID).ToList();
                objbookAppointment.CustomerIds = custIDs;
                var sDate = dateOfBooking.ToString("yyyy-MM-dd T HH:mm:ss");
                objbookAppointment.Start = sDate;
                var eeDate = dateOfBooking.ToString("yyyy-MM-dd T HH:mm:ss");
                objbookAppointment.End = eeDate;
                if (newAppointmentsPicker.SelectedItem != null)
                {
                    string selectedValue = (newAppointmentsPicker.SelectedItem).ToString();
                    Data.TryGetValue(selectedValue, out StatusId);
                }
                objbookAppointment.Status = StatusId;
                objbookAppointment.Notes = "";
                var Url = Application.Current.Properties["DomainUrl"] + "/api/booking/BookAppointment";
                var SerializedData = JsonConvert.SerializeObject(objbookAppointment);
                var result = PostData("POST", SerializedData, Url);


                

                dynamic data = JObject.Parse(result);
                var msg = Convert.ToString(data.Message);
                DisplayAlert("Success", msg, "ok");               
                Navigation.PushAsync(new SetAppointmentPage("","",""));
            }
            catch (Exception ex)
            {
                var msgs = ex.Message;
                DisplayAlert("success", msgs, "ok");
                ex.ToString();
            }
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                string apiURLs = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
                var result = PostData("GET", "", apiURLs);
                List<Customer> ListOfCustomer = JsonConvert.DeserializeObject<List<Customer>>(result);

                return ListOfCustomer;
            }
            catch (Exception ex)
            {
                var ms = "alert getAllC";
                DisplayAlert("success", ms, "ok");
                ex.ToString();
                return null;
            }


        }


        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServicesForAppontment("ECalandarAppointment", CategoryId, CategoryName, book,StatusId));
        }

        private void EditAppointmentByBookingDate(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalendarTimeSlotsPage(objdata, "ECalandarAppointment"));
        }

        private void AddCommentClick(object sender, EventArgs e)
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