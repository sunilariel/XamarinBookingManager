using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Syncfusion.SfSchedule.XForms;
using XamForms.Controls;
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
        //public static SfSchedule schedulee;
        int serviceID;
        
        string CurrentSelectedDay;
        int EmployeeId;
        string PageName = "";
        
        DateTime SelectedDateOfBooking;
        ObservableCollection<StaffWorkingHours> ListofFreetimeSlots = new ObservableCollection<StaffWorkingHours>();
        ObservableCollection<AddAppointments> listofnewAppointmentDetail = new ObservableCollection<AddAppointments>();
        public AddAppointments objAddAppointment = null;
        public Customer objCust = null;
        #endregion
        public CalendarTimeSlotsPage(AddAppointments obj, string pagename)
        {
            try
            {
                InitializeComponent();
                EmployeeId = obj.EmployeeId; 
                //EmployeeName = EmpName;
                serviceID = obj.ServiceId;
                PageName = pagename;
                //TimePeriods=tPeriod;
                CurrentSelectedDay = obj.DateOfBooking;
                objAddAppointment = new AddAppointments();
                objAddAppointment.CompanyId = obj.CompanyId;
                objAddAppointment.Cost = obj.Cost;
                objAddAppointment.Currency = obj.Currency;



                //objAddAppointment.DateOfBooking = obj.DateOfBooking;

                var selectDate = Convert.ToDateTime(obj.DateOfBooking);
                var dateOfBookings = selectDate.ToString("dd-MM-yyyy");
                var currentDay = selectDate.DayOfWeek;
                CurrentSelectedDay = currentDay.ToString();
                SelectedDateOfBooking = selectDate;

                var s = SelectedDateOfBooking.ToString("dd-MM-yyyy");

                var BookingDate = CurrentSelectedDay + "," + SelectedDateOfBooking.ToString("dd-MMM-yyyy");
                objAddAppointment.DateOfBooking = BookingDate;



                objAddAppointment.DurationInHours = obj.DurationInHours;
                objAddAppointment.DurationInMinutes = obj.DurationInMinutes;
                objAddAppointment.EmployeeId = obj.EmployeeId;
                objAddAppointment.EmployeeName = obj.EmployeeName;
                objAddAppointment.EndTime = obj.EndTime;
                objAddAppointment.ServiceId = obj.ServiceId;
                objAddAppointment.ServiceName = obj.ServiceName;
                objAddAppointment.StartTime = obj.StartTime;
                objAddAppointment.TimePeriod = obj.TimePeriod;                                                                       
                GetAvailableTime();
                //schedulee = new SfSchedule();
                var CurrentDate = DateTime.Now;
                DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                //schedulee.NavigateTo(SpecificDate);
                //schedule.CellTapped += GetAvailableTimeForAppointments;
                calender.SelectedDate = Convert.ToDateTime(obj.DateOfBooking);
                calender.DateClicked += GetAvailableTimeForAppointments;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void GetAvailableTime()
        {
            try
            {
                //var currentDay = DateTime.Now.DayOfWeek;
                //var dateOfBooking = DateTime.Now;
                //CurrentSelectedDay = currentDay.ToString();
                //SelectedDateOfBooking = dateOfBooking;
                //var BookingDate = CurrentSelectedDay + "," + SelectedDateOfBooking.ToString("dd-MMM-yyyy");
                //objAddAppointment.DateOfBooking = BookingDate;


                var selectDate = Convert.ToDateTime(SelectedDateOfBooking);
                var dateOfBookings = selectDate.ToString("dd-MM-yyyy");
                var currentDay = selectDate.DayOfWeek;
                CurrentSelectedDay = currentDay.ToString();
                SelectedDateOfBooking = selectDate;
                var datet = SelectedDateOfBooking.ToString("dd-MMM-yyyy");

                string url = Convert.ToString(Application.Current.Properties["DomainUrl"]);
                var apiUrl = url + "api/booking/GetFreeBookingSlotsForEmployee?companyId=" + Convert.ToInt32(CompanyId) + "&serviceId=" + serviceID + "&employeeId=" + EmployeeId + "&dateOfBooking=" + dateOfBookings + "&day=" + CurrentSelectedDay;
                var result = PostData("GET", "", apiUrl);

                bool hasValue = true;
                ListofTimeSlots.IsVisible = true;
                TimeSlotFrame.IsVisible = false;
                if (result.Contains("Key\":null"))
                {
                    
                    ListofTimeSlots.IsVisible = false;
                    TimeSlotFrame.IsVisible = true;
                    TimeSlotlbel.Text = "No Slots available for " + datet;
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void GetAvailableTimeForAppointments(object sender, DateTimeEventArgs e)
        {
            try
            {
                //DisplayAlert("CustomizeHeader", "888888888", "cancel");

                var currentDay = e.DateTime.DayOfWeek;
                var dateOfBooking = e.DateTime.Date;
                CurrentSelectedDay = currentDay.ToString();
                SelectedDateOfBooking = dateOfBooking;
                var BookingDate = CurrentSelectedDay + "," + SelectedDateOfBooking.ToString("dd-MMM-yyyy");
                objAddAppointment.DateOfBooking = BookingDate;
                string url = Convert.ToString(Application.Current.Properties["DomainUrl"]);
                var apiUrl = url + "api/booking/GetFreeBookingSlotsForEmployee?companyId=" + Convert.ToInt32(CompanyId) + "&serviceId=" + serviceID + "&employeeId=" + EmployeeId + "&dateOfBooking=" + dateOfBooking.ToString("dd-MM-yyyy") + "&day=" + currentDay.ToString();
                var result = PostData("GET", "", apiUrl);

                bool hasValue = true;
                ListofTimeSlots.IsVisible = true;
                TimeSlotFrame.IsVisible = false;
                if (result.Contains("Key\":null"))
                {
                    ListofTimeSlots.IsVisible = false;
                    TimeSlotFrame.IsVisible = true;
                    TimeSlotlbel.Text = "No Slots available for " + dateOfBooking.ToString("dd-MMM-yyyy");

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
                else
                {

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void SelectCustomerForAppointmentClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;
                
                var data = e.SelectedItem;
                var time = data.ToString();
                var t = Convert.ToString(time.Split(' ')[0]);
                var StartTime = Convert.ToString(t.Split(':')[0]);
                var EndTime = Convert.ToString(t.Split(':')[1]);
                objAddAppointment.StartTime = StartTime;
                objAddAppointment.EndTime = EndTime;


                objAddAppointment.TimePeriod = data.ToString();
                //AppointmentDetails objAppointment = new AppointmentDetails();
                if (PageName == "CalandarAppointment")
                {
                    Navigation.PushAsync(new CustomerForCalendarAppointmentPage(objAddAppointment, ""));
                }
                else if (PageName == "ECalandarAppointment")
                {
                    Navigation.PushAsync(new CalendarCreateAppointmentPage(objAddAppointment));
                }
                ((ListView)sender).SelectedItem = null;
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