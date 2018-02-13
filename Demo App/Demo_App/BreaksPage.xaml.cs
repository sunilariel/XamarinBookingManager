using Demo_App.Model;
using Newtonsoft.Json;
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
        public StaffBreakTime BHours = null;
        ObservableCollection<StaffBreakTime> BussinessDaysLst = new ObservableCollection<StaffBreakTime>(); 
        public BreaksPage (int EmployeeId)
		{
			InitializeComponent ();
            StaffId = (EmployeeId).ToString();
            GetBreakTimeofEmployee();

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



            TimeSpan tsStart = new TimeSpan(0, 12, 00, 00);
            TimeSpan tsEnd = new TimeSpan(0, 01, 00, 00);          
            foreach (var date in dates)
            {
                BHours = new StaffBreakTime();
                BHours.NameOfDay = Convert.ToString(date.DayOfWeek);
                BHours.Start = tsStart;
                BHours.End = tsEnd;                
                BussinessDaysLst.Add(BHours);             
            }
            BreakTimeList.ItemsSource = BussinessDaysLst;
        }

        private void BreaksClick(object sender,EventArgs e)
        {          
            Timepicker.IsOpen = !Timepicker.IsOpen;
        }

        public void SetBreakTime()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/AddBreak";
                var result = PostData("POST", "", apiUrl);
            }
            catch(Exception e)
            {

            }
        }

        public void EditBreakTime()
        {

        }

        public void GetBreakTimeofEmployee()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetAllBreaksForEmployee?employeeId=" + StaffId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<BreakTime> BreakTimeofEmployee = JsonConvert.DeserializeObject<ObservableCollection<BreakTime>>(result);
            }
            catch(Exception e)
            {
                e.ToString();
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