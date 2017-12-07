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
            InitializeComponent();           
            EmployeeId = StaffId;
            GetBuisnessHoursofStaff();                        
        }

        public void GetBuisnessHoursofStaff()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + EmployeeId;

            var result = PostData("GET", "", apiUrl);

            listofWorkingDays = JsonConvert.DeserializeObject<ObservableCollection<ProviderWorkingHours>>(result);

            foreach(var day in listofWorkingDays)
            {
                day.IsOffAllDay= day.IsOffAllDay==true?false:true;
            }
        
            BusinessHoursData.ItemsSource = listofWorkingDays;
        }

      
        public void SaveStaffWorkingHours(object sender,EventArgs e)
        {           
            foreach( var item in listofWorkingDays)
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
            Navigation.PushAsync(new StaffProfileDetailsPage(GetSelectedStaff()));
        }

        public Staff GetSelectedStaff()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + EmployeeId;
            var result = PostData("GET", "", apiUrl);
            Staff obj = JsonConvert.DeserializeObject<Staff>(result);
            return obj;
        }

        private void MondayToggled(object sender, ToggledEventArgs e)
        {
            var data = listofWorkingDays;        
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