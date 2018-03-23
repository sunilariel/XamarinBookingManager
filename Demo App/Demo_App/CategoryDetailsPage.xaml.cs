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


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailsPage : ContentPage
    {
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;
        string NameofCategory;
        ObservableCollection<AssignedServicetoStaff> ListOfAssignService = new ObservableCollection<AssignedServicetoStaff>();
        ObservableCollection<AssignServiceToCategory> ListofAllAssignedServices = new ObservableCollection<AssignServiceToCategory>();
        public CategoryDetailsPage(int categoryId, string categoryName)
        {
            try
            {
                InitializeComponent();
                CategoryID = categoryId;
                NameofCategory = categoryName;
                var provider = GetSelectedService();
                var ServiceString = "";
                foreach (var item in provider)
                {
                    if (item.isAssigned == true)
                    {
                        ServiceString = ServiceString + item.Name + ",";
                    }
                }
                if (ServiceString.Length > 0)
                {
                    CategoryServices.Text = ServiceString.Substring(0, ServiceString.Length - 1);
                }
                CategoryName.Text = NameofCategory;
            }           

            catch (Exception e)
            {
                e.ToString();
            }
        }

        public ObservableCollection<AssignedServicetoStaff> GetSelectedService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + CategoryID;
                var result = PostData("GET", "", apiUrl);
                ListOfAssignService = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
                //ObservableCollection<AssignServiceToCategory> ListofAssignServicesCategories = GetAssignServices();
                foreach (var item in ListOfAssignService)
                {
                    var ApiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetCategoriesAssignedToService?companyId=" + CompanyId + "&serviceId=" + item.Id;
                    var resultData = PostData("GET", "", ApiUrl);
                    ListofAllAssignedServices = JsonConvert.DeserializeObject<ObservableCollection<AssignServiceToCategory>>(resultData);
                }

                foreach (var service in ListofAllAssignedServices)
                {
                    foreach (var assignService in ListOfAssignService)
                    {
                        if (service.Id == assignService.Id)
                        {
                            assignService.isAssigned = true;
                        }
                    }
                }
                return ListOfAssignService;



                //var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceCategoriesForCompany?companyId=" + CompanyId;
                //var result = PostData("GET", "", apiUrl);
                //ObservableCollection<Category> ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(result);
                //foreach (var item in ListofAllCategories)
                //{
                //    var ApiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + item.Id;
                //    var resultData = PostData("GET", "", ApiUrl);
                //    ObservableCollection<Service> ListOfAssignService = JsonConvert.DeserializeObject<ObservableCollection<Service>>(resultData);

                //    ServicesAllocatedToCategory AllocateServices = new ServicesAllocatedToCategory();
                //    AllocateServices.CategoryName = item.Name;
                //    AllocateServices.CategoryId = item.Id;
                //    AllocateServices.AllocatedServiceCount = ListOfAssignService.Count + "services";

                //    ListOfAssignServiceCount.Add(AllocateServices);
                //}
                //ListofCategoriesData.ItemsSource = ListOfAssignServiceCount;
                //BindingContext = ListOfAssignServiceCount;



            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveCategoryDetails()
        {

            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }

            //Navigation.PopAsync(true);           

            Navigation.PushAsync(new ServiceCategoriesPage(CategoryID));

            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 3)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }



        }
        //public ObservableCollection<AssignServiceToCategory> GetAssignServices()
        //{
        //    try
        //    {
        //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetCategoriesAssignedToService?companyId=" + CompanyId+ "&serviceId" + ;
        //        var result = PostData("GET", "", apiUrl);
        //        ListofAllAssignedServices = JsonConvert.DeserializeObject<ObservableCollection<AssignServiceToCategory>>(result);
        //        return ListofAllAssignedServices;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        public void EditCategoryService()
        {                   
            var AllServices = GetService();
            foreach (var item in ListOfAssignService)
            {
                foreach (var service in AllServices)
                {
                    if (item.Id == service.Id)
                    {
                        service.isAssigned = true;
                        break;
                    }

                }
            }
            Navigation.PushAsync(new AddServiceToCategoryPage(AllServices, CategoryID, NameofCategory));
        }

        public ObservableCollection<AssignedServicetoStaff> GetService()
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

        public void EditCategory()
        {
            Navigation.PushAsync(new UpdateCategory(CategoryID, NameofCategory));
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