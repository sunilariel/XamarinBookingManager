using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfSchedule.XForms;
using Demo_App.Model;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CalenderPage : ContentPage
    {
        #region GloblesVaribles
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public static bool isCalenderPageOpen = false;
        public static SfSchedule schedulee;
        public bool IsMonthView = false;
        int i = 0;
        #endregion

        public CalenderPage()
        {

            isCalenderPageOpen = true;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var firstLabel = new Label
            {
                Text = "Label 1",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = Xamarin.Forms.Color.FromHex("#000000")
            };
            schedulee = new SfSchedule();
            //this.Content = schedulee;
            //schedulee.ScheduleView = ScheduleView.MonthView;                    
            //var CurrentDate = DateTime.Now;
            //DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            //schedulee.NavigateTo(SpecificDate);       
            // schedulee.VisibleDatesChangedEvent += Schedule_VisibleDatesChangedEvent;            
        }

        //private void Schedulee_ScheduleCellTapped(object sender, ScheduleTappedEventArgs e)
        //{
        //    Navigation.PushAsync(new GetAllocateServiceForEmployeePage(EmpID))
        //}

        public static SfSchedule getScheduleObj()
        {
            return schedulee;
        }


        //private void Schedule_VisibleDatesChangedEvent(object sender, VisibleDatesChangedEventArgs args)
        //{

        //}

        private void ChangeMonthView(object sender, EventArgs e)
        {
            IsMonthView = !IsMonthView;
            if (IsMonthView)
            {
                dropdownArrow.RotateTo(180, 200, Easing.SinInOut);
                schedulerFullMonthView.IsVisible = true;
                schedulerWeekView.IsVisible = false;
            }
            else
            {
                dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                schedulerFullMonthView.IsVisible = false;
                schedulerWeekView.IsVisible = true;
            }
        }

        public string currentWeek()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(
      (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
      (int)DateTime.Today.DayOfWeek);

            string result = string.Join("," + Environment.NewLine, Enumerable
              .Range(0, 7)
              .Select(i => startOfWeek
                 .AddDays(i)
                 .ToString("dd")));

            return result;
        }


        private void AddNewAppointment(object sender, EventArgs e)
        {
            //Navigation.PushAsync();
            // MonAppointmentBox.Children.Add(firstLabel);
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
            var result = PostData("POST", jsonString, apiURL);
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