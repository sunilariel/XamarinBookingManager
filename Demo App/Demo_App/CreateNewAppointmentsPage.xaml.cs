using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfSchedule.XForms;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewAppointmentsPage : ContentPage
    {
        #region GloblesVariables
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public static SfSchedule schedulee;
        int serviceID;
        int EmployeeId;
        string EmployeeName = "";
        string CurrentSelectedDay = "";
        string PageName = "";
        DateTime SelectedDateOfBooking;
        public static WorkingHoursofEmployee objbookingSlot;
        ObservableCollection<StaffWorkingHours> ListofFreetimeSlots = new ObservableCollection<StaffWorkingHours>();
        ObservableCollection<AddAppointments> listofnewAppointmentDetail = new ObservableCollection<AddAppointments>();
        public AddAppointments objAddAppointment = null;
        public Customer objCust = null;
        public Notes objnotes = null;
        #endregion

        public CreateNewAppointmentsPage(int ServiceID,string ServiceName, int EmpID,string empName,Customer Cust,double Cost,string pagename,Notes objNotes)
        {
            InitializeComponent();
            PageName = pagename;
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            serviceID = ServiceID;
            EmployeeId = EmpID;
            EmployeeName = empName;
            objAddAppointment = new AddAppointments();
            objAddAppointment.CompanyId = Convert.ToInt32(CompanyId);
            objAddAppointment.EmployeeId = EmployeeId;
            objAddAppointment.EmployeeName = EmployeeName;
            objAddAppointment.ServiceId = serviceID;
            objAddAppointment.ServiceName = ServiceName;
            objAddAppointment.Cost = Cost;

            schedulee = new SfSchedule();
            var CurrentDate = DateTime.Now;
            DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            schedulee.NavigateTo(SpecificDate);
            schedule.CellTapped += GetAvailableTimeForAppointments;

            //schedulee.VisibleDatesChangedEvent += Schedule_VisibleDatesChangedEvent;
            //schedulee.OnAppointmentLoadedEvent += Schedule_OnAppointmentLoadedEvent;


            //schedulee.OnMonthCellLoadedEvent += Schedule_onMonthRenderedEvent;

        }

        public static SfSchedule getScheduleObj()
        {
            return schedulee;
        }

        private void GetAvailableTimeForAppointments(object sender, CellTappedEventArgs e)
        {
            //DisplayAlert("CustomizeHeader", "888888888", "cancel");
            var currentDay = e.Datetime.DayOfWeek;           
            var dateOfBooking = e.Datetime.Date;
            CurrentSelectedDay= currentDay.ToString();
            SelectedDateOfBooking = dateOfBooking;
            string url = Convert.ToString(Application.Current.Properties["DomainUrl"]);         
            var apiUrl = url + "api/booking/GetFreeBookingSlotsForEmployee?companyId=" + Convert.ToInt32(CompanyId) + "&serviceId=" + serviceID + "&employeeId=" + EmployeeId + "&dateOfBooking=" + dateOfBooking.ToString("dd-MM-yyyy") + "&day=" + currentDay.ToString();
            var result = PostData("GET", "", apiUrl);

            bool hasValue = true;
            if (result.Contains("Key\":null"))
            {
                hasValue = false;
            }
            if (hasValue == true)
            {
                JObject json = JObject.Parse(result);
                JArray Value = (JArray)json["Value"];                
                List<string> timeSlots = new List<string>();
                foreach (var data in Value)
                {
                    try {
                        string Start = (string)data["Start"];
                        DateTime StartTime = DateTime.ParseExact(Start, "HH:mm:ss",
                                     CultureInfo.InvariantCulture);
                        string StartTimeSlot = String.Format("{0:t}", StartTime);
                        
                        string End= (string)data["End"];

                        DateTime EndTime = DateTime.ParseExact(End, "HH:mm:ss",
                                     CultureInfo.InvariantCulture);
                        string EndTimeSlot = String.Format("{0:t}", EndTime);
                        string time = StartTimeSlot + "-" + EndTimeSlot;
                        timeSlots.Add(time);
                    }
                    catch(Exception ex)
                    {
                       
                    }
             

                    //timeSlots.Add(EndTime);
                }
                ListofTimeSlots.ItemsSource = timeSlots;                
            }
            else
            {

            }

        }

        private void CreateAppointmentClick(object sender,SelectedItemChangedEventArgs e)
        {
            var data = e.SelectedItem;
            objAddAppointment.StartTime = data.ToString();
            //AppointmentDetails objAppointment = new AppointmentDetails();

            if (PageName == "EditAppointment")
            {
                Navigation.PushAsync(new UpdateAppointmentDetailsPage(objCust, objAddAppointment, CurrentSelectedDay, SelectedDateOfBooking, objnotes));
            }
            else
            {
                Navigation.PushAsync(new NewAppointmentPage(objAddAppointment, objCust, CurrentSelectedDay, SelectedDateOfBooking, objnotes));
            }
        }

        public string PostData(string Method, string SerializedData, string Url)
        {
            try
            {
                var result = "";
                var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                //HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(Url);
                httpRequest.Method = Method;
                httpRequest.ContentType = "application/json";              
                //httpRequest.ProtocolVersion = HttpVersion.Version10;
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