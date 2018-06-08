using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Configuration;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessHoursPage : ContentPage
    {
        #region GlobleFields
        int EmployeeId;
        string PageName;
        int CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);

        public BussinessHours BHours = null;
        public ProviderWorkingHours obj = null;

        //public CompanyBusinessHours CBHours = null;
        public CompanyWorkingHours CHours = null;
        public CReqWorkingHours CRHours = null;

        ObservableCollection<BussinessHours> BussinessDaysLst = new ObservableCollection<BussinessHours>();
        ObservableCollection<ProviderWorkingHours> listofWorkingDays = new ObservableCollection<ProviderWorkingHours>();

        //ObservableCollection<CReqWorkingHours> CompanyBussinessDaysLst = new ObservableCollection<CReqWorkingHours>();
        ObservableCollection<CompanyWorkingHours> CompanyListWorkingDays = new ObservableCollection<CompanyWorkingHours>();


        #endregion

        public BusinessHoursPage(string pagename)
        {
            try
            {
                InitializeComponent();
                PageName = pagename;
                if (PageName == "CompanyHours")
                {
                    DateTime today = DateTime.Today;
                    int currentDayOfWeek = (int)today.DayOfWeek;
                    DateTime sunday = today.AddDays(-currentDayOfWeek);
                    DateTime monday = sunday.AddDays(1);

                    if (currentDayOfWeek == 0)
                    {
                        monday = monday.AddDays(-7);
                    }
                    var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                    //var list = listofWorkingDays.Select(x => x.NameOfDayAsString).ToList();

                    TimeSpan tsStart = new TimeSpan(0, 08, 00, 00);
                    TimeSpan tsEnd = new TimeSpan(0, 17, 00, 00);
                    int Counts = 0;
                    foreach (var date in dates)
                    {
                        CHours = new CompanyWorkingHours();
                        CHours.CompanyId = CompanyId;
                        CHours.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        CHours.NameOfDay = Convert.ToString(date.DayOfWeek);
                        CHours.Start = tsStart;
                        CHours.End = tsEnd;
                        if (Counts == 5 || Counts == 6)
                        {
                            CHours.IsOffAllDay = false;
                        }
                        else
                        {
                            CHours.IsOffAllDay = true;
                        }
                        CompanyListWorkingDays.Add(CHours);
                        Counts++;
                    }
                    BusinessHoursData.ItemsSource = CompanyListWorkingDays;
                }
                else if (PageName == "SettingsCompanyHours")
                {
                    GetCompanyHours();
                    DateTime today = DateTime.Today;
                    int currentDayOfWeek = (int)today.DayOfWeek;
                    DateTime sunday = today.AddDays(-currentDayOfWeek);
                    DateTime monday = sunday.AddDays(1);

                    if (currentDayOfWeek == 0)
                    {
                        monday = monday.AddDays(-7);
                    }
                    var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
                    int count = 0;
                    foreach (var date in dates)
                    {
                        BHours = new BussinessHours();
                        BHours.NameOfDay = Convert.ToString(date.DayOfWeek);
                        //BHours.Start = tsStart;
                        //BHours.End = tsEnd;
                        switch (BHours.NameOfDay)
                        {
                            case "Sunday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Sunday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }

                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Monday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Monday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }


                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Tuesday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Tuesday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Wednesday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Wednesday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Thursday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Thursday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Friday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Friday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;          
                                break;
                            case "Saturday":
                                foreach (var item in CompanyListWorkingDays)
                                {
                                    switch (item.NameOfDay)
                                    {
                                        case "Saturday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                        }


                        BussinessDaysLst.Add(BHours);
                        count++;
                    }

                    BusinessHoursData.ItemsSource = BussinessDaysLst;
                }
                else
                {

                    if (Application.Current.Properties.ContainsKey("EmployeeID") == true)
                    {
                        EmployeeId = Convert.ToInt32(Application.Current.Properties["EmployeeID"]);
                    }
                    else if (Application.Current.Properties.ContainsKey("SelectedEmployeeID") == true)
                    {
                    EmployeeId = Convert.ToInt32(Application.Current.Properties["SelectedEmployeeID"]);
                    }

                    PageName = pagename;
                    GetBuisnessHoursofStaff();

                    DateTime today = DateTime.Today;
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
                    //var list = listofWorkingDays.Select(x => x.NameOfDayAsString).ToList();

                    //TimeSpan tsStart = new TimeSpan(0, 08, 00, 00);
                    //TimeSpan tsEnd = new TimeSpan(0, 05, 00, 00);
                    int count = 0;
                    foreach (var date in dates)
                    {
                        BHours = new BussinessHours();
                        BHours.NameOfDay = Convert.ToString(date.DayOfWeek);
                        //BHours.Start = tsStart;
                        //BHours.End = tsEnd;
                        switch (BHours.NameOfDay)
                        {
                            case "Sunday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Sunday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }

                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Monday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Monday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }


                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Tuesday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Tuesday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Wednesday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Wednesday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Thursday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Thursday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                            case "Friday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Friday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;          
                                break;
                            case "Saturday":
                                foreach (var item in listofWorkingDays)
                                {
                                    switch (item.NameOfDayAsString)
                                    {
                                        case "Saturday":
                                            BHours.Start = item.Start;
                                            BHours.End = item.End; ;
                                            if (item.IsOffAllDay == true)
                                            {
                                                BHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                BHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                    //BHours.IsOffAllDay = item.IsOffAllDay;
                                }
                                //BHours.IsOffAllDay = listofWorkingDays[count].IsOffAllDay;
                                break;
                        }


                        BussinessDaysLst.Add(BHours);
                        count++;
                    }

                    BusinessHoursData.ItemsSource = BussinessDaysLst;
                }
                
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        public void GetCompanyHours()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/company/GetOpeningHours?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);
                CompanyListWorkingDays = JsonConvert.DeserializeObject<ObservableCollection<CompanyWorkingHours>>(result);
                //foreach (var day in CompanyListWorkingDays)
                //{
                //    day.IsOffAllDay = day.IsOffAllDay == true ? false : true;
                //}
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void GetBuisnessHoursofStaff()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + EmployeeId;
                var result = PostData("GET", "", apiUrl);
                listofWorkingDays = JsonConvert.DeserializeObject<ObservableCollection<ProviderWorkingHours>>(result);
                //foreach (var day in listofWorkingDays)
                //{
                //    day.IsOffAllDay = day.IsOffAllDay == true ? false : true;
                //}
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public void SaveWorkingHours(object sender, EventArgs e)
        {
            try
            {
                if (PageName == "CompanyHours")
                {
                    List<CReqWorkingHours> listofworkinghours = new List<CReqWorkingHours>();
                    int count = 0;
                    foreach (var item in CompanyListWorkingDays)
                    {
                        string s = item.Start.ToString();
                        DateTime sdt = Convert.ToDateTime(s);
                        string starttime = sdt.ToString("HH:mm");

                        string en = item.End.ToString();
                        DateTime edt = Convert.ToDateTime(en);
                        string endtime = edt.ToString("HH:mm");


                        CRHours = new CReqWorkingHours();
                        CRHours.CompanyId = item.CompanyId;
                        CRHours.CreationDate = item.CreationDate;
                        CRHours.End = endtime;
                        CRHours.Id = item.Id;
                        if (item.IsOffAllDay == true)
                        {
                            CRHours.IsOffAllDay = false;
                        }
                        else
                        {
                            CRHours.IsOffAllDay = true;
                        }

                        //CRHours.IsOffAllDay = item.IsOffAllDay;
                        CRHours.NameOfDay = item.NameOfDay;
                        CRHours.Start = starttime;
                        listofworkinghours.Add(CRHours);
                        count++;
                    }
                    var SerializedObj = JsonConvert.SerializeObject(listofworkinghours);
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHoursForWeek";
                    var result = PostData("POST", SerializedObj, apiUrl);
                    Navigation.PushAsync(new AddStaffForCompanyRegistration());
                }
                else if (PageName == "SettingsCompanyHours")
                {
                    List<CReqWorkingHours> listofworkinghours = new List<CReqWorkingHours>();
                    int count = 0;
                    foreach (var item in CompanyListWorkingDays)
                    {
                        //string s = item.Start.ToString();
                        //DateTime sdt = Convert.ToDateTime(s);
                        //string starttime = sdt.ToString("HH:mm");

                        //string en = item.End.ToString();
                        //DateTime edt = Convert.ToDateTime(en);
                        //string endtime = edt.ToString("HH:mm");
                        CRHours = new CReqWorkingHours();
                        CRHours.CompanyId = item.CompanyId;
                        CRHours.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        //CRHours.End = endtime;
                        CRHours.Id = item.Id;
                        //CRHours.IsOffAllDay = item.IsOffAllDay;
                        CRHours.NameOfDay = item.NameOfDay;
                        //CRHours.Start = starttime;
                        switch (item.NameOfDay)
                        {
                            case "Sunday":
                                foreach (var list in BussinessDaysLst)
                                {

                                    switch (list.NameOfDay)
                                    {
                                        case "Sunday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Monday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Monday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Tuesday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Tuesday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Wednesday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Wednesday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Thursday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Thursday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Friday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Friday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Saturday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Saturday":
                                            string st = list.Start.ToString();
                                            DateTime sdti = Convert.ToDateTime(st);
                                            string starttimes = sdti.ToString("HH:mm");

                                            string ent = list.End.ToString();
                                            DateTime edti = Convert.ToDateTime(ent);
                                            string endtimes = edti.ToString("HH:mm");
                                            CRHours.Start = starttimes;
                                            CRHours.End = endtimes;
                                            if (list.IsOffAllDay == true)
                                            {
                                                CRHours.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                CRHours.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }

                        listofworkinghours.Add(CRHours);
                        count++;
                    }

                    var SerializedObj = JsonConvert.SerializeObject(listofworkinghours);
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHoursForWeek";
                    var result = PostData("POST", SerializedObj, apiUrl);
                    Navigation.PopAsync(true);
                }
                else
                {
                    int count = 0;
                    foreach (var item in listofWorkingDays)
                    {
                        obj = new ProviderWorkingHours();
                        obj.EmployeeId = EmployeeId;
                        obj.Id = item.Id;
                        obj.CompanyId = item.CompanyId;
                        obj.NameOfDay = item.NameOfDay;
                        obj.NameOfDayAsString = item.NameOfDayAsString;
                        obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.EntityStatus = "0";

                        switch (item.NameOfDayAsString)
                        {
                            case "Sunday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Sunday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Monday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Monday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Tuesday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Tuesday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Wednesday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Wednesday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Thursday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Thursday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Friday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Friday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "Saturday":
                                foreach (var list in BussinessDaysLst)
                                {
                                    switch (list.NameOfDay)
                                    {
                                        case "Saturday":
                                            obj.Start = list.Start;
                                            obj.End = list.End;
                                            if (list.IsOffAllDay == true)
                                            {
                                                obj.IsOffAllDay = false;
                                            }
                                            else
                                            {
                                                obj.IsOffAllDay = true;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }

                        var SerializedObj = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/SetWorkingHours";
                        var result = PostData("POST", SerializedObj, apiUrl);
                        count++;
                    }
                    
                    if (PageName == "CreatStaff")
                    {
                        Navigation.PushAsync(new StaffServicePeofile());
                    }
                    else
                    {
                        Navigation.PushAsync(new StaffProfileDetailsPage());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public Staff GetSelectedStaff()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + EmployeeId;
                var result = PostData("GET", "", apiUrl);
                Staff obj = JsonConvert.DeserializeObject<Staff>(result);
                return obj;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        private void MondayToggled(object sender, ToggledEventArgs e)
        {
            var data = BussinessDaysLst;
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