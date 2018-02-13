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
        string PageName = "";
        public BussinessHours BHours = null;
        ObservableCollection<BussinessHours> BussinessDaysLst = new ObservableCollection<BussinessHours>();
        ObservableCollection<ProviderWorkingHours> listofWorkingDays = new ObservableCollection<ProviderWorkingHours>();
        #endregion

        public BusinessHoursPage(string pagename)
        {
            try
            {               
               
                InitializeComponent();
                if (Application.Current.Properties.ContainsKey("EmployeeID") == true)
                {
                    EmployeeId = Convert.ToInt32(Application.Current.Properties["EmployeeID"]);
                }
                else if(Application.Current.Properties.ContainsKey("SelectedEmployeeID") == true)
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



                TimeSpan tsStart = new TimeSpan(0, 08, 00, 00);
                TimeSpan tsEnd = new TimeSpan(0, 05, 00, 00);
                int count = 0;
                foreach (var date in dates)
                {
                    BHours = new BussinessHours();
                    BHours.NameOfDay = Convert.ToString(date.DayOfWeek);
                    BHours.Start = tsStart;
                    BHours.End = tsEnd;
                    if (count == 5 || count == 6)
                    {
                        BHours.IsOffAllDay = false;
                    }
                    else
                    {
                        BHours.IsOffAllDay = true;
                    }
                    BussinessDaysLst.Add(BHours);
                    count++;
                }

                BusinessHoursData.ItemsSource = BussinessDaysLst;
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

                foreach (var day in listofWorkingDays)
                {
                    day.IsOffAllDay = day.IsOffAllDay == true ? false : true;
                }

                //BusinessHoursData.ItemsSource = listofWorkingDays;
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
                    foreach( var data in BussinessDaysLst)
                    {
                        CompanyWorkingHours objhurs = new CompanyWorkingHours();
                        objhurs.Id = 0;
                        objhurs.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                        objhurs.CreationDate = "2018-2-10T10:57:47.1870909+01:00";
                        objhurs.NameOfDay = data.NameOfDay;
                        objhurs.Start = data.Start.ToString();
                        objhurs.End = data.End.ToString();
                        objhurs.IsOffAllDay = data.IsOffAllDay;
                        objhurs.EntityStatus = "0";
                        var SerializedObj = JsonConvert.SerializeObject(objhurs);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHours";
                        var result = PostData("POST", SerializedObj, apiUrl);

                    }
                    Navigation.PushAsync(new AddStaffForCompanyRegistration());
                }
                else if(PageName == "SettingsCompanyHours")
                {
                    foreach (var data in BussinessDaysLst)
                    {
                        CompanyWorkingHours objhurs = new CompanyWorkingHours();
                        objhurs.Id = 0;
                        objhurs.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                        objhurs.CreationDate = Convert.ToString(DateTime.Now);
                        objhurs.NameOfDay = data.NameOfDay;
                        objhurs.Start = data.Start.ToString();
                        objhurs.End = data.End.ToString();
                        objhurs.IsOffAllDay = data.IsOffAllDay;
                        objhurs.EntityStatus = "0";
                        var SerializedObj = JsonConvert.SerializeObject(objhurs);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHours";
                        var result = PostData("POST", SerializedObj, apiUrl);

                    }
                    Navigation.PopAsync(true);
                }
                else
                {
                    foreach (var item in listofWorkingDays)
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