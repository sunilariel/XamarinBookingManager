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
        string[] currentWeek = new string[7];
        public AppointmentDetails obj = null;
        int EmployeeId;
        ObservableCollection<AppointmentDetails> ListofAppointment = new ObservableCollection<AppointmentDetails>();
        #endregion

        public CalenderPage()
        {
            currentWeek = GetCurrentWeek();
            BindingContext = currentWeek;
            if (Application.Current.Properties.ContainsKey("LastSelectedStaff") == true)
            {
                if (Application.Current.Properties.ContainsKey("SelectedEmpId") == true)
                {
                    EmployeeId = Convert.ToInt32(Application.Current.Properties["SelectedEmpId"]);
                }
              var datalist = GetAppointmentBookingByEmployeeID();
                //foreach (var list in datalist)
                //{
                //    var firstLabel = new Label
                //    {                        
                //        HorizontalOptions = LayoutOptions.StartAndExpand,
                //        TextColor = Xamarin.Forms.Color.FromHex("#000000")
                //    };
                //    firstLabel.Text = list.AppointmentDetail;
                //    //MonAppointmentBox.Children.Add(firstLabel);
                //}
            }
            isCalenderPageOpen = true;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            WeekMonlbl.Text = currentWeek[0];
            WeekTuelbl.Text = currentWeek[1];
            WeekWedlbl.Text = currentWeek[2];
            WeekThulbl.Text = currentWeek[3];
            WeekFrilbl.Text = currentWeek[4];
            WeekSatlbl.Text = currentWeek[5];
            WeekSunlbl.Text = currentWeek[6];

            Mondaylbl.Text ="MON"+" "+ currentWeek[0];
            Tuesdaylbl.Text ="TUE"+" "+ currentWeek[1];
            Wednesdaylbl.Text ="WED"+" "+ currentWeek[2];
            Thursdaylbl.Text ="THU"+" "+ currentWeek[3];
            Fridaylbl.Text ="FRI"+" "+ currentWeek[4];
            Saturdaylbl.Text ="SAT"+" "+ currentWeek[5];
            Sundaylbl.Text = "SUN"+" "+ currentWeek[6];
            schedulee = new SfSchedule();
            ViewHeaderStyle viewHeaderStyle = new ViewHeaderStyle();
            viewHeaderStyle.DayTextColor = Color.Black;
            viewHeaderStyle.DayTextStyle = Font.OfSize("Arial", 15);
            schedulee.ViewHeaderStyle = viewHeaderStyle;
            //MonthViewSettings month = new MonthViewSettings();
            //month.MonthLabelSettings.DayFormat = "E";
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
                MonthViewSettings monthViewSettings = new MonthViewSettings();
                monthViewSettings.MonthLabelSettings.DayFormat = "E";


            }
            else
            {

                dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                schedulerFullMonthView.IsVisible = false;
                schedulerWeekView.IsVisible = true;
            }
        }

        public string[] GetCurrentWeek()
        {

            //CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();

            _culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            _uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _uiculture;

            DateTime startOfWeek = DateTime.Today.AddDays(
      (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
      (int)DateTime.Today.DayOfWeek);

            string result = string.Join(",", Enumerable
              .Range(0, 7)
              .Select(i => startOfWeek
                 .AddDays(i)
                 .ToString("dd")));

            return result.Split(',');
        }


        private void AddNewAppointment(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("LastSelectedStaff") == true)
            {
                Application.Current.MainPage.Navigation.PushAsync(new GetAllocateServiceForEmployeePage());
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new SelectServiceCategory("NewCalAppointment"));
            }
           
            // MonAppointmentBox.Children.Add(firstLabel);
        }

        public ObservableCollection<AppointmentDetails> GetAppointmentBookingByEmployeeID()
        {
            try
            {
                string[] StartTime = { };
                var startDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(-1)).ToString("dd-MM-yyyy");
                var endDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(1)).ToString("dd-MM-yyyy");
                string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetBookingsForEmployeesByIdBetweenDates?companyId=" + CompanyId + "&commaSeperatedEmployeeIds=" + EmployeeId + "&startDate=" + startDate + "&endDate=" + endDate;

                var result = PostData("GET", "", apiURL);

                ObservableCollection<AllAppointments> appointments = JsonConvert.DeserializeObject<ObservableCollection<AllAppointments>>(result);
                ListofAppointment = new ObservableCollection<AppointmentDetails>();
                foreach (var appointment in appointments)
                {                  
                    string DurationHours = Convert.ToString(appointment.Service.DurationInMinutes == null ? 0 : appointment.Service.DurationInMinutes / 60);
                    string durmin = Convert.ToString(appointment.Service.DurationInMinutes == null ? 0 : appointment.Service.DurationInMinutes % 60); ;
                    string durhrs = DurationHours + "hrs";
                    string durationMins = durmin + "mins";
                    string Duration = durhrs + " " + durationMins;
                    var datebooking = appointment.Start;
                    var DateOFbooking = Convert.ToDateTime(datebooking).ToString("dd-MMM-yyyy");
                    string detail = appointment.Employee.FirstName + "," + appointment.Service.Name + "," + Duration + "," + appointment.Service.Cost;
                    DateTime startTime = Convert.ToDateTime(appointment.Start);
                    string Time = startTime.ToShortTimeString();
                    var DateTimeofBooking = DateOFbooking + "," + Time;
                    obj = new AppointmentDetails();
                    obj.BookingId = appointment.Id;
                    obj.EmployeeId = appointment.EmployeeId.ToString();
                    obj.ServiceId = appointment.ServiceId.ToString();
                    obj.EmployeeName = (appointment.Employee) == null ? "" : appointment.Employee.FirstName;
                    obj.ServiceName = (appointment.Service) == null ? "" : appointment.Service.Name;
                    obj.DurationInHours = (appointment.Service) == null ? 0 : appointment.Service.DurationInHours;
                    obj.DurationInMinutes = (appointment.Service) == null ? 0 : appointment.Service.DurationInMinutes;
                    obj.Cost = (appointment.Service) == null ? 0 : appointment.Service.Cost;
                    obj.Currency = (appointment.Service) == null ? "" : appointment.Service.Currency;
                    obj.status = appointment.Status;
                    obj.StartTime = appointment.Start;
                    obj.EndTime = appointment.End;
                    obj.BookingDate = DateTimeofBooking;
                    obj.Colour = (appointment.Service) == null ? "" : appointment.Service.Colour;
                    obj.DurationHrsMin = Duration;
                    obj.AppointmentDetail = detail;
                    obj.CommentNotes = appointment.Notes;
                    ListofAppointment.Add(obj);

                }

            }
            catch (Exception e)
            {

            }
            
            //CustomerAppoimentList.ItemsSource = ListofAppointment;
            return ListofAppointment;
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