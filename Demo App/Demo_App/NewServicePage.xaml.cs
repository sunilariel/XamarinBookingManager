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
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<AssignProvider> ListofServiceProviders = new ObservableCollection<AssignProvider>();

        public NewServicePage ()
		{
			InitializeComponent ();          
        }

        private void SelectServiceProvider(object sender, EventArgs args)
        {
        //    Navigation.PushAsync(new ServiceProviderPage());
        }
       
        public void AddService()
        {           
            Service obj = new Service();
            obj.Id = 0;
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.Name = ServiceName.Text;
            obj.CategoryName = "";
            obj.CategoryId = 0;
            obj.DurationInMinutes = Convert.ToInt32(ServiceDuration.Time.TotalMinutes);
            obj.DurationInHours = 0;
            obj.Cost = Convert.ToDouble(ServiceCost.Text);
            obj.Currency = "";
            obj.Colour = "";
            obj.Buffer = Convert.ToInt32(ServiceBufferTime.Time.TotalMinutes);
            obj.CreationDate = "2017-11-08T12:19:27.628Z";
            obj.Description = "";

            var SerializedData = JsonConvert.SerializeObject(obj);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AddService";
            var result = PostData("POST", SerializedData, apiUrl);

            JObject responsedata = JObject.Parse(result);
            dynamic responseValue = responsedata["ReturnObject"]["ServiceId"];
            int ServiceId = Convert.ToInt32(responseValue.Value);
            if(ServiceId != null)
            {
                // Navigation.PushAsync(new ServiceProviderPage(ServiceId));               
                Navigation.PushAsync(new ServiceProviderPage(GetStaff(),ServiceId, "AddService"));
            }          
            //Navigation.PushAsync(new ServicePage());
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
            var Method = "GET";
            var result = PostData(Method, "", Url);
            ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);
            return ListofServiceProviders;
        }

        public ObservableCollection<Service> GetService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);
            ObservableCollection<Service> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<Service>>(result);
            return ListofServices;
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
            obj.CreationDate = "2017-11-08T12:19:27.628Z";
            obj.EntityStatus = "0";

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
            obj.CreationDate = "2017-11-08T12:19:27.628Z";
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
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + CategoryId;
            var result = PostData("GET", "", apiUrl);
            return result;
        }

        public string AssignStaffToService(AssignServiceToStaff AssignStaff)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AssignServiceToStaff";
            var json = JsonConvert.SerializeObject(AssignStaff);
            var result = PostData("POST", json, apiUrl);
            return result;
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