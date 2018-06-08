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

        //int DurationInMinutes;
        //int DurationInHours;
        int StatusID;
        int EmployeeId;
        string EmployeeName = "";
        string CurrentSelectedDay = "";
        string PageName = "";
        DateTime SelectedDateOfBooking;
        //public static WorkingHoursofEmployee objbookingSlot;
        string selectCalendarPageDateofBooking;
        ObservableCollection<WorkingHoursofEmployee> objbookingSlot = new ObservableCollection<WorkingHoursofEmployee>();

        ObservableCollection<StaffWorkingHours> ListofFreetimeSlots = new ObservableCollection<StaffWorkingHours>();
        ObservableCollection<AddAppointments> listofnewAppointmentDetail = new ObservableCollection<AddAppointments>();
        public AddAppointments objAddAppointment = null;
        public Customer objCust = null;
        public Notes objnotes = null;
        #endregion

        public CreateNewAppointmentsPage(int ServiceID, string ServiceName, int EmpID, string empName, double Cost, int DurationInHours, int DurationInMinutes, string pagename, string dateofBooking,int statusID)
        {
            try
            {
                InitializeComponent();
                PageName = pagename;
                serviceID = ServiceID;
                StatusID = statusID;
                EmployeeId = EmpID;
                selectCalendarPageDateofBooking = dateofBooking;
                //DurationInMinutes = DurationInMinutes;
                //DurationInHours = DurationInHours;
                EmployeeName = empName;
                objAddAppointment = new AddAppointments();
                objAddAppointment.CompanyId = Convert.ToInt32(CompanyId);
                objAddAppointment.EmployeeId = EmployeeId;
                objAddAppointment.EmployeeName = EmployeeName;
                objAddAppointment.ServiceId = serviceID;
                objAddAppointment.ServiceName = ServiceName;
                objAddAppointment.Cost = Cost;
                objAddAppointment.DurationInHours = DurationInHours;
                objAddAppointment.DurationInMinutes = DurationInMinutes;
                objAddAppointment.Status = statusID;
                //var TotalHours = DurationInHours;                
                //var Minutes = DurationInMinutes;
                //var t = TotalHours * 60;
                //var totalMinutes = t + Minutes;
                //objAddAppointment.DurationInMinutes = totalMinutes;

                GetAvailableTime();
                schedulee = new SfSchedule();
                var CurrentDate = System.DateTime.Now;
                DateTime SpecificDate = new System.DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                schedulee.NavigateTo(SpecificDate);
                schedule.CellTapped += GetAvailableTimeForAppointments;
                //GetAppointmentWorkinghours();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static SfSchedule getScheduleObj()
        {

            return schedulee;
        }

        //public static WorkingHoursofEmployee getworkinghourofemployee()
        //{
        //    return objbookingSlot;
        //}
        public string GetAppointmentWorkinghours()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + EmployeeId;
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


        private void GetAvailableTimeForAppointments(object sender, CellTappedEventArgs e)
        {
            try
            {

                var currentDay = e.Datetime.DayOfWeek;
                var dateOfBookings = e.Datetime.Date;
                var currentTime = e.Datetime.TimeOfDay;
                CurrentSelectedDay = currentDay.ToString();
                SelectedDateOfBooking = dateOfBookings;

                var d = dateOfBookings.ToString("dd-MM-YYYY");

                var BookingDate = CurrentSelectedDay + "," + SelectedDateOfBooking.ToString("dd-MMM-yyyy");
                objAddAppointment.DateOfBooking = BookingDate;
                string url = Convert.ToString(Application.Current.Properties["DomainUrl"]);
                var apiUrl = url + "api/booking/GetFreeBookingSlotsForEmployee?companyId=" + Convert.ToInt32(CompanyId) + "&serviceId=" + serviceID + "&employeeId=" + EmployeeId + "&dateOfBooking=" + dateOfBookings.ToString("dd-MM-yyyy") + "&day=" + CurrentSelectedDay;
                var result = PostData("GET", "", apiUrl);

                bool hasValue = true;
                ListofTimeSlots.IsVisible = true;
                TimeSlotFrame.IsVisible = false;

                if (result.Contains("Key\":null"))
                {
                    ListofTimeSlots.IsVisible = false;
                    TimeSlotFrame.IsVisible = true;
                    TimeSlotlbel.Text = "No Slots available for " + dateOfBookings.ToString("dd-MMM-yyyy");

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
                            DateTime StartTime = Convert.ToDateTime(Start);
                            string StartTimeSlot = StartTime.ToString("hh:mm tt");
                            StartTimeSlot = StartTimeSlot.ToUpper();

                            //System.DateTime StartTime =System.DateTime.ParseExact(Start, "HH:mm:ss",
                            //             CultureInfo.InvariantCulture);
                            //string StartTimeSlot = String.Format("{0:t}", StartTime);

                            string End = (string)data["End"];
                            DateTime EndTime = Convert.ToDateTime(End);
                            string EndTimeSlot = EndTime.ToString("hh:mm tt");
                            EndTimeSlot = EndTimeSlot.ToUpper();

                            //System.DateTime EndTime =System.DateTime.ParseExact(End, "HH:mm:ss",
                            //             CultureInfo.InvariantCulture);
                            //string EndTimeSlot = String.Format("{0:t}", EndTime);
                            string time = StartTimeSlot + "-" + EndTimeSlot;
                            timeSlots.Add(time);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }


                        //timeSlots.Add(EndTime);
                    }
                    ListofTimeSlots.ItemsSource = timeSlots;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void CreateAppointmentClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var data = e.SelectedItem;
                var time = data.ToString();
                var t = Convert.ToString(time.Split(' ')[0]);
                var StartTime = Convert.ToString(t.Split(':')[0]);
                var EndTime = Convert.ToString(t.Split(':')[1]);
                objAddAppointment.StartTime = StartTime;
                objAddAppointment.EndTime = EndTime;

                objAddAppointment.TimePeriod = data.ToString();
                //AppointmentDetails objAppointment = new AppointmentDetails();

                if (PageName == "EditAppointment")
                {
                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 5; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                    }

                    Navigation.PushAsync(new UpdateAppointmentDetailsPage(objAddAppointment, CurrentSelectedDay, SelectedDateOfBooking));
                }
                else if (PageName == "NewCalAppointment")
                {
                    Navigation.PushAsync(new CustomerForCalendarAppointmentPage(objAddAppointment, ""));
                }
                else if (PageName == "ECalandarAppointment")
                {
                    Navigation.PushAsync(new CalendarCreateAppointmentPage(objAddAppointment));
                }
                else if(PageName == "NewAppointment")
                {
                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 5; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                    }
                    Navigation.PushAsync(new NewAppointmentPage(objAddAppointment, CurrentSelectedDay, SelectedDateOfBooking));
                }
                else
                {
                    Navigation.PushAsync(new NewAppointmentPage(objAddAppointment, CurrentSelectedDay, SelectedDateOfBooking));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public void GetAvailableTime()
        {
            try
            {
                //var currentDay = System.DateTime.Now.DayOfWeek;
                //var dateOfBookings = System.DateTime.Now.ToString("dd-MM-yyyy");
                //CurrentSelectedDay = currentDay.ToString();
                //SelectedDateOfBooking = System.DateTime.Now;


                var selectDate = Convert.ToDateTime(selectCalendarPageDateofBooking);
                var dateOfBookings = selectDate.ToString("dd-MM-yyyy");
                var currentDay = selectDate.DayOfWeek;
                CurrentSelectedDay = currentDay.ToString();
                SelectedDateOfBooking = selectDate;

                var s = SelectedDateOfBooking.ToString("dd-MMM-yyyy");

                var BookingDate = CurrentSelectedDay + "," + SelectedDateOfBooking.ToString("dd-MMM-yyyy");
                objAddAppointment.DateOfBooking = BookingDate;

                string url = Convert.ToString(Application.Current.Properties["DomainUrl"]);
                var apiUrl = url + "/api/booking/GetFreeBookingSlotsForEmployee?companyId=" + Convert.ToInt32(CompanyId) + "&serviceId=" + serviceID + "&employeeId=" + EmployeeId + "&dateOfBooking=" + dateOfBookings + "&day=" + CurrentSelectedDay;
                var result = PostData("GET", "", apiUrl);

                bool hasValue = true;
                ListofTimeSlots.IsVisible = true;
                TimeSlotFrame.IsVisible = false;
                if (result.Contains("Key\":null"))
                {
                    
                    ListofTimeSlots.IsVisible = false;
                    TimeSlotFrame.IsVisible = true;
                    TimeSlotlbel.Text = "No Slots available for " + s;
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
                            DateTime StartTime = Convert.ToDateTime(Start);
                            string StartTimeSlot = StartTime.ToString("hh:mm tt");
                            StartTimeSlot = StartTimeSlot.ToUpper();




                            string End = (string)data["End"];
                            DateTime EndTime = Convert.ToDateTime(End);
                            string EndTimeSlot = EndTime.ToString("hh:mm tt");
                            EndTimeSlot = EndTimeSlot.ToUpper();


                            string time = StartTimeSlot + "-" + EndTimeSlot;
                            timeSlots.Add(time);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                    }
                    ListofTimeSlots.ItemsSource = timeSlots;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
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