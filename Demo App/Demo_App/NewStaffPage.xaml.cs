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

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewStaffPage : ContentPage
    {
        public NewStaffPage()
        {
            InitializeComponent();
        }

        public void AddStaff(object sender, SelectedItemChangedEventArgs e)
        {
            Staff obj = new Staff();
            obj.Id = 0;
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.UserName = StaffEmail.Text;
            obj.Password = "";
            obj.FirstName = StaffFirstName.Text;
            obj.LastName = StaffLastName.Text;
            obj.Address = StaffAddress.Text;
            obj.Email = StaffEmail.Text;
            obj.TelephoneNo = StaffPhoneNumber.Text;
            obj.CreationDate = "2017-11-08T12:19:27.628Z";

            var data = JsonConvert.SerializeObject(obj);
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AddStaff";
            var ApiMethod = "POST";

            var result = PostData(ApiMethod, data, Url);

            //var Requestresponse = JsonConvert.DeserializeObject(result);
            //var jobject = (JObject)JsonConvert.DeserializeObject(result);
            //var jvalue = (JValue)jobject["ReturnObject"]["EmloyeeId"];

            JObject responsedata = JObject.Parse(result);
            dynamic ResponseValue = responsedata["ReturnObject"]["EmloyeeId"];
            int EmployeeId = Convert.ToInt32(ResponseValue.Value);

            SetBuisnessHours(EmployeeId);
            // Navigation.PushAsync(new StaffPage());

            //var staffdata =e.SelectedItem as Staff;
            //Navigation.PushAsync(new StaffServicePeofile(EmployeeId, staffdata)); 

            Navigation.PushAsync(new StaffServicePeofile(EmployeeId, obj));

        }

        public void GetStaff()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
            var Method = "GET";

            var result = PostData(Method, null, Url);
        }

        public void DeleteStaff()
        {

            var CompanyId = Application.Current.Properties["CompanyId"];
            var Method = "DELETE";
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/DeleteStaff?id=" + 1;
            PostData(Method, null, Url);


        }

        public string UpdateStaff()
        {
            Staff obj = new Staff();
            obj.Id = 0;
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.UserName = "123@gmail.com";
            obj.Password = "";
            obj.FirstName = "123";
            obj.LastName = "";
            obj.Address = "";
            obj.Email = "123@gmail.com";
            obj.TelephoneNo = "";
            obj.CreationDate = "2017-11-08T12:19:27.628Z";

            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/Update";
            var SerializedData = JsonConvert.SerializeObject(obj);
            var Method = "POST";

            var result = PostData(Method, SerializedData, apiUrl);
            return result;
        }

        public void SetBuisnessHours(int Id)
        {
            StaffWorkingHours obj = new StaffWorkingHours();
            for (var i = 0; i <= 6; i++)
            {
                obj.Id = 0;
                obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                obj.EmployeeId = Id;
                obj.Start = "08:00";
                obj.End = "17:00";

                if (i == 0)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Sunday";
                    obj.IsOffAllDay = true;
                }

                else if (i == 1)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Monday";
                    obj.IsOffAllDay = false;
                }
                else if (i == 2)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Tuesday";
                    obj.IsOffAllDay = false;
                }
                else if (i == 3)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Wednesday";
                    obj.IsOffAllDay = false;
                }
                else if (i == 4)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Thursday";
                    obj.IsOffAllDay = false;
                }
                else if (i == 5)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Friday";
                    obj.IsOffAllDay = false;
                }
                else if (i == 6)
                {
                    obj.NameOfDay = i;
                    obj.NameOfDayAsString = "Saturday";
                    obj.IsOffAllDay = true;
                }


                obj.CreationDate = "2017-11-10T10:57:47.1870909+01:00";
                obj.EntityStatus = "0";



                var SerializedObj = JsonConvert.SerializeObject(obj);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/SetWorkingHours";
                var result = PostData("POST", SerializedObj, apiUrl);

            }

        }

        public string SetEmployeeBreakTime()
        {
            BreakTime dataObj = new BreakTime();
            dataObj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            dataObj.EmployeeId = 3;
            dataObj.DayOfWeek = 5;
            dataObj.Start = "";
            dataObj.End = "";
            dataObj.CreationDate = new DateTime().ToString();

            DateTime startdate = DateTime.Parse(dataObj.Start, CultureInfo.CurrentCulture);
            dataObj.Start = startdate.ToString("HH:mm");
            DateTime endtdate = DateTime.Parse(dataObj.End, CultureInfo.CurrentCulture);
            dataObj.End = endtdate.ToString("HH:mm");

            var SerializedData = JsonConvert.SerializeObject(dataObj);
            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/AddBreak";

            var result = PostData("POST", SerializedData, apiUrl);
            return result;

        }

        public string UpdateBreakTimeofEmployee(string Status)
        {

            BreakTime dataObj = new BreakTime();
            dataObj.Id = "";
            dataObj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            dataObj.EmployeeId = 3;
            dataObj.DayOfWeek = 5;
            dataObj.Start = "";
            dataObj.End = "";
            dataObj.CreationDate = new DateTime().ToString();


            DateTime startdate = DateTime.Parse(dataObj.Start, CultureInfo.CurrentCulture);
            dataObj.Start = startdate.ToString("HH:mm");
            DateTime endtdate = DateTime.Parse(dataObj.End, CultureInfo.CurrentCulture);
            if (Status == "Start")
            {
                endtdate = startdate.AddHours(1);
            }
            dataObj.End = endtdate.ToString("HH:mm");


            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/UpdateBreak";
            var SerializedData = JsonConvert.SerializeObject(dataObj);

            var result = PostData("POST", SerializedData, apiUrl);
            return result;

        }

        public string GetBreakTimeHoursofEmployee(string EmployeeId)
        {
            try
            {
                var result = "";

                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/GetWorkingHours?employeeId=" + EmployeeId;
                result = PostData("GET", "", apiUrl);


                var data = JsonConvert.DeserializeObject<List<EmployeeWorkingHours>>(result);
                List<EmployeeWorkingHours> empworkinghours = new List<EmployeeWorkingHours>();
                empworkinghours = data;


                apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/GetAllBreaksForEmployee?employeeId=" + EmployeeId;
                result = PostData("GET", "", apiUrl);



                var breakdata = JsonConvert.DeserializeObject<List<BreakTime>>(result);
                List<BreakTime> ListofBreaktime = new List<BreakTime>();
                ListofBreaktime = breakdata;

                List<BreakTimeHoursofEmployee> listofEmployeeBreakTime = new List<BreakTimeHoursofEmployee>();
                foreach (var item in empworkinghours)
                {

                    BreakTimeHoursofEmployee dt = new BreakTimeHoursofEmployee();
                    dt.EmployeeId = item.EmployeeId;
                    dt.CompanyId = item.CompanyId;
                    dt.Day = item.NameOfDayAsString;
                    dt.DayOfWeek = item.NameOfDay;
                    dt.CreationDate = DateTime.Now.ToString();
                    dt.Available = item.IsOffAllDay == true ? false : true;
                    List<TimeSchedule> objtime = new List<TimeSchedule>();
                    foreach (var obj in ListofBreaktime)
                    {
                        if (item.NameOfDay == obj.DayOfWeek)
                        {

                            TimeSchedule time = new TimeSchedule();
                            time.Id = obj.Id;
                            time.DayOfWeek = obj.DayOfWeek;
                            DateTime startdate = DateTime.Parse(obj.Start, CultureInfo.CurrentCulture);
                            time.Start = startdate.ToString("hh:mm tt");
                            DateTime enddatedate = DateTime.Parse(obj.End, CultureInfo.CurrentCulture);
                            time.End = enddatedate.ToString("hh:mm tt");
                            objtime.Add(time);

                        }
                    }
                    dt.StartEndTime = objtime;
                    listofEmployeeBreakTime.Add(dt);
                }
                var jsonstring = JsonConvert.SerializeObject(listofEmployeeBreakTime.OrderBy(x => ((int)x.DayOfWeek + 6) % 7));
                return jsonstring;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteBreakTimeOfEmployee(string BreakId)
        {
            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/DeleteBreak?id=" + BreakId;
            var result = PostData("DELETE", "", apiUrl);
            return result;
        }

        public string AllocateServicetoEmployee(AssignServiceToStaff dataObj)
        {
            string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/AllocateService";
            var jsonstring = JsonConvert.SerializeObject(dataObj);

            var result = PostData("POST", jsonstring, apiUrl);
            return result;
        }

        public string DeAllocateServicetoEmployee(string CompanyId, string EmployeeId, string ServiceId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/DeAllocateServiceForEmployee?companyId=" + CompanyId + "&employeeId=" + EmployeeId + "&serviceId=" + ServiceId;

            var result = PostData("POST", "", apiUrl);
            return result;
        }

        public string GetAllServiceStatus(string CompanyId, string EmployeeId)
        {
            try
            {
                //Get List of Allocated Service//
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/GetAllocateServiceForEmployee?empid=" + EmployeeId + "&compid=" + CompanyId;

                var result = PostData("GET", "", apiUrl);


                List<AssignedServiceStatus> listofAllocatedService = new List<AssignedServiceStatus>();
                listofAllocatedService = JsonConvert.DeserializeObject<List<AssignedServiceStatus>>(result);

                var AllocatedServiceCount = listofAllocatedService.Count();

                //Get List of All Services//

                apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;

                result = PostData("GET", "", apiUrl);


                List<AssignedServiceStatus> listofAllServices = new List<AssignedServiceStatus>();
                listofAllServices = JsonConvert.DeserializeObject<List<AssignedServiceStatus>>(result);

                foreach (var item in listofAllServices)
                {
                    foreach (var data in listofAllocatedService)
                    {

                        item.AllocatedServiceCount = (listofAllocatedService.Count()).ToString();

                        if (item.Id == data.Id && item.Name == data.Name)
                        {
                            item.Confirmed = true;
                            break;
                        }
                        else
                        {
                            item.Confirmed = false;
                        }
                    }
                }

                var jsonstring = JsonConvert.SerializeObject(listofAllServices);
                return jsonstring;

            }
            catch (Exception exception)
            {
                return exception.ToString();
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

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && stack.HeightRequest > 30 && stack.HeightRequest < 151)
            {
                stack.HeightRequest = stack.HeightRequest + e.TotalY;
                if (stack.HeightRequest < 31)
                    stack.HeightRequest = 31;
                if (stack.HeightRequest > 150)
                    stack.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }

        private void CrossClick(object sender,EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}

