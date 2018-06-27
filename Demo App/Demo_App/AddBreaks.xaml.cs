using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo_App.Model;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Rg.Plugins.Popup.Services;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBreaks : PopupPage
    {
        int StaffIDs;
        string SelectDay;
        int CompanyID;
        public StaffBreakTime BHours = null;
        ObservableCollection<StaffBreakTime> AddBreakTimeLists = new ObservableCollection<StaffBreakTime>();

        public AddBreaks(int staffId, string day, int compID)
        {
            InitializeComponent();
            StaffIDs = staffId;
            SelectDay = day;
            CompanyID = compID;
            TimeSpan tmStart = new TimeSpan(0, 13, 00, 00);
            TimeSpan tmEnd = new TimeSpan(0, 14, 00, 00);
            BHours = new StaffBreakTime();
            BHours.CompanyId = CompanyID;
            BHours.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            BHours.NameOfDay = day;



            BHours.Start = tmStart;
            BHours.End = tmEnd;
            BHours.EmployeeId = StaffIDs;
            AddBreakTimeLists.Add(BHours);
            AddBreakTimeList.ItemsSource = AddBreakTimeLists;

            //var hours = Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh.mm tt"));
            //foreach (var item in hours)
            //{
            //    StartPicker.Items.Add(item);
            //    EndPicker.Items.Add(item);
            //}

        }

        public void SaveStaffBreaks_Clicked(object sender, EventArgs e)
        {
            //List<SaveStaffBreakTime> obj = new List<SaveStaffBreakTime>();
            SaveStaffBreakTime obj = new SaveStaffBreakTime();

            foreach (var item in AddBreakTimeLists)
            {
                int weekValue = 0;
                switch (item.NameOfDay)
                {
                    case "Monday":
                        weekValue = (int)DayOfWeek.Monday;
                        break;
                    case "Tuesday":
                        weekValue = (int)DayOfWeek.Tuesday;
                        break;
                    case "Wednesday":
                        weekValue = (int)DayOfWeek.Wednesday;
                        break;
                    case "Thursday":
                        weekValue = (int)DayOfWeek.Thursday;
                        break;
                    case "Friday":
                        weekValue = (int)DayOfWeek.Friday;
                        break;
                    case "Saturday":
                        weekValue = (int)DayOfWeek.Saturday;
                        break;
                    case "Sunday":
                        weekValue = (int)DayOfWeek.Sunday;
                        break;
                }
                //`}

                string stt = item.Start.ToString();
                string ett = item.End.ToString();
                DateTime st = Convert.ToDateTime(stt);
                DateTime et = Convert.ToDateTime(ett);
                if (st == et)
                {
                    et = et.AddHours(1);
                }
                string StartTime = st.ToString("HH:mm");
                string EndTime = et.ToString("HH:mm");
                //SaveStaffBreakTime staff = new SaveStaffBreakTime();
                //obj.BreakID = 0;
                obj.CompanyId = item.CompanyId;
                obj.CreationDate = item.CreationDate;
                obj.DayOfWeek = weekValue;
                obj.EmployeeId = item.EmployeeId;
                obj.Start = StartTime;
                obj.End = EndTime;
                var SerializedObj = JsonConvert.SerializeObject(obj);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/AddBreak";
                var result = PostData("POST", SerializedObj, apiUrl);
            }
            disaaaa.IsVisible = false;
            PopupNavigation.PopAsync();

            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 6; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            // Navigation.PopAsync(true);

            Navigation.PushAsync(new BreaksPage(obj, StaffIDs, SelectDay, Convert.ToString(CompanyID)));


            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 5)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }




        }
        public EventHandler SaveButtonEventHandler { get; set; }
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