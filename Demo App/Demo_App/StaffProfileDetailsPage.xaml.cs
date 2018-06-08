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
    public partial class StaffProfileDetailsPage : ContentPage
    {
        #region GlobleFields
        public Staff objStaff = null;
        public int StaffId;
        public string CompanyId = (Application.Current.Properties["CompanyId"]).ToString();
        ObservableCollection<AssignedServicetoStaff> ListOfServices = null;
        int ListofServicesCount = 0;
        int ListofAllocatedServicesCount = 0;
        #endregion

        public StaffProfileDetailsPage()
        {
            if (Application.Current.Properties.ContainsKey("SelectedEmployeeID") == true)
            {
                StaffId = Convert.ToInt32(Application.Current.Properties["SelectedEmployeeID"]);
            }
            else if (Application.Current.Properties.ContainsKey("EmployeeID") == true)
            {
                StaffId = Convert.ToInt32(Application.Current.Properties["EmployeeID"]);
            }



            //StaffId = Convert.ToInt32(Application.Current.Properties["EmployeeID"]);
            //StaffId = Convert.ToInt32(Application.Current.Properties["SelectedEmployeeID"]);
            InitializeComponent();
            GetAllocatedServicetoStaff();
            GetEmployeeDetail();

            GetAllTimeOffForEmployee();
            ServiceAllocationCount.Text = ListofAllocatedServicesCount + "/" + ListofServicesCount + " " + "services active";

            //Objstaff = new Staff();
            //Objstaff.Id = staff.Id;
            //Objstaff.FirstName = staff.FirstName;
            //Objstaff.LastName = staff.LastName;
            //Objstaff.Email = staff.Email;
            //Objstaff.TelephoneNo = staff.TelephoneNo;
            //Objstaff.Address = staff.Address;
            BindingContext = objStaff;
        }

        private void CrossClick(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("EmployeeID");
            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            // Navigation.PopAsync(true);

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

            // Navigation.PopAsync(true);

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

        private void BreaksClick(object sender, EventArgs e)
        {
            SaveStaffBreakTime obj = new SaveStaffBreakTime();
            Navigation.PushAsync(new BreaksPage(obj, StaffId, "", CompanyId));
        }
        private void WorkingDaysClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BusinessHoursPage("EditStaff"));
        }
        private void ServiceProvidedClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ServicesProviderPage(StaffId, ListOfServices));
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

        public void GetEmployeeDetail()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + Application.Current.Properties["SelectedEmployeeID"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objStaff = JsonConvert.DeserializeObject<Staff>(result);
            }
            catch (Exception e)
            {
                e.ToString();
            }

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

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (e.TotalY != 0 && CustomerProfile.HeightRequest > 30 && CustomerProfile.HeightRequest < 151)
                {
                    CustomerProfile.HeightRequest = CustomerProfile.HeightRequest + e.TotalY;
                    if (CustomerProfile.HeightRequest < 31)
                        CustomerProfile.HeightRequest = 31;
                    if (CustomerProfile.HeightRequest > 150)
                        CustomerProfile.HeightRequest = 150;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //stack.HeightRequest = 20;
        }

        public async void DeleteStaff(int Id)
        {
            Application.Current.Properties.Remove("EmployeeID");
            try
            {

                var confirmed = await DisplayAlert("Confirm", "Are you sure You want to delete this Staff", "Yes", "No");
                if (confirmed)
                {
                    var CompanyId = Application.Current.Properties["CompanyId"];
                    var Method = "DELETE";
                    var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/DeleteStaff?id=" + StaffId;
                    var result = PostData(Method, null, Url);

                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 3; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                    }

                    // Navigation.PopAsync(true);

                    await Navigation.PushAsync(new StaffPage());

                    int pCount = Navigation.NavigationStack.Count();

                    for (int i = 0; i < pCount; i++)
                    {
                        if (i == 3)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[i]);
                        }
                    }



                }
                else
                {
                    await Navigation.PushAsync(new StaffProfileDetailsPage());
                }


            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private void EditStaffDetails(object sender, EventArgs e)
        {
            //Staff staff = new Staff();
            Navigation.PushAsync(new EditStaffPage());
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