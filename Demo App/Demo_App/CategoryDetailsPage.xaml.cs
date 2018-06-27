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
        string btnname = "";
        ObservableCollection<AssignedServicetoStaff> ListOfAssignService = new ObservableCollection<AssignedServicetoStaff>();
        ObservableCollection<AssignServiceToCategory> ListofAllAssignedServices = new ObservableCollection<AssignServiceToCategory>();
        public CategoryDetailsPage(int categoryId, string categoryName, string btnName)
        {
            try
            {
                InitializeComponent();
                btnname = btnName;
                ToolbarItems.Remove(saveButton);
                ToolbarItems.Remove(UpdateButton);
                ToolbarItems.Remove(DeleteButton);
                if (btnname == "delete")
                {
                    ToolbarItems.Remove(saveButton);
                    ToolbarItems.Remove(UpdateButton);
                    ToolbarItems.Add(DeleteButton);
                }
                else if (btnname == "update")
                {
                    ToolbarItems.Remove(saveButton);
                    ToolbarItems.Add(UpdateButton);
                    ToolbarItems.Remove(DeleteButton);
                }
                else
                {
                    ToolbarItems.Add(saveButton);
                    ToolbarItems.Remove(UpdateButton);
                    ToolbarItems.Remove(DeleteButton);
                }
                CategoryID = categoryId;
                NameofCategory = categoryName;
                var provider = GetSelectedService();
                var ServiceString = "";
                foreach (var item in provider)
                {
                    if (item.isAssigned == true)
                    {
                        ServiceString = ServiceString + item.Name + " , ";
                    }
                }
                if (ServiceString.Length > 0)
                {
                    CategoryServices.Text = ServiceString.Remove(ServiceString.Length - 2);
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
                //foreach (var item in ListOfAssignService)
                //{
                //    var ApiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetCategoriesAssignedToService?companyId=" + CompanyId + "&serviceId=" + item.Id;
                //    var resultData = PostData("GET", "", ApiUrl);
                //    ListofAllAssignedServices = JsonConvert.DeserializeObject<ObservableCollection<AssignServiceToCategory>>(resultData);
                //}

                //foreach (var service in ListofAllAssignedServices)
                //{
                foreach (var assignService in ListOfAssignService)
                {
                    //if (service.Id == assignService.Id)
                    //{
                    assignService.isAssigned = true;
                    //}
                }
                //}
                return ListOfAssignService;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
        public async void DeleteCategoryDetails()
        {
            try
            {
                var confirmed = await DisplayAlert("Confirm", "Are you sure You want to delete this Category", "Yes", "No");
                if (confirmed)
                {
                    string apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeleteCategory?companyId=" + CompanyId + "&categoryId=" + CategoryID;

                    var result = PostData("DELETE", "", apiUrl);

                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 3; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                    }

                    //Navigation.PopAsync(true);           

                   await Navigation.PushAsync(new ServiceCategoriesPage(CategoryID));

                    int pCount = Navigation.NavigationStack.Count();

                    for (int i = 0; i < pCount; i++)
                    {
                        if (i == 2)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[i]);
                        }
                    }

                }
                else
                {
                    //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    //await Navigation.PushAsync(new CategoryDetailsPage(CategoryID, NameofCategory, btnname));
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public void UpdateCategoryDetails()
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
            Navigation.PushAsync(new AddServiceToCategoryPage(AllServices, CategoryID, NameofCategory, "update"));
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
                e.ToString();
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