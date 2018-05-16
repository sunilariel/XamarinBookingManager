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
        #region GloblesFields
        int EmpID;
        string empName = "";
        int ServiceID;
        string ServiceName = "";
        int CustID;
        string BookingID = Convert.ToString(Application.Current.Properties["BookingID"]);
        double Cost;
        int DurationInHours;
        int DurationInMinutes;
        int EndHour;
        int EndMinute;
        string StartHour;
        string StartMinutes;
        string day = "";
        int CompanyId;
        string book;
        DateTime dateOfBooking;
        public Customer objCust = null;
        public Service service = null;
        public Notes objNotes = null;
        public UpdateAppointments obj = null;
        public AddAppointments addAppointments = null;
        public UpdateBookAppointment UpdatebookAppointment = null;
        ObservableCollection<Notes> ListNotes = new ObservableCollection<Notes>();
        int CategoryId;
        int StatusId;
        Dictionary<string, int> Data = null;

        #endregion

        public UpdateAppointmentDetailsPage(AddAppointments appointment, string Day, DateTime DateOfBooking)
        {
            try
            {
                InitializeComponent();
                GetSelectedCustomerById();

                StartHour = appointment.StartTime;
                StartMinutes = appointment.EndTime;
                //TimePeriods = objAddAppointment.TimePeriod;
                EmpID = appointment.EmployeeId;
                //EmpName = objAddAppointment.EmployeeName;
                ServiceID = appointment.ServiceId;
                EndHour = appointment.DurationInHours;
                EndMinute = appointment.DurationInMinutes;

                //var h = TimeSpan.FromMinutes(appointment.DurationInMinutes);
                //var minutes= string.Format("{0:00}", (int)h.Minutes);
                //EndMinute = Convert.ToInt32(minutes);


                CompanyId = appointment.CompanyId;
                obj = new UpdateAppointments();
                obj.BookingId = appointment.BookingId;
                obj.CompanyId = appointment.CompanyId;
                obj.Cost = appointment.Cost;
                obj.Currency = appointment.Currency;
                obj.DateOfBooking = appointment.DateOfBooking;
                obj.DurationInHours = appointment.DurationInHours;
                obj.DurationInMinutes = appointment.DurationInMinutes;
                obj.EmployeeId = appointment.EmployeeId;
                obj.EmployeeName = appointment.EmployeeName;
                obj.EndTime = appointment.EndTime;
                obj.ServiceId = appointment.ServiceId;
                obj.ServiceName = appointment.ServiceName;
                obj.StartTime = appointment.StartTime;
                obj.TimePeriod = appointment.TimePeriod;

                dateOfBooking = Convert.ToDateTime(appointment.DateOfBooking.Split(',')[1]);
                book = appointment.DateOfBooking;

                UpdateAppointmentTime.Text = appointment.TimePeriod;

                if (objCust != null)
                {
                    CustID = objCust.Id;
                    AppointmentCustomerName.Text = objCust.FirstName;
                    AppointmentCustomerEmail.Text = objCust.Email;
                    AppointmentCustomerMobNo.Text = objCust.TelephoneNo;
                }
                BindingContext = appointment;
                Data = new Dictionary<string, int>
                {
                    { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
                   { "No-Show",5}, { "Paid",6},{ "Running Late",7}, { "Custom Label",8},
                };

                foreach (var item in Data.Keys)
                {
                    AppointmentsPicker.Items.Add(item);
                }
                appointment.Status = appointment.Status - 1;
                AppointmentsPicker.SelectedIndex = appointment.Status;

            //    Data = new Dictionary<string, int>
            //{
            //   { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
            //   { "No-Show",5}, { "Paid",6},{ "Running Late",7}, { "Custom Label",8},
            //};
            //    foreach (var item in Data.Keys)
            //    {
            //        AppointmentsPicker.Items.Add(item);

            //    }
            //    obj.status = Convert.ToInt32(obj.status) - 1;
            //    AppointmentsPicker.SelectedIndex = obj.status;



                service = new Service();
                service.Id = appointment.ServiceId;
                service.Name = appointment.ServiceName;
                service.Cost = appointment.Cost;
                addAppointments = new AddAppointments();
                addAppointments.CompanyId = appointment.CompanyId;
                addAppointments.EmployeeId = EmpID;
                addAppointments.EmployeeName = empName;
                addAppointments.ServiceId = ServiceID;
                addAppointments.ServiceName = ServiceName;
                addAppointments.Cost = Cost;
                addAppointments.StartTime = appointment.StartTime;
                addAppointments.EndTime = appointment.EndTime;
                addAppointments.TimePeriod = appointment.StartTime;

                //var NotesList = GetAllCustomerNotes();

               

                //day = Day;
                //dateOfBooking = DateOfBooking;
                //EmpID = appointment.EmployeeId;
                //empName = appointment.EmployeeName;
                //Cost = appointment.Cost;
                //CustID = objCust.Id;
                //ServiceID = appointment.ServiceId;
                //ServiceName = appointment.ServiceName;
                //service = new Service();
                //service.Id = Convert.ToInt32(appointment.ServiceId);
                //service.Name = appointment.ServiceName;
                //service.Cost = appointment.Cost;
                //addAppointments = new AddAppointments();
                //addAppointments.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                //addAppointments.EmployeeId = EmpID;
                //addAppointments.EmployeeName = empName;
                //addAppointments.ServiceId = ServiceID;
                //addAppointments.ServiceName = ServiceName;
                //addAppointments.Cost = Cost;
                //addAppointments.StartTime = appointment.StartTime;
                //addAppointments.EndTime = appointment.EndTime;
                //addAppointments.TimePeriod = appointment.StartTime;
                //if (objCust != null)
                //{
                //    AppointmentCustomerName.Text = objCust.FirstName;
                //    AppointmentCustomerEmail.Text = objCust.Email;
                //    AppointmentCustomerMobNo.Text = objCust.TelephoneNo;
                //}
                //ObservableCollection<Notes> notesLst = new ObservableCollection<Notes>();
                //foreach (var data in NotesList)
                //{
                //    Notes obj = new Notes();
                //    obj.CompanyId = data.CompanyId;
                //    obj.CreationDate = Convert.ToDateTime(data.CreationDate);
                //    obj.CustomerId = data.CustomerId;
                //    obj.Description = data.Description;
                //    obj.WhoAddedThis = data.WhoAddedThis;
                //    notesLst.Add(obj);
                //}
                //notesLst.OrderByDescending(x => x.CreationDate);
                //UpdateComment.Text = notesLst[0].Description;
                //foreach (var item in NotesList)
                //{
                //    UpdateComment.Text = item.Description;
                //}
                //string strttime = appointment.StartTime.Split('-')[0];
                //string endtime = appointment.StartTime.Split('-')[1];
                //obj = new UpdateAppointments();
                //obj.EmployeeId = appointment.EmployeeId;
                //obj.ServiceId = appointment.ServiceId;
                //obj.EmployeeName = appointment.EmployeeName;
                //obj.ServiceName = appointment.ServiceName;
                //obj.DurationInHours = appointment.DurationInHours;
                //obj.DurationInMinutes = appointment.DurationInMinutes;
                //obj.Cost = appointment.Cost;
                //obj.Currency = appointment.Currency;
                //obj.StartTime = strttime;
                //obj.EndTime = endtime;
                //obj.DateOfBooking = day + "," + dateOfBooking.ToString("dd-MMM-yyyy");
                //obj.CommentNotes = objNotes == null ? "" : objNotes.Description;
                //obj.TimePeriod = appointment.StartTime;
                //BindingContext = obj;
                ////string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };

                //Data = new Dictionary<string, int>
                //{
                //    { "No Label",1}, { "Pending",2}, { "Confirmed",3}, { "Done",4},
                //   { "No-Show",5}, { "Paid",6},{ "Running Late",7}, { "Custom Label",8},
                //};

                //foreach (var item in Data.Keys)
                //{
                //    AppointmentsPicker.Items.Add(item);
                //}


                //// var id = Data.TryGetValue("No Label", out StatusId);
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

        private void EditServiceForAppointmentClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new SelectServiceCategory("EditAppointment", DateofBooking,StatusId));
        }


        private void UpdateAppointmentbyBookingDateClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceID, ServiceName, EmpID, empName, Cost, DurationInHours, DurationInMinutes, "EditAppointment", DateofBooking,StatusId));
        }

        private void EditCommentClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new CustomerCommentsForAppointmentPage(addAppointments, objCust, day, dateOfBooking, "EditAppointment"));
            Navigation.PushAsync(new AddNotesPage());
        }

        private void UpdateAppointmentbyStaffClick(object sender, EventArgs e)
        {
            //Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            //string ssss = string.Empty;
            //var s = grid.Children[0];
            //Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            //var selectedD = label.Text;
            var DateofBooking = Convert.ToDateTime(dateOfBooking).ToString();
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, "EditAppointment", DateofBooking,StatusId));
        }

        public void UpdateAppointments()
        {
            try
            {
                //string[] AppointmentDate = { };
                //string[] TimeAppointment = { };
                //string[] hours = { };
                //string[] Endmins = { };
                //string[] Endmin = { };
                //string Time = UpdateAppointmentTime.Text;
                //if (Time != null)
                //{
                //    TimeAppointment = Time.Split('-');
                //    hours = TimeAppointment[0].Split(':');
                //    Endmins = TimeAppointment[1].Split(':');
                //    Endmin = Endmins[1].Split(' ');
                //}
                //if (AppointmentsPicker.SelectedItem != null)
                //{
                //    string selectedValue = (AppointmentsPicker.SelectedItem).ToString();
                //    Data.TryGetValue(selectedValue, out StatusId);
                //}
                //var GetAllCustomerData = GetAllCustomer();
                //List<int> custIDs = GetAllCustomerData.Select(z => z.Id).ToList();
                //UpdatebookAppointment = new UpdateBookAppointment();
                //UpdatebookAppointment.Id = Application.Current.Properties["BookingID"].ToString();
                //UpdatebookAppointment.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                //UpdatebookAppointment.EmployeeId = EmpID;
                //UpdatebookAppointment.ServiceId = ServiceID;
                //UpdatebookAppointment.CustomerIdsCommaSeperated = CustID.ToString();
                //UpdatebookAppointment.StartHour = Convert.ToInt32(hours[0]);
                //UpdatebookAppointment.StartMinute = 0;
                //UpdatebookAppointment.EndHour = 0;
                //UpdatebookAppointment.EndMinute = Convert.ToInt32(Endmin[0]);
                //UpdatebookAppointment.IsAdded = true;
                //UpdatebookAppointment.Message = UpdateComment.Text;
                //UpdatebookAppointment.Notes = UpdateComment.Text;
                //UpdatebookAppointment.CustomerIds = custIDs;
                //UpdatebookAppointment.Start = dateOfBooking;
                //UpdatebookAppointment.End = dateOfBooking;
                //UpdatebookAppointment.Status = StatusId;

                UpdatebookAppointment = new UpdateBookAppointment();
                UpdatebookAppointment.Id = BookingID;
                UpdatebookAppointment.CompanyId = CompanyId;
                UpdatebookAppointment.ServiceId = ServiceID;
                UpdatebookAppointment.EmployeeId = EmpID;
                UpdatebookAppointment.CustomerIdsCommaSeperated = CustID.ToString();
                UpdatebookAppointment.StartHour = StartHour;
                UpdatebookAppointment.StartMinute = StartMinutes;
                UpdatebookAppointment.EndHour = EndHour;
                UpdatebookAppointment.EndMinute = EndMinute;
                UpdatebookAppointment.IsAdded = true;
                UpdatebookAppointment.Message = "";

                var GetAllCustomerData = GetAllCustomer();
                List<int> custIDs = GetAllCustomerData.Select(x => x.Id).ToList();
                custIDs = custIDs.Where(x => x == CustID).ToList();
                UpdatebookAppointment.CustomerIds = custIDs;
                var sDate = dateOfBooking.ToString("yyyy-MM-dd T HH:mm:ss");
                UpdatebookAppointment.Start = sDate;
                var eeDate = dateOfBooking.ToString("yyyy-MM-dd T HH:mm:ss");
                UpdatebookAppointment.End = eeDate;
                if (AppointmentsPicker.SelectedItem != null)
                {
                    string selectedValue = (AppointmentsPicker.SelectedItem).ToString();
                    Data.TryGetValue(selectedValue, out StatusId);
                }
                UpdatebookAppointment.Status = StatusId;
                UpdatebookAppointment.Notes = "";





                var SerializedData = JsonConvert.SerializeObject(UpdatebookAppointment);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/UpdateBooking";
                var result = PostData("POST", SerializedData, apiUrl);
                BookAppointment objBookappointments = new BookAppointment();
                objBookappointments.CompanyId = UpdatebookAppointment.CompanyId;
                objBookappointments.CustomerIdsCommaSeperated = UpdatebookAppointment.CustomerIdsCommaSeperated;
                objBookappointments.EmployeeId = UpdatebookAppointment.EmployeeId;
                objBookappointments.ServiceId = UpdatebookAppointment.ServiceId;
                objBookappointments.StartHour = UpdatebookAppointment.StartHour;
                objBookappointments.StartMinute = UpdatebookAppointment.StartMinute;
                objBookappointments.EndHour = UpdatebookAppointment.EndHour;
                objBookappointments.EndMinute = UpdatebookAppointment.EndMinute;
                objBookappointments.IsAdded = UpdatebookAppointment.IsAdded;
                objBookappointments.Message = UpdatebookAppointment.Message;
                objBookappointments.Notes = UpdatebookAppointment.Notes;
                objBookappointments.CustomerIds = UpdatebookAppointment.CustomerIds;
                objBookappointments.Start = UpdatebookAppointment.Start.ToString();
                objBookappointments.End = UpdatebookAppointment.End.ToString();
                objBookappointments.Status = UpdatebookAppointment.Status;

                dynamic data = JObject.Parse(result);
                var msg = Convert.ToString(data.Message);
                DisplayAlert("Success", msg, "ok");

                for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                }

                // Navigation.PopAsync(true);

                Navigation.PushAsync(new AddAppointmentsPage(objBookappointments));                
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

        public string SetStatusOfAppointment()
        {
            try
            {
                string selectedValue = (AppointmentsPicker.SelectedItem).ToString();
                Data.TryGetValue(selectedValue, out StatusId);
                string apiUrl = Application.Current.Properties["DomainUrl"] + "api/booking/SetStatus?status=" + StatusId + "&bookingId=" + Application.Current.Properties["BookingID"];
                string result = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));
                httpWebRequest.ContentLength = 0;

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