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
    public partial class StaffServicePeofile : ContentPage
    {
        #region GlobleFields
        public int StaffId;
        public string CompanyId = (Application.Current.Properties["CompanyId"]).ToString();
        ObservableCollection<AssignedServicetoStaff> ListOfServices = null;
        int ListofServicesCount = 0;
        int ListofAllocatedServicesCount = 0;
        public Staff objStaff = null;
        #endregion

        public StaffServicePeofile()
        {
            try
            {
                InitializeComponent();
                GetEmployeeDetail();
                StaffId = Convert.ToInt32(Application.Current.Properties["EmployeeID"]);
                //InitializeComponent();
                GetAllocatedServicetoStaff();
                GetAllTimeOffForEmployee();
                AllocationCount.Text = ListofAllocatedServicesCount + "/" + ListofServicesCount + " " + "services active";
                BindingContext = objStaff;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        private void WorkingDays(object sender, EventArgs args)
        {
            Navigation.PushAsync(new BusinessHoursPage("CreatStaff"));
        }
        private void ServiceProvided(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AssignServiceToStaffPage(objStaff, ListOfServices));
        }
        private void BreaksClick(object sender, EventArgs args)
        {
            SaveStaffBreakTime obj = new SaveStaffBreakTime();
            Navigation.PushAsync(new BreaksPage(obj, StaffId, "", CompanyId));
        }
        private void DoneClick(object sender, EventArgs args)
        {
            Application.Current.Properties.Remove("EmployeeID");
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);                  
            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            //Navigation.PopAsync(true);           

            Navigation.PushAsync(new StaffPage());

            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 3)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }


        }

        private void CrossClick(object sender, EventArgs e)
        {

            Application.Current.Properties.Remove("EmployeeID");

            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            //Navigation.PopAsync(true);

            Navigation.PushAsync(new StaffPage());

            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 3)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.Properties.Remove("EmployeeID");
            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            //Navigation.PopAsync(true);

            Navigation.PushAsync(new StaffPage());

            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 3)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }
            return true;
        }

        public void GetAllocatedServicetoStaff()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetAllocateServiceForEmployee?empid=" + StaffId + "&compid=" + CompanyId;

                var result = PostData("GET", "", apiUrl);

                ObservableCollection<AssignedServicetoStaff> ListofAllocatedService = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);

                ListOfServices = GetAllService();

                ListofServicesCount = ListOfServices.Count;
                ListofAllocatedServicesCount = ListofAllocatedService.Count;

                foreach (var service in ListOfServices)
                {
                    foreach (var selectedservice in ListofAllocatedService)
                    {
                        if (service.Id == selectedservice.Id)
                        {
                            service.isAssigned = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void GetEmployeeDetail()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + Application.Current.Properties["EmployeeID"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objStaff = JsonConvert.DeserializeObject<Staff>(result);
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public void GetAllTimeOffForEmployee()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetWorkingHours?employeeId=" + StaffId;
                var result = PostData("GET", "", apiUrl);

                List<ProviderWorkingHours> WorkingHourStatus = JsonConvert.DeserializeObject<List<ProviderWorkingHours>>(result);

                foreach (var item in WorkingHourStatus)
                {
                    switch (item.NameOfDayAsString)
                    {
                        case "Sunday":
                            if (item.IsOffAllDay == true)
                            {
                                Sunday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Sunday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Monday":
                            if (item.IsOffAllDay == true)
                            {
                                Monday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Monday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Tuesday":
                            if (item.IsOffAllDay == true)
                            {
                                Tuesday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Tuesday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Wednesday":
                            if (item.IsOffAllDay == true)
                            {
                                Wednesday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Wednesday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Thursday":
                            if (item.IsOffAllDay == true)
                            {
                                Thursday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Thursday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Friday":
                            if (item.IsOffAllDay == true)
                            {
                                Friday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Friday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                        case "Saturday":
                            if (item.IsOffAllDay == true)
                            {
                                Saturday.TextColor = Xamarin.Forms.Color.Gray;
                            }
                            else
                            {
                                Saturday.TextColor = Xamarin.Forms.Color.Black;
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public ObservableCollection<AssignedServicetoStaff> GetAllService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<AssignedServicetoStaff> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
                return ListofServices;
            }
            catch (Exception e)
            {
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