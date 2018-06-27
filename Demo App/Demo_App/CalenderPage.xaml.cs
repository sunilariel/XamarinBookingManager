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
using System.Threading;
using XamForms.Controls;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CalenderPage : ContentPage
    {
        #region GloblesVaribles
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryId;
        string CategoryName;
        public static bool isCalenderPageOpen = false;
        public static SfSchedule schedulee;
        public bool IsMonthView = false;
        int i = 0;
        int statusId;
        string[] currentWeek = new string[7];
        //string WeekMonth;

        //string EmpName = "";
        string selectesPageN = "";

        public CalenderAppointmentDetail obj = null;
        public WeekofDay objweek = null;
        int EmployeeId;
        string empName = "";
        ObservableCollection<CalenderAppointmentDetail> ListofAppointments = new ObservableCollection<CalenderAppointmentDetail>();
        List<string> week = new List<string>();
        ObservableCollection<WeekofDay> Listofweek = new ObservableCollection<WeekofDay>();

        ObservableCollection<AllAppointments> appointments = new ObservableCollection<AllAppointments>();

        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsMON = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsTUE = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsWED = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsTHU = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsFRI = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsSAT = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<CalenderAppointmentDetail> ListofAppointmentsSUN = new ObservableCollection<CalenderAppointmentDetail>();
        ObservableCollection<Employees> objEmp = new ObservableCollection<Employees>();
        string datedayofweek;

        #endregion

        public CalenderPage(string DateDayofWeek)
        {
            try
            {
                datedayofweek = DateDayofWeek;
                InitializeComponent();

                //await Task.Delay(5000)
                if (Application.Current.Properties.ContainsKey("LastSelectedStaff") == true)
                {
                    if (Application.Current.Properties.ContainsKey("SelectedEmpId") == true)
                    {
                        EmployeeId = Convert.ToInt32(Application.Current.Properties["SelectedEmpId"]);
                        empName = Convert.ToString(Application.Current.Properties["LastSelectedStaff"]);
                    }



                    //currentWeek = GetCurrentWeek();
                    //BindingContext = currentWeek;
                    //InitializeComponent();
                    isCalenderPageOpen = true;
                    NavigationPage.SetHasNavigationBar(this, false);


                    if (DateDayofWeek == "")
                    {
                        WeekofDays();
                    }
                    else
                    {
                        var dt = DateDayofWeek.Split(' ');
                        var sdt = dt[1];
                        var date = Convert.ToDateTime(sdt);

                        //var cW = week;

                        //var dwe = cW[6];
                        //DateTime dst = Convert.ToDateTime(dwe);
                        DateTime today = date;
                        int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7);
                        DateTime nextTuesday = today.AddDays(daysUntilTuesday);
                        int currentDayOfWeek = (int)nextTuesday.DayOfWeek;
                        DateTime sunday = nextTuesday.AddDays(-currentDayOfWeek);
                        DateTime monday = sunday.AddDays(1);
                        // If we started on Sunday, we should actually have gone *back*
                        // 6 days instead of forward 1...
                        if (currentDayOfWeek == 0)
                        {
                            monday = monday.AddDays(-7);
                        }
                        var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                        week.Clear();
                        foreach (var item in dates)
                        {
                            //var ddde = item.Date;
                            //var d = Convert.ToString(ddde);
                            //week.Add(d);

                            var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                            week.Add(dd);
                        }
                    }



                    //WeekMonlbl.Text = currentWeek[0];
                    //WeekTuelbl.Text = currentWeek[1];
                    //WeekWedlbl.Text = currentWeek[2];
                    //WeekThulbl.Text = currentWeek[3];
                    //WeekFrilbl.Text = currentWeek[4];
                    //WeekSatlbl.Text = currentWeek[5];
                    //WeekSunlbl.Text = currentWeek[6];

                    WeekMonlbl.Text = Convert.ToDateTime(week[0]).ToString("dd");
                    WeekTuelbl.Text = Convert.ToDateTime(week[1]).ToString("dd");
                    WeekWedlbl.Text = Convert.ToDateTime(week[2]).ToString("dd");
                    WeekThulbl.Text = Convert.ToDateTime(week[3]).ToString("dd");
                    WeekFrilbl.Text = Convert.ToDateTime(week[4]).ToString("dd");
                    WeekSatlbl.Text = Convert.ToDateTime(week[5]).ToString("dd");
                    WeekSunlbl.Text = Convert.ToDateTime(week[6]).ToString("dd");


                    //MondaylblM.Text = week[0];

                    Mondaylbl.Text = "MON" + " " + Convert.ToDateTime(week[0]).ToString("dd-MMM");
                    Tuesdaylbl.Text = "TUE" + " " + Convert.ToDateTime(week[1]).ToString("dd-MMM");
                    Wednesdaylbl.Text = "WED" + " " + Convert.ToDateTime(week[2]).ToString("dd-MMM");
                    Thursdaylbl.Text = "THU" + " " + Convert.ToDateTime(week[3]).ToString("dd-MMM");
                    Fridaylbl.Text = "FRI" + " " + Convert.ToDateTime(week[4]).ToString("dd-MMM");
                    Saturdaylbl.Text = "SAT" + " " + Convert.ToDateTime(week[5]).ToString("dd-MMM");
                    Sundaylbl.Text = "SUN" + " " + Convert.ToDateTime(week[6]).ToString("dd-MMM");

                    GetAppointmentBookingByEmployeeID();
                    var MON = Convert.ToDateTime(week[0]).ToString("dd-MMM-yyyy");
                    var TUE = Convert.ToDateTime(week[1]).ToString("dd-MMM-yyyy");
                    var WED = Convert.ToDateTime(week[2]).ToString("dd-MMM-yyyy");
                    var THU = Convert.ToDateTime(week[3]).ToString("dd-MMM-yyyy");
                    var FRI = Convert.ToDateTime(week[4]).ToString("dd-MMM-yyyy");
                    var SAT = Convert.ToDateTime(week[5]).ToString("dd-MMM-yyyy");
                    var SUN = Convert.ToDateTime(week[6]).ToString("dd-MMM-yyyy");
                    currentMonth.Text = Convert.ToDateTime(week[0]).ToString("MMM yyyy");

                    listViewMON.IsVisible = false;
                    listViewTUE.IsVisible = false;
                    listViewWED.IsVisible = false;
                    listViewTHU.IsVisible = false;
                    listViewFRI.IsVisible = false;
                    listViewSAT.IsVisible = false;
                    listViewSUN.IsVisible = false;


                    listEmptyMON.IsVisible = true;
                    listEmptyTUE.IsVisible = true;
                    listEmptyWED.IsVisible = true;
                    listEmptyTHU.IsVisible = true;
                    listEmptyFRI.IsVisible = true;
                    listEmptySAT.IsVisible = true;
                    listEmptySUN.IsVisible = true;

                    foreach (var item in ListofAppointments)
                    {
                        //listEmptyMON.IsVisible = true;
                        var d = item.BookingDate.Split(',');
                        var date = Convert.ToDateTime(d[0]).ToString("dd-MMM-yyyy");

                        if (MON == date)
                        {
                            listViewMON.IsVisible = true;
                            listEmptyMON.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsMON.Add(obj);
                            CustomerAppoimentListMON.ItemsSource = ListofAppointmentsMON;
                            //CustomerAppoimentListMONDay.ItemsSource = ListofAppointmentsMON;

                        }
                        else if (TUE == date)
                        {
                            listViewTUE.IsVisible = true;
                            listEmptyTUE.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsTUE.Add(obj);
                            CustomerAppoimentListTUE.ItemsSource = ListofAppointmentsTUE;

                        }
                        else if (WED == date)
                        {
                            listViewWED.IsVisible = true;
                            listEmptyWED.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsWED.Add(obj);
                            CustomerAppoimentListWED.ItemsSource = ListofAppointmentsWED;

                        }
                        else if (THU == date)
                        {

                            listViewTHU.IsVisible = true;
                            listEmptyTHU.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsTHU.Add(obj);
                            CustomerAppoimentListTHU.ItemsSource = ListofAppointmentsTHU;

                        }
                        else if (FRI == date)
                        {
                            listViewFRI.IsVisible = true;
                            listEmptyFRI.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsFRI.Add(obj);
                            CustomerAppoimentListFRI.ItemsSource = ListofAppointmentsFRI;

                        }
                        else if (SAT == date)
                        {
                            listViewSAT.IsVisible = true;
                            listEmptySAT.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsSAT.Add(obj);
                            CustomerAppoimentListSAT.ItemsSource = ListofAppointmentsSAT;

                        }
                        else if (SUN == date)
                        {
                            listViewSUN.IsVisible = true;
                            listEmptySUN.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsSUN.Add(obj);
                            CustomerAppoimentListSUN.ItemsSource = ListofAppointmentsSUN;

                        }

                    }

                    //calender.SelectedDate = DateTime.Now;
                    calender.DateClicked += Calendar_DateClicked;
                    calender.RightArrowClicked += Calendar_RightArrowClicked;
                    calender.LeftArrowClicked += Calendar_LeftArrowClicked;
                  

                    schedulee = new SfSchedule();
                    ViewHeaderStyle viewHeaderStyle = new ViewHeaderStyle();
                    viewHeaderStyle.DayTextColor = Color.Black;
                    viewHeaderStyle.DayTextStyle = Font.OfSize("Arial", 15);
                    schedulee.ViewHeaderStyle = viewHeaderStyle;
                    DependencyService.Get<IProgressInterface>().Hide();

                }
                else
                {


                    //DependencyService.Get<IProgressInterface>().Show();


                    //currentWeek = GetCurrentWeek();
                    //BindingContext = currentWeek;
                    isCalenderPageOpen = true;
                    NavigationPage.SetHasNavigationBar(this, false);
                    //InitializeComponent();


                    if (datedayofweek == "")
                    {
                        WeekofDays();
                    }
                    else
                    {
                        var dt = datedayofweek.Split(' ');
                        var sdt = dt[1];
                        var date = Convert.ToDateTime(sdt);

                        //var cW = week;

                        //var dwe = cW[6];
                        //DateTime dst = Convert.ToDateTime(dwe);
                        DateTime today = date;
                        int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7);
                        DateTime nextTuesday = today.AddDays(daysUntilTuesday);
                        int currentDayOfWeek = (int)nextTuesday.DayOfWeek;
                        DateTime sunday = nextTuesday.AddDays(-currentDayOfWeek);
                        DateTime monday = sunday.AddDays(1);
                        // If we started on Sunday, we should actually have gone *back*
                        // 6 days instead of forward 1...
                        if (currentDayOfWeek == 0)
                        {
                            monday = monday.AddDays(-7);
                        }
                        var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                        week.Clear();
                        foreach (var item in dates)
                        {
                            //var ddde = item.Date;
                            //var d = Convert.ToString(ddde);
                            //week.Add(d);

                            var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                            week.Add(dd);
                        }





                        //    System.DateTime today = System.DateTime.Today;
                        //    int currentDayOfWeek = (int)today.DayOfWeek;
                        //    System.DateTime sunday = today.AddDays(-currentDayOfWeek);
                        //    System.DateTime monday = sunday.AddDays(1);
                        //    if (currentDayOfWeek == 0)
                        //    {
                        //        monday = monday.AddDays(-7);
                        //    }
                        //    var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();

                        //    foreach (var item in dates)
                        //    {

                        //        var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                        //        week.Add(dd);
                        //    }
                    }





                    //WeekMonlbl.Text = currentWeek[0];
                    //WeekTuelbl.Text = currentWeek[1];
                    //WeekWedlbl.Text = currentWeek[2];
                    //WeekThulbl.Text = currentWeek[3];
                    //WeekFrilbl.Text = currentWeek[4];
                    //WeekSatlbl.Text = currentWeek[5];
                    //WeekSunlbl.Text = currentWeek[6];

                    WeekMonlbl.Text = Convert.ToDateTime(week[0]).ToString("dd");
                    WeekTuelbl.Text = Convert.ToDateTime(week[1]).ToString("dd");
                    WeekWedlbl.Text = Convert.ToDateTime(week[2]).ToString("dd");
                    WeekThulbl.Text = Convert.ToDateTime(week[3]).ToString("dd");
                    WeekFrilbl.Text = Convert.ToDateTime(week[4]).ToString("dd");
                    WeekSatlbl.Text = Convert.ToDateTime(week[5]).ToString("dd");
                    WeekSunlbl.Text = Convert.ToDateTime(week[6]).ToString("dd");


                    //MondaylblM.Text = week[0];

                    Mondaylbl.Text = "MON" + " " + Convert.ToDateTime(week[0]).ToString("dd-MMM");
                    Tuesdaylbl.Text = "TUE" + " " + Convert.ToDateTime(week[1]).ToString("dd-MMM");
                    Wednesdaylbl.Text = "WED" + " " + Convert.ToDateTime(week[2]).ToString("dd-MMM");
                    Thursdaylbl.Text = "THU" + " " + Convert.ToDateTime(week[3]).ToString("dd-MMM");
                    Fridaylbl.Text = "FRI" + " " + Convert.ToDateTime(week[4]).ToString("dd-MMM");
                    Saturdaylbl.Text = "SAT" + " " + Convert.ToDateTime(week[5]).ToString("dd-MMM");
                    Sundaylbl.Text = "SUN" + " " + Convert.ToDateTime(week[6]).ToString("dd-MMM");

                    GetAppointmentBookingByEmployeeIDs();
                    var MON = Convert.ToDateTime(week[0]).ToString("dd-MMM-yyyy");
                    var TUE = Convert.ToDateTime(week[1]).ToString("dd-MMM-yyyy");
                    var WED = Convert.ToDateTime(week[2]).ToString("dd-MMM-yyyy");
                    var THU = Convert.ToDateTime(week[3]).ToString("dd-MMM-yyyy");
                    var FRI = Convert.ToDateTime(week[4]).ToString("dd-MMM-yyyy");
                    var SAT = Convert.ToDateTime(week[5]).ToString("dd-MMM-yyyy");
                    var SUN = Convert.ToDateTime(week[6]).ToString("dd-MMM-yyyy");
                    currentMonth.Text = Convert.ToDateTime(week[0]).ToString("MMM yyyy");

                    listViewMON.IsVisible = false;
                    listViewTUE.IsVisible = false;
                    listViewWED.IsVisible = false;
                    listViewTHU.IsVisible = false;
                    listViewFRI.IsVisible = false;
                    listViewSAT.IsVisible = false;
                    listViewSUN.IsVisible = false;


                    listEmptyMON.IsVisible = true;
                    listEmptyTUE.IsVisible = true;
                    listEmptyWED.IsVisible = true;
                    listEmptyTHU.IsVisible = true;
                    listEmptyFRI.IsVisible = true;
                    listEmptySAT.IsVisible = true;
                    listEmptySUN.IsVisible = true;

                    foreach (var item in ListofAppointments)
                    {
                        //listEmptyMON.IsVisible = true;
                        var d = item.BookingDate.Split(',');
                        var date = Convert.ToDateTime(d[0]).ToString("dd-MMM-yyyy");

                        if (MON == date)
                        {
                            listViewMON.IsVisible = true;
                            listEmptyMON.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsMON.Add(obj);
                            CustomerAppoimentListMON.ItemsSource = ListofAppointmentsMON;
                            //CustomerAppoimentListMONDay.ItemsSource = ListofAppointmentsMON;

                        }
                        else if (TUE == date)
                        {
                            listViewTUE.IsVisible = true;
                            listEmptyTUE.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsTUE.Add(obj);
                            CustomerAppoimentListTUE.ItemsSource = ListofAppointmentsTUE;

                        }
                        else if (WED == date)
                        {
                            listViewWED.IsVisible = true;
                            listEmptyWED.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsWED.Add(obj);
                            CustomerAppoimentListWED.ItemsSource = ListofAppointmentsWED;

                        }

                        else if (THU == date)
                        {

                            listViewTHU.IsVisible = true;
                            listEmptyTHU.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsTHU.Add(obj);
                            CustomerAppoimentListTHU.ItemsSource = ListofAppointmentsTHU;

                        }
                        else if (FRI == date)
                        {
                            listViewFRI.IsVisible = true;
                            listEmptyFRI.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsFRI.Add(obj);
                            CustomerAppoimentListFRI.ItemsSource = ListofAppointmentsFRI;

                        }
                        else if (SAT == date)
                        {
                            listViewSAT.IsVisible = true;
                            listEmptySAT.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsSAT.Add(obj);
                            CustomerAppoimentListSAT.ItemsSource = ListofAppointmentsSAT;

                        }
                        else if (SUN == date)
                        {
                            listViewSUN.IsVisible = true;
                            listEmptySUN.IsVisible = false;
                            obj = new CalenderAppointmentDetail();
                            obj.StartTime = item.StartTime;
                            obj.BookingDate = item.BookingDate;
                            obj.CustomerName = item.CustomerName;
                            obj.DurationHrsMin = item.DurationHrsMin;
                            obj.AppointmentDetail = item.AppointmentDetail;
                            ListofAppointmentsSUN.Add(obj);
                            CustomerAppoimentListSUN.ItemsSource = ListofAppointmentsSUN;

                        }

                    }

                    schedulee = new SfSchedule();
                    ViewHeaderStyle viewHeaderStyle = new ViewHeaderStyle();
                    viewHeaderStyle.DayTextColor = Color.Black;
                    viewHeaderStyle.DayTextStyle = Font.OfSize("Arial", 15);
                    schedulee.ViewHeaderStyle = viewHeaderStyle;                                       
                    calender.DateClicked += Calendar_DateClicked;
                    calender.RightArrowClicked += Calendar_RightArrowClicked;
                    calender.LeftArrowClicked += Calendar_LeftArrowClicked;
                    //DateSelectedcommand = DateTime.Now;

                    DependencyService.Get<IProgressInterface>().Hide();

                }

            }
            catch (Exception e)
            {
                e.ToString();
            }

        }
        public DateTime DateSelectedcommand { get; set; }
        private void Calendar_RightArrowClicked(object sender,DateTimeEventArgs e)
        {
            var d = e.DateTime.ToString("MMM yyyy");
            currentMonth.Text = d;

        }
        private void Calendar_LeftArrowClicked(object sender, DateTimeEventArgs e)
        {
            var d = e.DateTime.ToString("MMM yyyy");
            currentMonth.Text = d;
        }
        private void Calendar_DateClicked(object sender, DateTimeEventArgs e)
        {
            try
            {                
                System.DateTime today = e.DateTime;
                int currentDayOfWeek = (int)today.DayOfWeek;
                System.DateTime sunday = today.AddDays(-currentDayOfWeek);
                System.DateTime monday = sunday.AddDays(1);
                if (currentDayOfWeek == 0)
                {
                    monday = monday.AddDays(-7);
                }
                var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                week.Clear();
                foreach (var item in dates)
                {

                    var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                    week.Add(dd);
                }
                WeekMonlbl.Text = Convert.ToDateTime(week[0]).ToString("dd");
                WeekTuelbl.Text = Convert.ToDateTime(week[1]).ToString("dd");
                WeekWedlbl.Text = Convert.ToDateTime(week[2]).ToString("dd");
                WeekThulbl.Text = Convert.ToDateTime(week[3]).ToString("dd");
                WeekFrilbl.Text = Convert.ToDateTime(week[4]).ToString("dd");
                WeekSatlbl.Text = Convert.ToDateTime(week[5]).ToString("dd");
                WeekSunlbl.Text = Convert.ToDateTime(week[6]).ToString("dd");

                Mondaylbl.Text = "MON" + " " + Convert.ToDateTime(week[0]).ToString("dd-MMM");
                Tuesdaylbl.Text = "TUE" + " " + Convert.ToDateTime(week[1]).ToString("dd-MMM");
                Wednesdaylbl.Text = "WED" + " " + Convert.ToDateTime(week[2]).ToString("dd-MMM");
                Thursdaylbl.Text = "THU" + " " + Convert.ToDateTime(week[3]).ToString("dd-MMM");
                Fridaylbl.Text = "FRI" + " " + Convert.ToDateTime(week[4]).ToString("dd-MMM");
                Saturdaylbl.Text = "SAT" + " " + Convert.ToDateTime(week[5]).ToString("dd-MMM");
                Sundaylbl.Text = "SUN" + " " + Convert.ToDateTime(week[6]).ToString("dd-MMM");

                string weekDateDay = week[0];

                Application.Current.MainPage.Navigation.PushAsync(new SetAppointmentPage(selectesPageN, empName, weekDateDay));
                dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                schedulerFullMonthView.IsVisible = false;
                schedulerWeekView.IsVisible = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public void WeekofDays()
        {
            System.DateTime today = System.DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            System.DateTime sunday = today.AddDays(-currentDayOfWeek);
            System.DateTime monday = sunday.AddDays(1);
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();

            foreach (var item in dates)
            {

                var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                week.Add(dd);
            }
        }

        public void LeftNavigateArrow_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                var cW = week;

                var dwe = cW[0];
                DateTime dst = Convert.ToDateTime(dwe);
                DateTime today = dst;
                int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7) - 1;
                DateTime nextTuesday = today.AddDays(daysUntilTuesday);
                int currentDayOfWeek = (int)nextTuesday.DayOfWeek;
                DateTime sunday = nextTuesday.AddDays(-currentDayOfWeek);
                DateTime monday = sunday.AddDays(1);
                // If we started on Sunday, we should actually have gone *back*
                // 6 days instead of forward 1...
                if (currentDayOfWeek == 0)
                {
                    monday = monday.AddDays(-7);
                }
                var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                week.Clear();
                foreach (var item in dates)
                {
                    //var ddde = item.Date;
                    //var d = Convert.ToString(ddde);
                    //week.Add(d);

                    var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                    week.Add(dd);
                }

                WeekMonlbl.Text = Convert.ToDateTime(week[0]).ToString("dd");
                WeekTuelbl.Text = Convert.ToDateTime(week[1]).ToString("dd");
                WeekWedlbl.Text = Convert.ToDateTime(week[2]).ToString("dd");
                WeekThulbl.Text = Convert.ToDateTime(week[3]).ToString("dd");
                WeekFrilbl.Text = Convert.ToDateTime(week[4]).ToString("dd");
                WeekSatlbl.Text = Convert.ToDateTime(week[5]).ToString("dd");
                WeekSunlbl.Text = Convert.ToDateTime(week[6]).ToString("dd");

                Mondaylbl.Text = "MON" + " " + Convert.ToDateTime(week[0]).ToString("dd-MMM");
                Tuesdaylbl.Text = "TUE" + " " + Convert.ToDateTime(week[1]).ToString("dd-MMM");
                Wednesdaylbl.Text = "WED" + " " + Convert.ToDateTime(week[2]).ToString("dd-MMM");
                Thursdaylbl.Text = "THU" + " " + Convert.ToDateTime(week[3]).ToString("dd-MMM");
                Fridaylbl.Text = "FRI" + " " + Convert.ToDateTime(week[4]).ToString("dd-MMM");
                Saturdaylbl.Text = "SAT" + " " + Convert.ToDateTime(week[5]).ToString("dd-MMM");
                Sundaylbl.Text = "SUN" + " " + Convert.ToDateTime(week[6]).ToString("dd-MMM");

                string weekDateDay = week[0];

                Application.Current.MainPage.Navigation.PushAsync(new SetAppointmentPage(selectesPageN, empName, weekDateDay));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        public async void RightNavigateArrow_ClickEvent(object sender, EventArgs e)
        {
            try
            {

                var cW = week;

                var dwe = cW[6];
                DateTime dst = Convert.ToDateTime(dwe);
                DateTime today = dst;
                int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7) + 1;
                DateTime nextTuesday = today.AddDays(daysUntilTuesday);
                int currentDayOfWeek = (int)nextTuesday.DayOfWeek;
                DateTime sunday = nextTuesday.AddDays(-currentDayOfWeek);
                DateTime monday = sunday.AddDays(1);
                // If we started on Sunday, we should actually have gone *back*
                // 6 days instead of forward 1...
                if (currentDayOfWeek == 0)
                {
                    monday = monday.AddDays(-7);
                }
                var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                week.Clear();
                foreach (var item in dates)
                {
                    //var ddde = item.Date;
                    //var d = Convert.ToString(ddde);
                    //week.Add(d);

                    var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM-yyyy");
                    week.Add(dd);
                }

                WeekMonlbl.Text = Convert.ToDateTime(week[0]).ToString("dd");
                WeekTuelbl.Text = Convert.ToDateTime(week[1]).ToString("dd");
                WeekWedlbl.Text = Convert.ToDateTime(week[2]).ToString("dd");
                WeekThulbl.Text = Convert.ToDateTime(week[3]).ToString("dd");
                WeekFrilbl.Text = Convert.ToDateTime(week[4]).ToString("dd");
                WeekSatlbl.Text = Convert.ToDateTime(week[5]).ToString("dd");
                WeekSunlbl.Text = Convert.ToDateTime(week[6]).ToString("dd");

                Mondaylbl.Text = "MON" + " " + Convert.ToDateTime(week[0]).ToString("dd-MMM");
                Tuesdaylbl.Text = "TUE" + " " + Convert.ToDateTime(week[1]).ToString("dd-MMM");
                Wednesdaylbl.Text = "WED" + " " + Convert.ToDateTime(week[2]).ToString("dd-MMM");
                Thursdaylbl.Text = "THU" + " " + Convert.ToDateTime(week[3]).ToString("dd-MMM");
                Fridaylbl.Text = "FRI" + " " + Convert.ToDateTime(week[4]).ToString("dd-MMM");
                Saturdaylbl.Text = "SAT" + " " + Convert.ToDateTime(week[5]).ToString("dd-MMM");
                Sundaylbl.Text = "SUN" + " " + Convert.ToDateTime(week[6]).ToString("dd-MMM");

                string weekDateDay = week[0];

                await Application.Current.MainPage.Navigation.PushAsync(new SetAppointmentPage(selectesPageN, empName, weekDateDay));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public static SfSchedule getScheduleObj()
        {
            return schedulee;
        }
        private void ChangeMonthView(object sender, EventArgs e)
        {
            try
            {                
                IsMonthView = !IsMonthView;
                if (IsMonthView)
                {
                    //calender.DateClicked += Calendar_DateClicked;
                    XamForms.Controls.Calendar c = (XamForms.Controls.Calendar)calender;
                    var dd= c.TitleLabelText;
                    currentMonth.Text = dd;

                    dropdownArrow.RotateTo(180, 200, Easing.SinInOut);
                    schedulerWeekView.IsVisible = false;
                    schedulerFullMonthView.IsVisible = true;                    
                    MonthViewSettings monthViewSettings = new MonthViewSettings();
                    monthViewSettings.MonthLabelSettings.DayFormat = "E";
                }
                else
                {
                    dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                    schedulerFullMonthView.IsVisible = false;
                    schedulerWeekView.IsVisible = true;
                    currentMonth.Text = Convert.ToDateTime(week[0]).ToString("MMM yyyy");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //public string[] GetCurrentWeek()
        //{
        //    try
        //    {
        //        //CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
        //        CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        //        CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();

        //        _culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
        //        _uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

        //        System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = _uiculture;



        //        DateTime startOfWeek = DateTime.Today.AddDays(
        //  (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
        //  (int)DateTime.Today.DayOfWeek);

        //        string result = string.Join(",", Enumerable
        //          .Range(0, 7)
        //          .Select(i => startOfWeek
        //             .AddDays(i)
        //             .ToString("dd")));

        //        return result.Split(',');
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return null;
        //    }
        //}

        private void AddNewAppointment(object sender, EventArgs e)
        {
            try
            {

                Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
                string ssss = string.Empty;
                var s = grid.Children[0];
                Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
                var selectedD = label.Text;
                var DateofBooking = Convert.ToDateTime(selectedD).ToString();

                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceCategoriesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);
                ObservableCollection<Category> ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(result);
                var c = ListofAllCategories.ToList();
                //if (c.Count == 0)
                //{
                //    Navigation.PushAsync(new SelectServicesForAppontment("NewCalAppointment", CategoryId, CategoryName, DateofBooking, statusId));
                //}
                if (Application.Current.Properties.ContainsKey("LastSelectedStaff") == true)
                {
                    Application.Current.MainPage.Navigation.PushAsync(new GetAllocateServiceForEmployeePage(EmployeeId, empName, DateofBooking));
                }
                else
                {
                    if (c.Count == 0)
                    {
                        Application.Current.MainPage.Navigation.PushAsync(new SelectServicesForAppontment("NewCalAppointment", CategoryId, CategoryName, DateofBooking, statusId));
                    }
                    else
                    {
                        Application.Current.MainPage.Navigation.PushAsync(new SelectServiceCategory("NewCalAppointment", DateofBooking, statusId));
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            // MonAppointmentBox.Children.Add(firstLabel);
        }
        public void GetSelectedCustomerById()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/staff/GetAllEmployees?companyId=" + CompanyId;
                var result = PostData("GET", "", apiURL);
                //objEmp = JsonConvert.DeserializeObject<Employees>(result);

                ObservableCollection<Employees> objEmp = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(result);


                //objEmp = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(result);

            }
            catch (Exception e)
            {
                e.ToString();
            }

        }
        public ObservableCollection<CalenderAppointmentDetail> GetAppointmentBookingByEmployeeIDs()
        {
            try
            {
                string apiURLs = Application.Current.Properties["DomainUrl"] + "/api/staff/GetAllEmployees?companyId=" + CompanyId;
                var results = PostData("GET", "", apiURLs);
                //objEmp = JsonConvert.DeserializeObject<Employees>(result);

                ObservableCollection<Employees> objEmp = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(results);

                ListofAppointments = new ObservableCollection<CalenderAppointmentDetail>();
                foreach (var item in objEmp)
                {
                    string[] StartTime = { };
                    var startDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(-1)).ToString("dd-MM-yyyy");
                    var endDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(1)).ToString("dd-MM-yyyy");
                    string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetBookingsForEmployeesByIdBetweenDates?companyId=" + item.CompanyId + "&commaSeperatedEmployeeIds=" + item.Id + "&startDate=" + startDate + "&endDate=" + endDate;

                    var result = PostData("GET", "", apiURL);

                    ObservableCollection<AllAppointments> appointments = JsonConvert.DeserializeObject<ObservableCollection<AllAppointments>>(result);
                    foreach (var appointment in appointments)
                    {

                        var datebooking = appointment.Start;
                        var DateOFbooking = Convert.ToDateTime(datebooking).ToString("dd-MMM-yyyy");
                        System.DateTime startTime = Convert.ToDateTime(appointment.Start);
                        string Time = startTime.ToString("hh:mm tt");
                        Time = Time.ToUpper();
                        var DateTimeofBooking = DateOFbooking + "," + Time;
                        obj = new CalenderAppointmentDetail();
                        obj.StartTime = Time;
                        obj.BookingDate = DateTimeofBooking;
                        string DurationHours = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes / 60);
                        string durmin = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes % 60); ;
                        string durhrs = DurationHours + "hrs";
                        string durationMins = durmin + "mins";
                        string Duration = durhrs + " " + durationMins;
                        obj.DurationHrsMin = Duration;
                        string detail = appointment.Service.Name + "," + " w/ " + appointment.Employee.FirstName + "," + " $" + appointment.Service.Cost;
                        string details = detail;

                        if (details.Length > 18)
                        {
                            details = string.Concat(details.Substring(0, 18), "...");
                        }
                        obj.AppointmentDetail = details;
                        obj.CustomerName = appointment.Customers.Select(x => x.FirstName).FirstOrDefault();
                        //obj.CommentNotes = "abc";
                        //obj.TimePeriod = Duration;
                        ListofAppointments.Add(obj);
                    }
                }
                return ListofAppointments;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }

            //CustomerAppoimentList.ItemsSource = ListofAppointment;

        }
        public ObservableCollection<CalenderAppointmentDetail> GetAppointmentBookingByEmployeeID()
        {
            try
            {
                string[] StartTime = { };
                var startDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(-1)).ToString("dd-MM-yyyy");
                var endDate = Convert.ToDateTime(DateTime.Now.Date.AddYears(1)).ToString("dd-MM-yyyy");
                string apiURL = Application.Current.Properties["DomainUrl"] + "api/booking/GetBookingsForEmployeesByIdBetweenDates?companyId=" + CompanyId + "&commaSeperatedEmployeeIds=" + EmployeeId + "&startDate=" + startDate + "&endDate=" + endDate;

                var result = PostData("GET", "", apiURL);

                ObservableCollection<AllAppointments> appointments = JsonConvert.DeserializeObject<ObservableCollection<AllAppointments>>(result);
                ListofAppointments = new ObservableCollection<CalenderAppointmentDetail>();

                foreach (var appointment in appointments)
                {
                    var datebooking = appointment.Start;
                    var DateOFbooking = Convert.ToDateTime(datebooking).ToString("dd-MMM-yyyy");
                    System.DateTime startTime = Convert.ToDateTime(appointment.Start);
                    string Time = startTime.ToString("hh:mm tt");
                    Time = Time.ToUpper();
                    var DateTimeofBooking = DateOFbooking + "," + Time;
                    obj = new CalenderAppointmentDetail();
                    obj.StartTime = Time;
                    obj.BookingDate = DateTimeofBooking;
                    string DurationHours = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes / 60);
                    string durmin = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes % 60); ;
                    string durhrs = DurationHours + "hrs";
                    string durationMins = durmin + "mins";
                    string Duration = durhrs + " " + durationMins;
                    obj.DurationHrsMin = Duration;
                    string detail = appointment.Service.Name + "," + " w/ " + appointment.Employee.FirstName + "," + " $" + appointment.Service.Cost;
                    string details = detail;

                    if (details.Length > 18)
                    {
                        details = string.Concat(details.Substring(0, 18), "...");
                    }
                    obj.AppointmentDetail = details;
                    obj.CustomerName = appointment.Customers.Select(x => x.FirstName).FirstOrDefault();
                    //obj.CommentNotes = "abc";
                    //obj.TimePeriod = Duration;
                    ListofAppointments.Add(obj);
                    //string DurationHours = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes / 60);
                    //string durmin = Convert.ToString(appointment.Service == null ? 0 : appointment.Service.DurationInMinutes % 60); ;
                    //string durhrs = DurationHours + "hrs";
                    //string durationMins = durmin + "mins";
                    //string Duration = durhrs + " " + durationMins;
                    //var datebooking = appointment.Start;
                    //var DateOFbooking = Convert.ToDateTime(datebooking).ToString("dd-MMM-yyyy");

                    //string detail = appointment.Service.Name + "," + " w/ " + appointment.Employee.FirstName + "," + " $" + appointment.Service.Cost;

                    //System.DateTime startTime = Convert.ToDateTime(appointment.Start);
                    //string Time = startTime.ToString("hh:mm tt");
                    //Time = Time.ToUpper();
                    //var DateTimeofBooking = DateOFbooking + "," + Time;
                    //obj = new CalenderAppointmentDetail();



                    //obj.DurationHrsMin = Duration;

                    //string details = detail;

                    //if (details.Length > 18)
                    //{
                    //    details = string.Concat(details.Substring(0, 18), "...");
                    //}

                    //obj.AppointmentDetail = details;


                    ////obj.AppointmentDetail = detail;
                    //obj.CustomerName = appointment.Customers.Select(x => x.FirstName).FirstOrDefault();

                    ////obj.TimePeriod = Duration;
                    //ListofAppointments.Add(obj);

                }
                return ListofAppointments;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }

            //CustomerAppoimentList.ItemsSource = ListofAppointment;

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