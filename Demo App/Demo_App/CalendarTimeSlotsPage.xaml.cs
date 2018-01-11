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
	public partial class CalendarTimeSlotsPage : ContentPage
	{
        #region GloblesFields

        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public static SfSchedule schedulee;
        int serviceID;
        int EmployeeId;
        string EmployeeName = "";
        string CurrentSelectedDay = "";
        string PageName = "";
        DateTime SelectedDateOfBooking;        
        ObservableCollection<StaffWorkingHours> ListofFreetimeSlots = new ObservableCollection<StaffWorkingHours>();
        ObservableCollection<AddAppointments> listofnewAppointmentDetail = new ObservableCollection<AddAppointments>();
        public AddAppointments objAddAppointment = null;
        public Customer objCust = null;      
        #endregion
        public CalendarTimeSlotsPage (AssignedServicetoStaff obj,int EmpID,string EmpName, string pagename)
		{
			InitializeComponent ();
            EmployeeId = EmpID;
            EmployeeName = EmpName;
            serviceID = obj.Id;
            PageName = pagename;
            objAddAppointment = new AddAppointments();
            objAddAppointment.ServiceId = serviceID;
            objAddAppointment.ServiceName = obj.Name;
            objAddAppointment.EmployeeId = EmployeeId;
            objAddAppointment.EmployeeName = EmployeeName;
            objAddAppointment.Cost = obj.Cost;
            objAddAppointment.DurationInHours = obj.DurationInHours;
            objAddAppointment.DurationInMinutes = obj.DurationInMinutes;
            schedulee = new SfSchedule();
            var CurrentDate = DateTime.Now;
            DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            schedulee.NavigateTo(SpecificDate);
            schedule.CellTapped += GetAvailableTimeForAppointments;
        }

        private void GetAvailableTimeForAppointments(object sender, CellTappedEventArgs e)
        {
            //DisplayAlert("CustomizeHeader", "888888888", "cancel");
            var currentDay = e.Datetime.DayOfWeek;
            var dateOfBooking = e.Datetime.Date;
            CurrentSelectedDay = currentDay.ToString();
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
                    try
                    {
                        string Start = (string)data["Start"];
                        DateTime StartTime = DateTime.ParseExact(Start, "HH:mm:ss",
                                     CultureInfo.InvariantCulture);
                        string StartTimeSlot = String.Format("{0:t}", StartTime);

                        string End = (string)data["End"];

                        DateTime EndTime = DateTime.ParseExact(End, "HH:mm:ss",
                                     CultureInfo.InvariantCulture);
                        string EndTimeSlot = String.Format("{0:t}", EndTime);
                        string time = StartTimeSlot + "-" + EndTimeSlot;
                        timeSlots.Add(time);
                    }
                    catch (Exception ex)
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

        private void SelectCustomerForAppointmentClick(object sender, SelectedItemChangedEventArgs e)
        {
            var data = e.SelectedItem;
            objAddAppointment.TimePeriod = data.ToString();
            //AppointmentDetails objAppointment = new AppointmentDetails();
            if (PageName == "CalandarAppointment")
            {
                Navigation.PushAsync(new CustomerForCalendarAppointmentPage(objAddAppointment));
            }
            else
            {
                //Navigation.PushAsync(new NewAppointmentPage(objAddAppointment, objCust, CurrentSelectedDay, SelectedDateOfBooking, objnotes));
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