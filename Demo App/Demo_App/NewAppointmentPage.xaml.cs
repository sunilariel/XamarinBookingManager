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
        #region GloblesVariables
        int EmpID;
        string empName = "";
        int ServiceID;
        string ServiceName = "";
        int CustID;
        double Cost;
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public Notes objNotes = null;
        ObservableCollection<Notes> ListNotes = new ObservableCollection<Notes>();
        public Service service = null;
        public BookAppointment objbookAppointment = null;
        public AddAppointments obj = null;
        Dictionary<string, int> Data = null;
        int StatusId;
        int CategoryId;
        
        #endregion

        public NewAppointmentPage(AddAppointments objAddAppointments, string Day, DateTime DateOfBooking)
        {
            try
            {
                var notesList = GetAllCustomerNotes();
                InitializeComponent();
                GetSelectedCustomerById();
                day = Day;
                dateOfBooking = DateOfBooking;
                EmpID = objAddAppointments.EmployeeId;
                empName = objAddAppointments.EmployeeName;
                ServiceID = objAddAppointments.ServiceId;
                ServiceName = objAddAppointments.ServiceName;
                if (objCust != null)
                {
                    CustID = objCust.Id;
                }
                Cost = objAddAppointments.Cost;
                service = new Service();
                service.Id = Convert.ToInt32(objAddAppointments.ServiceId);
                service.Name = objAddAppointments.ServiceName;
                service.Cost = objAddAppointments.Cost;
                obj = new AddAppointments();
                obj.CompanyId = objAddAppointments.CompanyId;
                obj.ServiceId = objAddAppointments.ServiceId;
                obj.EmployeeId = objAddAppointments.EmployeeId;
                obj.EmployeeName = objAddAppointments.EmployeeName;
                obj.ServiceName = objAddAppointments.ServiceName;
                obj.Cost = objAddAppointments.Cost;
                obj.StartTime = objAddAppointments.StartTime;
                obj.EndTime = objAddAppointments.EndTime;

                //objCust = new Customer();
                //objCust.Id = Cust.Id;
                //objCust.FirstName = Cust.FirstName;
                //objCust.LastName = Cust.LastName;
                //objCust.UserName = Cust.UserName;
                //objCust.Email = Cust.Email;
                //objCust.TelephoneNo = Cust.TelephoneNo;
                //objCust.Address = Cust.Address;
                AppointmentDatelbl.Text = Day + ", " + DateOfBooking.ToString("dd-MMM-yyyy");
                if (objCust != null)
                {
                    CustName.Text = objCust.FirstName;
                    CustEmail.Text = objCust.Email;
                    CustPhoneNo.Text = objCust.TelephoneNo;
                }
                foreach (var item in notesList)
                {
                    AddComment.Text = item.Description;
                }
                BindingContext = objAddAppointments;

                //string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };          
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

            }

        }

        private void UpdateAppointmentbyBookingDateClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceID, ServiceName, EmpID, empName, Cost, "NewAppointment"));
        }

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectServiceCategory("NewAppointment"));
        }
        private void EditAppointmentbyStaffClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, "NewAppointment"));
        }
        private void AddCommentClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new CustomerCommentsForAppointmentPage(obj,objCust, day,dateOfBooking,"addAppointment"));
            Navigation.PushAsync(new AddNotesPage());
        }

        public void CreateAppointment()
        {
            try
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
                //Toast.MakeText(this.Content, msg, ToastLength.Short).Show();

                Navigation.PushAsync(new AddAppointmentsPage(objbookAppointment));
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
                var result = PostData("GET", "", apiURL);
                List<Customer> ListOfCustomer = JsonConvert.DeserializeObject<List<Customer>>(result); return ListOfCustomer;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public string GetAppointmentWorkinghours()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + EmpID;
                string result = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public ObservableCollection<Notes> GetAllCustomerNotes()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                ListNotes = JsonConvert.DeserializeObject<ObservableCollection<Notes>>(result);
                return ListNotes;
            }
            catch(Exception e)
            {
                return null;
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