using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telerik.XamarinForms.Input;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using Demo_App.Model;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

//using Com.Syncfusion.Schedule;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CalenderPage : ContentPage
    {
        public static bool isCalenderPageOpen = false;
        public static SfSchedule schedulee;
        int i = 0;
        public CalenderPage()
        {

            isCalenderPageOpen = true;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //var calendar = new RadCalendar();                      
            schedulee = new SfSchedule();
            this.Content = schedulee;

            schedulee.ScheduleView = ScheduleView.MonthView;
                    
            var CurrentDate = DateTime.Now;
            DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            schedulee.NavigateTo(SpecificDate);
       
            schedulee.VisibleDatesChangedEvent += Schedule_VisibleDatesChangedEvent;
            schedulee.OnAppointmentLoadedEvent += Schedule_OnAppointmentLoadedEvent;

            //schedulee.OnMonthCellLoadedEvent += Schedule_onMonthRenderedEvent;

            ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();
            //Creating new event   
            ScheduleAppointment clientMeeting = new ScheduleAppointment();
            DateTime currentDate = DateTime.Now;
            DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 10, 0, 0);
            DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0);
            clientMeeting.StartTime = startTime;
            clientMeeting.EndTime = endTime;
            clientMeeting.Color = Color.Blue;
            clientMeeting.Subject = "ClientMeeting";
            appointmentCollection.Add(clientMeeting);
            schedulee.DataSource = appointmentCollection;
        }

        public static SfSchedule getScheduleObj()
        {
            return schedulee;
        }
       
       
       private void schedule_celltapped(object sender, CellTappedEventArgs e)
        {
            
        }

        private void Schedule_onMonthRenderedEvent(object sender, MonthCellLoadedEventArgs args)
        {
            
        }

        private void Schedule_OnAppointmentLoadedEvent(object sender, AppointmentLoadedEventArgs args)
        {

        }

        private void Schedule_VisibleDatesChangedEvent(object sender, VisibleDatesChangedEventArgs args)
        {
          
        }


        private void MyButton_Click(object sender, EventArgs e)
        {
            // var result = DisplayAlert("click", "you click on me?", "Yes", "Cancel");

        }

        private void PivotItem_Tapped(object sender, EventArgs e)
        {
            
        }

        public string GetBookingsForEmployeesByIdBetweenDates(string CompanyId, string commaSeperatedEmployeeIds, string StartDate, string EndDate)
        {               
              var startDate = StartDate.Split('T')[0];
              var endDate = EndDate.Split('T')[0];
              string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/GetBookingsForEmployeesByIdBetweenDates?companyId=" + CompanyId + "&commaSeperatedEmployeeIds=" + commaSeperatedEmployeeIds + "&startDate=" + startDate + "&endDate=" + endDate;

               var result = PostData("GET", "", apiUrl);             
               return result;          
        }

        public string SetCompanyWorkingHours(ReqWorkingHours dataobj)
        {           
                DateTime starttime = DateTime.Parse(dataobj.Start, CultureInfo.InvariantCulture);
                dataobj.Start = starttime.ToString("HH:mm");

                DateTime endtime = DateTime.Parse(dataobj.End, CultureInfo.InvariantCulture);
                dataobj.End = endtime.ToString("HH:mm");

                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHours";
                var jsonString = JsonConvert.SerializeObject(dataobj);
                var result= PostData("POST", jsonString, apiURL);                   
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