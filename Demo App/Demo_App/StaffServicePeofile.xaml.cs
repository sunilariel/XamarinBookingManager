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
        public int StaffId;      
        public string CompanyId = (Application.Current.Properties["CompanyId"]).ToString();
        ObservableCollection<AssignedServicetoStaff> ListOfServices = null;
        int ListofServicesCount = 0;
        int ListofAllocatedServicesCount = 0;
        public Staff objStaff = null;
        public StaffServicePeofile (int EmployeeId,Staff staffdata)
		{
			InitializeComponent ();
            objStaff = new Staff();
            objStaff.Id = EmployeeId;
            objStaff.FirstName = staffdata.FirstName;
            objStaff.LastName = staffdata.LastName;
            objStaff.Email = staffdata.Email;
            objStaff.Address = staffdata.Address;
            objStaff.TelephoneNo = staffdata.TelephoneNo;
            StaffId = EmployeeId;
            //StaffId = staff.Id;
            InitializeComponent();
            GetAllocatedServicetoStaff();
            GetAllTimeOffForEmployee();
           AllocationCount.Text = ListofAllocatedServicesCount + "/" + ListofServicesCount + " " + "services active";
            BindingContext = staffdata;
        }
        private void WorkingDays(object sender, EventArgs args)
        {
            Navigation.PushAsync(new BusinessHoursPage(StaffId));
        }
        private void ServiceProvided(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AssignServiceToStaffPage(objStaff));
        }
        private void BreaksClick(object sender, EventArgs args)
        {
            
        }
        private void DoneClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new StaffPage());
        }

        public void GetAllocatedServicetoStaff()
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

        public void GetAllTimeOffForEmployee()
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

        public ObservableCollection<AssignedServicetoStaff> GetAllService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);

            ObservableCollection<AssignedServicetoStaff> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
            return ListofServices;
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