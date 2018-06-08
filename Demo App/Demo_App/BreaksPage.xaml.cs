using Demo_App.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
    public partial class BreaksPage : ContentPage
    {
        string StaffId;
        string SelectDay;
        int CompanyID;
        List<string> week = new List<string>();

        public StaffBreakTime BHours = null;

        ObservableCollection<StaffBreakTime> BussinessDaysLst = new ObservableCollection<StaffBreakTime>();

        ObservableCollection<StaffBreakTime> ListofsMON = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsTUE = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsWED = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsTHU = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsFRI = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsSAT = new ObservableCollection<StaffBreakTime>();
        ObservableCollection<StaffBreakTime> ListofsSUN = new ObservableCollection<StaffBreakTime>();

        ObservableCollection<StaffBreakTime> ListofBreaks = new ObservableCollection<StaffBreakTime>();

        ObservableCollection<ProviderWorkingHours> ListofBreaksHours = new ObservableCollection<ProviderWorkingHours>();

        public StaffBreakTime obj = null;
        public ProviderWorkingHours objStaffHours = null;
        string oldStartTime;
        string oldEndTime;

        public BreaksPage(SaveStaffBreakTime ob, int EmployeeId, string day, string compId)
        {
            InitializeComponent();
            SelectDay = day;
            oldStartTime = ob.Start;
            oldEndTime = ob.End;
            CompanyID = Convert.ToInt32(compId);
            StaffId = (EmployeeId).ToString();
            GetBreakTimeofEmployee();
            weeks();
            var mon = week[0];
            var tue = week[1];
            var wed = week[2];
            var thu = week[3];
            var fri = week[4];
            var sat = week[5];
            var sun = week[6];
            DateTime monds = Convert.ToDateTime(mon);
            var monday = monds.DayOfWeek;
            DateTime tues = Convert.ToDateTime(tue);
            var tuesday = tues.DayOfWeek;
            DateTime weds = Convert.ToDateTime(wed);
            var wednesday = weds.DayOfWeek;
            DateTime thurs = Convert.ToDateTime(thu);
            var thursday = thurs.DayOfWeek;
            DateTime frids = Convert.ToDateTime(fri);
            var friday = frids.DayOfWeek;
            DateTime sats = Convert.ToDateTime(sat);
            var saturday = sats.DayOfWeek;
            DateTime sunds = Convert.ToDateTime(sun);
            var sunday = sunds.DayOfWeek;
            MondayLbl.Text = monday.ToString();
            TuesdayLbl.Text = tuesday.ToString();
            WednesdayLbl.Text = wednesday.ToString();
            ThursdayLbl.Text = thursday.ToString();
            FridayLbl.Text = friday.ToString();
            SaturdayLbl.Text = saturday.ToString();
            SundayLbl.Text = sunday.ToString();

            var mn = Convert.ToDateTime(week[0]).ToString("dd-MMM-yyyy");
            var te = Convert.ToDateTime(week[1]).ToString("dd-MMM-yyyy");
            var we = Convert.ToDateTime(week[2]).ToString("dd-MMM-yyyy");
            var th = Convert.ToDateTime(week[3]).ToString("dd-MMM-yyyy");
            var fr = Convert.ToDateTime(week[4]).ToString("dd-MMM-yyyy");
            var st = Convert.ToDateTime(week[5]).ToString("dd-MMM-yyyy");
            var su = Convert.ToDateTime(week[6]).ToString("dd-MMM-yyyy");

            listViewMON.IsVisible = false;
            listViewTUE.IsVisible = false;
            listViewWED.IsVisible = false;
            listViewTHU.IsVisible = false;
            listViewFRI.IsVisible = false;
            listViewSAT.IsVisible = false;
            listViewSUN.IsVisible = false;

            MonLightGray.IsVisible = false;
            TUELightGray.IsVisible = false;
            WEDLightGray.IsVisible = false;
            THULightGray.IsVisible = false;
            FRILightGray.IsVisible = false;
            SATLightGray.IsVisible = false;
            SUNLightGray.IsVisible = false;

            GetBreakTimeofHours();

            foreach (var item in ListofBreaksHours)
            {
                switch (item.NameOfDayAsString)
                {
                    case "Monday":
                        if (item.IsOffAllDay == true)
                        {
                            MondayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eMONStacLayout.IsEnabled = false;
                            enableMondayLbl.IsVisible = false;
                        }
                        else
                        {
                            MondayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableMondayLbl.IsVisible = true;
                            MondayLbl.IsEnabled = false;
                            eMONStacLayout.IsEnabled = true;
                        }
                        break;
                    case "Tuesday":
                        if (item.IsOffAllDay == true)
                        {
                            TuesdayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eTUEStacLayout.IsEnabled = false;
                            enableTuesdayLbl.IsVisible = false;
                        }
                        else
                        {
                            TuesdayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableTuesdayLbl.IsVisible = true;
                            TuesdayLbl.IsEnabled = false;
                            eTUEStacLayout.IsEnabled = true;
                        }
                        break;
                    case "Wednesday":
                        if (item.IsOffAllDay == true)
                        {
                            WednesdayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eWEDStacLayout.IsEnabled = false;
                            enableWednesdayLbl.IsVisible = false;
                        }
                        else
                        {
                            WednesdayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableWednesdayLbl.IsVisible = true;
                            WednesdayLbl.IsEnabled = false;
                            eWEDStacLayout.IsEnabled = true;
                        }
                        break;
                    case "Thursday":
                        if (item.IsOffAllDay == true)
                        {
                            ThursdayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eTHUStacLayout.IsEnabled = false;
                            enableThursdayLbl.IsVisible = false;
                        }
                        else
                        {
                            ThursdayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableThursdayLbl.IsVisible = true;
                            ThursdayLbl.IsEnabled = false;
                            eTHUStacLayout.IsEnabled = true;
                        }
                        break;

                    case "Friday":
                        if (item.IsOffAllDay == true)
                        {
                            FridayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eFRIStacLayout.IsEnabled = false;
                            enableFridayLbl.IsVisible = false;
                        }
                        else
                        {
                            FridayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableFridayLbl.IsVisible = true;
                            FridayLbl.IsEnabled = false;
                            eFRIStacLayout.IsEnabled = true;
                        }
                        break;
                    case "Saturday":
                        if (item.IsOffAllDay == true)
                        {
                            SaturdayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eSATStacLayout.IsEnabled = false;
                            enableSaturdayLbl.IsVisible = false;
                        }
                        else
                        {
                            SaturdayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableSaturdayLbl.IsVisible = true;
                            SaturdayLbl.IsEnabled = false;
                            eSATStacLayout.IsEnabled = true;
                        }
                        break;
                    case "Sunday":
                        if (item.IsOffAllDay == true)
                        {
                            SundayLbl.TextColor = Xamarin.Forms.Color.Gray;
                            eSUNStacLayout.IsEnabled = false;
                            enableSundayLbl.IsVisible = false;
                        }
                        else
                        {
                            SundayLbl.TextColor = Xamarin.Forms.Color.Black;
                            enableSundayLbl.IsVisible = true;
                            SundayLbl.IsEnabled = false;
                            eSUNStacLayout.IsEnabled = true;
                        }
                        break;
                }
            }


            foreach (var item in ListofBreaks)
            {
                if (item.NameOfDay == "1")
                {
                    var mi = Convert.ToDateTime(week[0]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (mn == date)
                    {
                        listViewMON.IsVisible = true;
                        MONWhite.IsVisible = false;
                        MonLightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsMON.Add(obj);
                        BreakStaffListMON.ItemsSource = ListofsMON;
                        
                    }
                }
                else if (item.NameOfDay == "2")
                {
                    var mi = Convert.ToDateTime(week[1]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (te == date)
                    {
                        listViewTUE.IsVisible = true;
                        TUEWhite.IsVisible = false;
                        TUELightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsTUE.Add(obj);
                        BreakStaffListTUE.ItemsSource = ListofsTUE;
                    }
                }
                else if (item.NameOfDay == "3")
                {
                    var mi = Convert.ToDateTime(week[2]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (we == date)
                    {
                        listViewWED.IsVisible = true;
                        WEDWhite.IsVisible = false;
                        WEDLightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsWED.Add(obj);
                        BreakStaffListWED.ItemsSource = ListofsWED;
                    }
                }
                else if (item.NameOfDay == "4")
                {
                    var mi = Convert.ToDateTime(week[3]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (th == date)
                    {
                        listViewTHU.IsVisible = true;
                        THUWhite.IsVisible = false;
                        THULightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsTHU.Add(obj);
                        BreakStaffListTHU.ItemsSource = ListofsTHU;
                    }
                }
                else if (item.NameOfDay == "5")
                {
                    var mi = Convert.ToDateTime(week[4]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (fr == date)
                    {
                        listViewFRI.IsVisible = true;
                        FRIWhite.IsVisible = false;
                        FRILightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsFRI.Add(obj);
                        BreakStaffListFRI.ItemsSource = ListofsFRI;
                    }
                }
                else if (item.NameOfDay == "6")
                {
                    var mi = Convert.ToDateTime(week[5]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (st == date)
                    {
                        listViewSAT.IsVisible = true;
                        SATWhite.IsVisible = false;
                        SATLightGray.IsVisible = true;
                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsSAT.Add(obj);
                        BreakStaffListSAT.ItemsSource = ListofsSAT;
                    }
                }
                else if (item.NameOfDay == "0")
                {
                    var mi = Convert.ToDateTime(week[6]);
                    var d = mi.ToString();
                    var date = Convert.ToDateTime(d).ToString("dd-MMM-yyyy");
                    if (su == date)
                    {
                        listViewSUN.IsVisible = true;
                        SUNWhite.IsVisible = false;
                        SUNLightGray.IsVisible = true;

                        obj = new StaffBreakTime();
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = item.CreationDate;
                        obj.NameOfDay = item.NameOfDay;
                        obj.EmployeeId = item.EmployeeId;
                        obj.Id = item.Id;


                        obj.Start = item.Start;
                        obj.End = item.End;
                        ListofsSUN.Add(obj);
                        BreakStaffListSUN.ItemsSource = ListofsSUN;
                    }
                }
            }


            //CustomerAppoimentListMON.ItemsSource


        }
        public List<string> weeks()
        {
            System.DateTime today = System.DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            foreach (var item in dates)
            {
                var ddd = item.Date;
                var d = Convert.ToString(ddd);
                week.Add(d);
            }

            //foreach (var item in dates)
            //{

            //    var dd = item.DayOfWeek.ToString().ToUpper().Substring(0, 3) + " " + item.Date.ToString("dd-MMM");
            //    week.Add(dd);
            //}
            return week;
        }
        public void SaveStaffBreaks(object sender, EventArgs e)
        {
            var mon = ListofsMON.Select(x => x.NameOfDay).FirstOrDefault();
            var tue = ListofsTUE.Select(x => x.NameOfDay).FirstOrDefault();
            var wed = ListofsWED.Select(x => x.NameOfDay).FirstOrDefault();
            var thu = ListofsTHU.Select(x => x.NameOfDay).FirstOrDefault();
            var fri = ListofsFRI.Select(x => x.NameOfDay).FirstOrDefault();
            var sat = ListofsSAT.Select(x => x.NameOfDay).FirstOrDefault();
            var sun = ListofsSUN.Select(x => x.NameOfDay).FirstOrDefault();


            foreach (var i in ListofBreaks)
            {
                if (i.NameOfDay == mon)
                {
                    foreach (var item in ListofsMON)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == tue)
                {
                    foreach (var item in ListofsTUE)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == wed)
                {
                    foreach (var item in ListofsWED)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == thu)
                {
                    foreach (var item in ListofsTHU)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == fri)
                {
                    foreach (var item in ListofsFRI)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == sat)
                {
                    foreach (var item in ListofsSAT)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
                else if (i.NameOfDay == sun)
                {
                    foreach (var item in ListofsSUN)
                    {
                        string stt = item.Start.ToString();
                        DateTime st = Convert.ToDateTime(stt);
                        string StartTime = st.ToString("HH:00");

                        string ett = item.End.ToString();
                        DateTime et = Convert.ToDateTime(ett);
                        string EndTime = et.ToString("HH:00");

                        BreakTime obj = new BreakTime();
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EmployeeId = item.EmployeeId;
                        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
                        obj.Start = StartTime;
                        obj.End = EndTime;
                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
                        var result = PostData("POST", SerializedObj, apiUrl);
                    }
                }
            }





            //if (mon == "1")
            //{
            //    foreach (var item in ListofsMON)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (tue == "2")
            //{
            //    foreach (var item in ListofsTUE)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (wed == "3")
            //{
            //    foreach (var item in ListofsWED)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (thu == "4")
            //{
            //    foreach (var item in ListofsTHU)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (fri == "5")
            //{
            //    foreach (var item in ListofsFRI)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (sat == "6")
            //{
            //    foreach (var item in ListofsSAT)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}
            //else if (sun == "7")
            //{
            //    foreach (var item in ListofsSUN)
            //    {
            //        string stt = item.Start.ToString();
            //        DateTime st = Convert.ToDateTime(stt);
            //        string StartTime = st.ToString("HH:mm");

            //        string ett = item.End.ToString();
            //        DateTime et = Convert.ToDateTime(ett);
            //        string EndTime = et.ToString("HH:mm");

            //        BreakTime obj = new BreakTime();
            //        obj.Id = item.Id;
            //        obj.CompanyId = item.CompanyId;
            //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EmployeeId = item.EmployeeId;
            //        obj.DayOfWeek = Convert.ToInt32(item.NameOfDay);
            //        obj.Start = StartTime;
            //        obj.End = EndTime;
            //        var SerializedObj = JsonConvert.SerializeObject(obj);
            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            //        var result = PostData("POST", SerializedObj, apiUrl);
            //    }
            //}


            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 5; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            // Navigation.PopAsync(true);

            Navigation.PushAsync(new StaffProfileDetailsPage());


            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 4)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }

        }

        private void BreaksClick(object sender, EventArgs e)
        {
            Xamarin.Forms.Grid grid = (Xamarin.Forms.Grid)sender;
            var s = grid.Children[0];
            Xamarin.Forms.Label label = (Xamarin.Forms.Label)s;
            var selectedD = label.Text;
            var day = selectedD;
            SelectDay = day;
            //if (day == "Monday")
            //{
            //    var mon = Convert.ToDateTime(day).ToString("dd-MMM-yyyy");
            //}

            var sID = Convert.ToInt32(StaffId);
            //Navigation.PushAsync(new AddBreaks(sID, day));
            var page = new AddBreaks(sID, day, CompanyID);
            PopupNavigation.Instance.PushAsync(page);
            //Timepicker.IsOpen = !Timepicker.IsOpen;
            //Navigation.PushAsync(new AddBreaks(StaffId));
        }

        //public void SetBreakTime()
        //{
        //    try
        //    {
        //        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/AddBreak";
        //        var result = PostData("POST", "", apiUrl);
        //    }
        //    catch(Exception e)
        //    {

        //    }
        //}

        public void EditBreakTime()
        {

        }
        public ObservableCollection<ProviderWorkingHours> GetBreakTimeofHours()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + StaffId;
                var result = PostData("GET", "", apiUrl);

                List<ProviderWorkingHours> WorkingHourStatus = JsonConvert.DeserializeObject<List<ProviderWorkingHours>>(result);
                ListofBreaksHours = new ObservableCollection<ProviderWorkingHours>();
                foreach (var item in WorkingHourStatus)
                {
                    objStaffHours = new ProviderWorkingHours();
                    objStaffHours.IsOffAllDay = item.IsOffAllDay;
                    objStaffHours.NameOfDay = item.NameOfDay;
                    objStaffHours.NameOfDayAsString = item.NameOfDayAsString;
                    objStaffHours.Start = item.Start;
                    objStaffHours.Id = item.Id;
                    objStaffHours.EntityStatus = item.EntityStatus;
                    objStaffHours.End = item.End;
                    objStaffHours.EmployeeId = item.EmployeeId;
                    objStaffHours.CreationDate = item.CreationDate;
                    objStaffHours.CompanyId = item.CompanyId;
                    ListofBreaksHours.Add(objStaffHours);
                }
                return ListofBreaksHours;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


        public ObservableCollection<StaffBreakTime> GetBreakTimeofEmployee()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetAllBreaksForEmployee?employeeId=" + StaffId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<BreakTime> BreakTimeofEmployee = JsonConvert.DeserializeObject<ObservableCollection<BreakTime>>(result);
                ListofBreaks = new ObservableCollection<StaffBreakTime>();
                foreach (var item in BreakTimeofEmployee)
                {
                    TimeSpan start;
                    TimeSpan.TryParse(item.Start, out start);
                    TimeSpan end;
                    TimeSpan.TryParse(item.End, out end);

                    obj = new StaffBreakTime();
                    obj.Id = item.Id;
                    obj.CompanyId = item.CompanyId;
                    obj.EmployeeId = item.EmployeeId;
                    obj.Start = start;
                    obj.End = end;
                    obj.CreationDate = item.CreationDate;
                    obj.NameOfDay = Convert.ToString(item.DayOfWeek);
                    ListofBreaks.Add(obj);
                }
                return ListofBreaks;
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