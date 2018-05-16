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
        string StartHour;
        string StartMinutes;
        int EndHour;
        int EndMinute;
        int DurationInHours;
        int DurationInMinutes;
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
        int CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);

        #endregion

        public NewAppointmentPage(AddAppointments objAddAppointments, string Day, DateTime DateOfBooking)
        {
            InitializeComponent();
            try
            {
                var notesList = GetAllCustomerNotes();

                GetSelectedCustomerById();
                day = Day;
                StartHour = objAddAppointments.StartTime;
                StartMinutes = objAddAppointments.EndTime;
                EndHour = objAddAppointments.DurationInHours;
                EndMinute = objAddAppointments.DurationInMinutes;

                //dateOfBooking = DateOfBooking;
                EmpID = objAddAppointments.EmployeeId;
                empName = objAddAppointments.EmployeeName;
                ServiceID = objAddAppointments.ServiceId;
                ServiceName = objAddAppointments.ServiceName;
                if (objCust != null)
                {
                    CustID = objCust.Id;
                }
                Cost = objAddAppointments.Cost;
                DurationInHours = objAddAppointments.DurationInHours;
                DurationInMinutes = objAddAppointments.DurationInMinutes;
                service = new Service();
                service.Id = Convert.ToInt32(objAddAppointments.ServiceId);
                service.Name = objAddAppointments.ServiceName;
                service.Cost = objAddAppointments.Cost;
                service.DurationInHours = objAddAppointments.DurationInHours;
                service.DurationInMinutes = objAddAppointments.DurationInMinutes;
                obj = new AddAppointments();
                obj.CompanyId = objAddAppointments.CompanyId;
                obj.ServiceId = objAddAppointments.ServiceId;
                obj.EmployeeId = objAddAppointments.EmployeeId;
                obj.EmployeeName = objAddAppointments.EmployeeName;
                obj.ServiceName = objAddAppointments.ServiceName;
                obj.Cost = objAddAppointments.Cost;
                obj.DurationInHours = objAddAppointments.DurationInHours;
                obj.DurationInMinutes = objAddAppointments.DurationInMinutes;
                obj.StartTime = objAddAppointments.StartTime;
                obj.EndTime = objAddAppointments.EndTime;
                obj.TimePeriod = objAddAppointments.TimePeriod;
                obj.Status = objAddAppointments.Status;
                AppointmentDatelbl.Text = objAddAppointments.StartTime;

                //objCust = new Customer();
                //objCust.Id = Cust.Id;
                //objCust.FirstName = Cust.FirstName;
                //objCust.LastName = Cust.LastName;
                //objCust.UserName = Cust.UserName;
                //objCust.Email = Cust.Email;
                //objCust.TelephoneNo = Cust.TelephoneNo;
                //objCust.Address = Cust.Address;
                dateOfBooking = Convert.ToDateTime(objAddAppointments.DateOfBooking.Split(',')[1]);
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
               { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
               { "No-Show",5}, { "Paid",6},{ "Running Late",7}, { "Custom Label",8},
            };

                if(objAddAppointments.Status != 0)
                {
                    foreach (var item in Data.Keys)
                    {
                        newAppointmentsPicker.Items.Add(item);
                    }
                    obj.Status = objAddAppointments.Status - 1;
                    newAppointmentsPicker.SelectedIndex = obj.Status;
                }
                else
                {
                    foreach (var item in Data.Keys)
                    {
                        newAppointmentsPicker.Items.Add(item);
                    }

                    newAppointmentsPicker.SelectedIndex = 0;
                }
                
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public string AddStatusOfNewAppointment()
        {
            string selectedValue = (newAppointmentsPicker.SelectedItem).ToString();
            Data.TryGetValue(selectedValue, out StatusId);
            var id = StatusId;
            return id.ToString();
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

        private void UpdateAppointmentbyBookingDateClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceID, ServiceName, EmpID, empName, Cost, DurationInHours, DurationInMinutes, "NewAppointment", DateofBooking, StatusId));
        }

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new SelectServiceCategory("NewAppointment", DateofBooking, StatusId));
        }
        private void EditAppointmentbyStaffClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, "NewAppointment", DateofBooking, StatusId));
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

                for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                }

                // Navigation.PopAsync(true);

                Navigation.PushAsync(new AddAppointmentsPage(objbookAppointment));

            }
            catch (Exception e)
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
            catch (Exception e)
            {
                e.ToString();
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
            catch (Exception e)
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
            catch (Exception e)
            {
                e.ToString();
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