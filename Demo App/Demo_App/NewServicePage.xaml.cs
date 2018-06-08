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
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;


namespace Demo_App
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewServicePage : ContentPage
    {
        #region GlobleFields
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<AssignProvider> ListofServiceProviders = new ObservableCollection<AssignProvider>();
        //string name = "";
        string DurationOfService = "";
        string PageName;
        //string PageNames = Convert.ToString(Application.Current.Properties["selectedPage"]);
        #endregion
        string test = "test";
        public NewServicePage(ObservableCollection<object> todaycollection, ObservableCollection<object> todaycollectionBuffer, string pagename)
        {
            try
            {
                test = pagename;

                PageName = pagename;

                Application.Current.Properties["selectPage"] = pagename;

                InitializeComponent();

                if (Application.Current.Properties["ServiceName"] != null || Application.Current.Properties["ServiceName"] != "" || Application.Current.Properties["ServiceCost"] != null || Application.Current.Properties["ServiceCost"] != ""|| Application.Current.Properties["ServiceDurationTime"] != null || Application.Current.Properties["ServiceDurationTime"] != ""|| Application.Current.Properties["ServiceBufferTime"] != null || Application.Current.Properties["ServiceBufferTime"] != "")
                {
                    ServiceName.Text = Convert.ToString(Application.Current.Properties["ServiceName"]);
                    ServiceCost.Text = Convert.ToString(Application.Current.Properties["ServiceCost"]);
                    duration.Text = Convert.ToString(Application.Current.Properties["ServiceDurationTime"]);
                    //BufferTime.Text = Convert.ToString(Application.Current.Properties["ServiceBufferTime"]);
                }




                if (todaycollection.Count > 0)
                {
                    int Minutes = Convert.ToInt32(todaycollection[0]) * 60 + Convert.ToInt32(todaycollection[1]);
                    DurationOfService = Minutes + " min";                    
                    duration.Text = DurationOfService;                    
                }

                if (todaycollectionBuffer.Count > 0)
                {
                    int Min = Convert.ToInt32(todaycollectionBuffer[0]) * 60 + Convert.ToInt32(todaycollectionBuffer[1]);
                    string BufferTimeOfService = Min + " min";                    
                    if (Application.Current.Properties["ServiceDurationTime"] != null)
                    {
                        duration.Text = Convert.ToString(Application.Current.Properties["ServiceDurationTime"]);
                    }
                    //BufferTime.Text = BufferTimeOfService;
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            
        }
       

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["ServiceName"] = ServiceName.Text;
            Application.Current.Properties["serviceProfileTitle"] = ServiceName.Text;
            Application.Current.Properties["ServiceCost"] = ServiceCost.Text;
           
            //Application.Current.Properties["ServiceBufferTime"] = BufferTime.Text;
           
            date.IsOpen = !date.IsOpen;

        }
        private void BufferButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["ServiceName"] = ServiceName.Text;
            Application.Current.Properties["serviceProfileTitle"] = ServiceName.Text;
            Application.Current.Properties["ServiceCost"] = ServiceCost.Text;
            Application.Current.Properties["ServiceDurationTime"] = duration.Text;
           
            //buffer.IsOpen = !buffer.IsOpen;
           
        }

        public void AddService()
        {
            try
            {
                if (ServiceName.Text == "" || duration.Text == ""|| ServiceCost.Text == "")
                {
                    DisplayAlert("Success", "All fields is required", "ok");
                    return;
                }                              
                //DisplayAlert("Success", "Cost is required", "ok");
                

                if (duration.Text != "" || ServiceCost.Text != "")
                {
                    string[] ServiceBufferTime = { };
                    string[] ServiceDuration = { };
                    string Duration = duration.Text;
                    //string bufferTime = BufferTime.Text;
                    if (Duration != null)
                    {
                        ServiceDuration = Duration.Split(' ');
                    }
                    //if (bufferTime != null)
                    //{
                    //    ServiceBufferTime = bufferTime.Split(' ');
                    //}

                    var minutes = ServiceDuration[0];
                    var totalMinutes = Convert.ToInt32(minutes);
                    var time = TimeSpan.FromMinutes(totalMinutes);

                    var durationHours = string.Format("{0:00}", (int)time.Hours);
                    var durationMinutes = string.Format("{0:00}", (int)time.Minutes);

                    Service obj = new Service();
                    //obj.Id = 0;
                    obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                    obj.Name = ServiceName.Text;
                    obj.CategoryName = "";
                    obj.CategoryId = 0;
                    //obj.DurationInMinutes = Convert.ToInt32(durationMinutes);
                    obj.DurationInMinutes = Convert.ToInt32(totalMinutes);

                    obj.DurationInHours = 0;
                    //obj.DurationInHours = Convert.ToInt32(durationHours); 

                    obj.Cost = Convert.ToDouble(ServiceCost.Text);
                    obj.Currency = "";
                    obj.Colour = "";
                    //obj.Buffer = Convert.ToInt32(ServiceBufferTime[0]);
                    obj.Buffer = 0;
                    obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                    obj.Description = "";

                    var SerializedData = JsonConvert.SerializeObject(obj);
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AddService";
                    var result = PostData("POST", SerializedData, apiUrl);

                    JObject responsedata = JObject.Parse(result);
                    dynamic responseValue = responsedata["ReturnObject"]["ServiceId"];
                    int ServiceId = Convert.ToInt32(responseValue.Value);
                    if (ServiceId != null)
                    {
                        Application.Current.Properties["ServiceID"] = ServiceId;
                        if (PageName == "ServiceCreateAfterRegistration")
                        {
                            Navigation.PushAsync(new ServiceProviderPage(GetStaff(), "ServiceCreateAfterRegistration"));
                        }
                        else if (PageName == "ServiceCreateAfterLogin")
                        {
                            Navigation.PushAsync(new ServiceProviderPage(GetStaff(), "AddService"));
                        }
                        else if (PageName == "CalenderPage"|| PageName == "CustomerPage"|| PageName == "ActivityPage"|| PageName == "AccountPage")
                        {
                            Navigation.PushAsync(new ServiceProviderPage(GetStaff(), PageName));
                        }
                        //else if (PageName == "CustomerPage")
                        //{
                        //    Navigation.PushAsync(new ServiceProviderPage(GetStaff(), "CustomerPage"));
                        //}
                        //else if (PageName == "ActivityPage")
                        //{
                        //    Navigation.PushAsync(new ServiceProviderPage(GetStaff(), "ActivityPage"));
                        //}
                        //else if (PageName == "AccountPage")
                        //{
                        //    Navigation.PushAsync(new ServiceProviderPage(GetStaff(), "AccountPage"));
                        //}
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";
                var result = PostData(Method, "", Url);
                ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);
                return ListofServiceProviders;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public ObservableCollection<Service> GetService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);
                ObservableCollection<Service> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<Service>>(result);
                return ListofServices;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public void DeleteService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/DeleteService?companyId=" + 0;
            var result = PostData("DELETE", "", apiUrl);
        }

        public void AddCategories()
        {
            Category obj = new Category();
            obj.Id = 0;
            obj.CompanyId = CompanyId;
            obj.Name = "c2";
            obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

            var SerializedData = JsonConvert.SerializeObject(obj);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/CreateCategory";
            var result = PostData("POST", SerializedData, apiUrl);
        }

        public void DeleteCategory()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/DeleteCategory?companyId=" + CompanyId + "&categoryId=70";
            PostData("Delete", "", apiUrl);
        }

        public string UpdateService()
        {
            Service obj = new Service();
            obj.Id = 0;
            obj.CompanyId = Convert.ToInt32(CompanyId);
            obj.Name = "s2";
            obj.CategoryName = "";
            obj.CategoryId = 0;
            obj.DurationInMinutes = 20;
            obj.DurationInHours = 0;
            obj.Cost = 30;
            obj.Currency = "";
            obj.Colour = "";
            obj.Buffer = 10;
            obj.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
            obj.Description = "";

            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/UpdateService";
            var SerializedData = JsonConvert.SerializeObject(obj);
            var result = PostData("POST", SerializedData, apiUrl);
            return result;
        }

        public string AssignCategorytoService(string CompanyId, string SeviceId, string CategoryId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/AssignCategoryToService?companyId=" + CompanyId + "&categoryId=" + CategoryId + "&serviceId=" + SeviceId;
            var result = PostData("PUT", "", apiUrl);
            return result;
        }

        public string DeAllocateCategoryFromService(string CompanyId, string SeviceId, string CategoryId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/DeAllocateCategoryFromService?companyId=" + CompanyId + "&categoryId=" + CategoryId + "&serviceId=" + SeviceId;
            var result = PostData("POST", "", apiUrl);
            return result;
        }

        public string GetCategoriesAssignedToService(string CompanyId, string ServiceId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetCategoriesAssignedToService?companyId=" + CompanyId + "&serviceId=" + ServiceId;
            var result = PostData("GET", "", apiUrl);
            return result;
        }

        public string GetAllServices(string CompanyId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);
            return result;
        }

        public string GetAllServiceForCategory(string CategoryId, string CompanyId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + CategoryId;
                var result = PostData("GET", "", apiUrl);
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string AssignStaffToService(AssignServiceToStaff AssignStaff)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AssignServiceToStaff";
                var json = JsonConvert.SerializeObject(AssignStaff);
                var result = PostData("POST", json, apiUrl);
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeAssignedStaffToService(string CompanyId, string EmployeeId, string ServiceId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/DeAllocateServiceForEmployee?companyId=" + CompanyId + "&employeeId=" + EmployeeId + "&serviceId=" + ServiceId;
                var result = PostData("POST", "", apiUrl);
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string GetEmployeeAssignedtoService(string ServiceId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/GetEmployeeAllocatedToService?serviceId=" + ServiceId;
                var result = PostData("GET", "", apiUrl);
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
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