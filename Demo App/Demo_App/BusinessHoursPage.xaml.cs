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
        int EmployeeId;
        ObservableCollection<ProviderWorkingHours> listofWorkingDays = new ObservableCollection<ProviderWorkingHours>();
        public BusinessHoursPage(int StaffId)
        {
            //ObservableCollection<StaffWorkingHours> listofWorkingDays = new ObservableCollection<StaffWorkingHours>();
            InitializeComponent();
            GetBusinessHours();
            EmployeeId = StaffId;
            //BindingContext = new StaffWorkingHours();
            //TestListview.ItemsSource = listofWorkingDays;
            //TestListview.ItemsSource = new List<string> {"fasfasf","fasfas"};
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


        public void GetBusinessHours()
        {
            string[] Days = new string[] {"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};
            for (var i = 0; i <= 6; i++)
            {
                ProviderWorkingHours obj = new ProviderWorkingHours();
                obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
               // obj.CompanyId = 2;
                obj.EmployeeId = EmployeeId;
                obj.NameOfDay = i;
                obj.NameOfDayAsString = Days[i];
                if (Days[i] == "Sunday" || Days[i] == "Saturday")
                {
                    obj.IsOffAllDay = false;
                }
                else
                {
                    obj.IsOffAllDay = true;
                }
                TimeSpan startTime = new TimeSpan(8, 0, 0);
                TimeSpan endTime = new TimeSpan(17, 0, 0);
                obj.Start = startTime;
                obj.End = endTime;

                obj.CreationDate = DateTime.Now.ToString();

                listofWorkingDays.Add(obj);
            }

            BusinessHoursData.ItemsSource = listofWorkingDays;
        }


        public void SaveStaffWorkingHours(object sender,EventArgs e)
        {
            ObservableCollection<ProviderWorkingHours> StaffWorkingHours = listofWorkingDays;

            foreach( var item in StaffWorkingHours)
            {
                ProviderWorkingHours obj = new ProviderWorkingHours();
                obj.EmployeeId = EmployeeId;
                obj.Id = item.Id;
                obj.CompanyId = item.CompanyId;
                obj.NameOfDay = item.NameOfDay;
                obj.NameOfDayAsString = item.NameOfDayAsString;
                if (item.IsOffAllDay == false)
                {
                    obj.IsOffAllDay = true;
                }
                else
                {
                    obj.IsOffAllDay = false;

                }
                obj.Start = item.Start;
                obj.End = item.End;
                obj.CreationDate = "2017-11-10T10:57:47.1870909+01:00";
                obj.EntityStatus = "0";

                var SerializedObj = JsonConvert.SerializeObject(obj);
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/SetWorkingHours";
                    var result = PostData("POST", SerializedObj, apiUrl);
           
            }               
            }


        private void MondayToggled(object sender, ToggledEventArgs e)
        {
            var data = listofWorkingDays;
            //if (e.Value == true)
            //{
            //    lblMonday.TextColor = Color.Black;
            //}
            //else
            //{
            //    lblMonday.TextColor = Color.Gray;
            //}
        }
    }

}