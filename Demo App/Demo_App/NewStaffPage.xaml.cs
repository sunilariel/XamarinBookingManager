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
    public partial class NewStaffPage : ContentPage
    {

        string PageName = "";
        ObservableCollection<CompanyWorkingHours> CompanyListWorkingDays = new ObservableCollection<CompanyWorkingHours>();
        public ProviderWorkingHours BHours = null;
        public NewStaffPage(string pagename)
        {
            PageName = pagename;
            InitializeComponent();
        }

        public void AddStaff(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (StaffFirstName.Text == null)
                {
                    return;
                }
                else
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
                    obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                    var data = JsonConvert.SerializeObject(obj);
                    var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AddStaff";
                    var ApiMethod = "POST";

                    var result = PostData(ApiMethod, data, Url);

                    JObject responsedata = JObject.Parse(result);

                    dynamic ResponseValue = responsedata["ReturnObject"]["EmloyeeId"];
                    Application.Current.Properties["EmployeeID"] = Convert.ToInt32(ResponseValue.Value);
                    int EmployeeId = Convert.ToInt32(ResponseValue.Value);

                    SetBuisnessHours(EmployeeId);

                    if (PageName == "StaffCreateAfterLogin")
                    {
                        Navigation.PushAsync(new StaffServicePeofile());
                    }
                    else if (PageName == "StaffCreateAfterRegistration")
                    {
                        Navigation.PushAsync(new AddStaffForCompanyRegistration());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void GetStaff()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, null, Url);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void DeleteStaff()
        {

            var CompanyId = Application.Current.Properties["CompanyId"];
            var Method = "DELETE";
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/DeleteStaff?id=" + 1;
            PostData(Method, null, Url);


        }

        public void GetCompanyHours()
        {
            try
            {
                var CompanyId =Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/company/GetOpeningHours?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);
                CompanyListWorkingDays = JsonConvert.DeserializeObject<ObservableCollection<CompanyWorkingHours>>(result);
                foreach (var day in CompanyListWorkingDays)
                {
                    day.IsOffAllDay = day.IsOffAllDay == true ? false : true;
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void SetBuisnessHours(int Id)
        {
            //if (PageName== "StaffCreateAfterLogin")
            //{

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
                var CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                BHours = new ProviderWorkingHours();
                BHours.NameOfDayAsString = Convert.ToString(date.DayOfWeek);
                BHours.EmployeeId = Id;
                BHours.NameOfDay = Convert.ToInt32(date.DayOfWeek);
                BHours.CompanyId = CompanyId;
                BHours.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                BHours.EntityStatus = "0";
                switch (BHours.NameOfDayAsString)
                {                    
                    case "Sunday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Sunday":                                    
                                    BHours.Id = item.Id;
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                        
                        break;
                    case "Monday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Monday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                       
                        break;
                    case "Tuesday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Tuesday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                        
                        break;
                    case "Wednesday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Wednesday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                        
                        break;
                    case "Thursday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Thursday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                        
                        break;
                    case "Friday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Friday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                                 
                        break;
                    case "Saturday":
                        foreach (var item in CompanyListWorkingDays)
                        {
                            switch (item.NameOfDay)
                            {
                                case "Saturday":
                                    BHours.Start = item.Start;
                                    BHours.End = item.End;
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
                        }                        
                        break;
                }
                var SerializedObj = JsonConvert.SerializeObject(BHours);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/SetWorkingHours";
                var result = PostData("POST", SerializedObj, apiUrl);
                count++;
            }
            //StaffWorkingHours obj = new StaffWorkingHours();

            //for (var i = 0; i <= 6; i++)
            //{
            //    obj.Id = 0;
            //    obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            //    obj.EmployeeId = Id;
            //    obj.Start = "08:00";
            //    obj.End = "17:00";

            //    if (i == 0)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Sunday";
            //        obj.IsOffAllDay = true;
            //    }

            //    else if (i == 1)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Monday";
            //        obj.IsOffAllDay = false;
            //    }
            //    else if (i == 2)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Tuesday";
            //        obj.IsOffAllDay = false;
            //    }
            //    else if (i == 3)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Wednesday";
            //        obj.IsOffAllDay = false;
            //    }
            //    else if (i == 4)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Thursday";
            //        obj.IsOffAllDay = false;
            //    }
            //    else if (i == 5)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Friday";
            //        obj.IsOffAllDay = false;
            //    }
            //    else if (i == 6)
            //    {
            //        obj.NameOfDay = i;
            //        obj.NameOfDayAsString = "Saturday";
            //        obj.IsOffAllDay = true;
            //    }


            //    obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //    obj.EntityStatus = "0";



            //    var SerializedObj = JsonConvert.SerializeObject(obj);

            //    //var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHours";
            //    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/SetWorkingHours";
            //    var result = PostData("POST", SerializedObj, apiUrl);

            //    //}
            //}
            //else if (PageName== "StaffCreateAfterRegistration")
            //{
            //    StaffWorkingHours obj = new StaffWorkingHours();
            //    for (var i = 0; i <= 6; i++)
            //    {
            //        obj.Id = 0;
            //        obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            //        obj.EmployeeId = Id;
            //        obj.Start = "08:00";
            //        obj.End = "17:00";

            //        if (i == 0)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Sunday";
            //            obj.IsOffAllDay = true;
            //        }

            //        else if (i == 1)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Monday";
            //            obj.IsOffAllDay = false;
            //        }
            //        else if (i == 2)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Tuesday";
            //            obj.IsOffAllDay = false;
            //        }
            //        else if (i == 3)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Wednesday";
            //            obj.IsOffAllDay = false;
            //        }
            //        else if (i == 4)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Thursday";
            //            obj.IsOffAllDay = false;
            //        }
            //        else if (i == 5)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Friday";
            //            obj.IsOffAllDay = false;
            //        }
            //        else if (i == 6)
            //        {
            //            obj.NameOfDay = i;
            //            obj.NameOfDayAsString = "Saturday";
            //            obj.IsOffAllDay = true;
            //        }


            //        obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            //        obj.EntityStatus = "0";



            //        var SerializedObj = JsonConvert.SerializeObject(obj);

            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/SetWorkingHours";

            //        //var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/SetWorkingHours";
            //        var result = PostData("POST", SerializedObj, apiUrl);

            //    }
            //}



        }

        public string SetEmployeeBreakTime(int Id)
        {
            BreakTime dataObj = new BreakTime();
            dataObj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            dataObj.EmployeeId = Id;
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

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}

