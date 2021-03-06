﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceDetailsPage : ContentPage
    {
        #region GlobleFields
        ObservableCollection<object> todaycollectionBuffer = new ObservableCollection<object>();
        ObservableCollection<object> todaycollection = new ObservableCollection<object>();
        string CompanyId = Application.Current.Properties["CompanyId"].ToString();
        string ServiceId;
        public ServiceDetails service = null;
        #endregion



        public ServiceDetailsPage()
        {
            try
            {
                InitializeComponent();
                var Servicedata = GetSelectedService();
                ServiceId = Application.Current.Properties["ServiceID"].ToString();
                service = new ServiceDetails();
                service.Id = Servicedata.Id;
                var dHour = Servicedata.DurationInHours * 60;
                var dMinute = Servicedata.DurationInMinutes + dHour;
                service.DurationInMinutes = dMinute + " " + "min";

                //service.DurationInMinutes = Servicedata.DurationInHours + " " + "hrs" + " " + Servicedata.DurationInMinutes + " " + "min";
                service.BufferTimeInMinutes = Servicedata.Buffer + " " + "min";
                service.Cost = "$" + Servicedata.Cost;
                service.Name = Servicedata.Name;
                var category = GetCategoriesAssignedtoService();
                var CategoryString = "";
                foreach (var item in category)
                {
                    if (item.Confirmed == true)
                    {
                        CategoryString = CategoryString + item.Name + ",";
                    }
                }
                if (CategoryString.Length > 0)
                {
                    service.Categories = CategoryString.Substring(0, CategoryString.Length - 1);
                }
                else
                {
                    service.Categories = "No Category";
                }

                var provider = GetServiceProvider();

                var ProviderString = "";
                foreach (var item in provider)
                {
                    if (item.confirmed == true)
                    {
                        ProviderString = ProviderString + item.FirstName + ",";
                    }
                }
                if (ProviderString.Length > 0)
                {
                    service.ServiceProviders = ProviderString.Substring(0, ProviderString.Length - 1);
                }
                else
                {
                    service.Categories = "No Service";
                }
                BindingContext = service;
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        private void PrivateServiceToggle(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                lblPrivateService.Text = "On";
                //lblPrivateService.TextColor = Color.Black;
            }
            else
            {
                lblPrivateService.Text = "Off";
                //lblPrivateService.TextColor = Color.Black;
            }
        }

        public ObservableCollection<AssignCategory> GetCategoriesAssignedtoService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetCategoriesAssignedToService?companyId=" + CompanyId + "&serviceId=" + ServiceId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<AssignCategory> ListofAssignedCategories = JsonConvert.DeserializeObject<ObservableCollection<AssignCategory>>(result);

                ObservableCollection<AssignCategory> ListofCategories = GetCategories();

                foreach (var category in ListofCategories)
                {
                    foreach (var assigncategory in ListofAssignedCategories)
                    {
                        if (category.Id == assigncategory.Id)
                        {
                            category.Confirmed = true;
                        }
                    }
                }
                return ListofCategories;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public Service GetSelectedService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetServiceById?id=" + Application.Current.Properties["ServiceID"];

                var result = PostData("GET", "", apiUrl);

                Service ServiceDetail = JsonConvert.DeserializeObject<Service>(result);

                return ServiceDetail;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public ObservableCollection<AssignProvider> GetServiceProvider()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetEmployeeAllocatedToService?serviceId=" + ServiceId;

                var result = PostData("GET", "", apiUrl);

                ObservableCollection<AssignProvider> ListOfAssignProvider = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);

                ObservableCollection<AssignProvider> ListofProvider = GetStaff();

                foreach (var provider in ListofProvider)
                {
                    foreach (var AssignProvider in ListOfAssignProvider)
                    {
                        if (provider.Id == AssignProvider.Id)
                        {
                            provider.confirmed = true;
                        }
                    }
                }

                return ListofProvider;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public ObservableCollection<AssignCategory> GetCategories()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetServiceCategoriesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);
                ObservableCollection<AssignCategory> ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<AssignCategory>>(result);

                return ListofAllCategories;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, "", Url);

                ObservableCollection<AssignProvider> ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);


                return ListofServiceProviders;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        private void EditServiceCost(object sender, EventArgs args)
        {
            // Navigation.PushAsync(new NewServicePage());
            Navigation.PushAsync(new EditServiceDetails(service, todaycollection, todaycollectionBuffer));
        }
        private void EditService(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditServiceDetails(service, todaycollection, todaycollectionBuffer));
            
        }
        private void EditCategories(object sender, EventArgs args)
        {
            // Service service = new Service();
            Navigation.PushAsync(new ChooseCategoriesPage("",GetCategoriesAssignedtoService()));
        }
        private void SetnewDuration(object sender, EventArgs args)
        {

            Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, ""));
        }
        private void EditServiceProvider(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ServiceProviderPage(GetServiceProvider(), "EditService"));
        }
        private void AddNotes(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new AddNotesPage());
        }

        private void CreateService()
        {
            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

            }

            //Navigation.PopAsync(true);

            Navigation.PushAsync(new ServicePage());


            int pCount = Navigation.NavigationStack.Count();

            for (int i = 0; i < pCount; i++)
            {
                if (i == 3)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }
        }

        //CreateService
        public async void DeleteService()
        {

            try
            {

                var confirmed = await DisplayAlert("Confirm", "Are you sure You want to delete this Service", "Yes", "No");
                if (confirmed)
                {
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeleteService?companyId=" + ServiceId;
                    var result = PostData("DELETE", "", apiUrl);

                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 3; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

                    }

                    //Navigation.PopAsync(true);

                    await Navigation.PushAsync(new ServicePage());


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
                    //await Navigation.PushAsync(new ServiceDetailsPage());
                }


            }
            catch (Exception e)
            {
                e.ToString();
            }




            //try
            //{                

            //        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeleteService?companyId=" + ServiceId;
            //    var result = PostData("DELETE", "", apiUrl);
            //}
            //catch (Exception e)
            //{
            //    e.ToString();
            //}

            //for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
            //{
            //    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

            //}

            ////Navigation.PopAsync(true);

            //Navigation.PushAsync(new ServicePage());


            //int pCount = Navigation.NavigationStack.Count();

            //for (int i = 0; i < pCount; i++)
            //{
            //    if (i == 3)
            //    {
            //        Navigation.RemovePage(Navigation.NavigationStack[i]);
            //    }
            //}
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
                httpRequest.ContentLength = 0;

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



        private void CrossClick()
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    var result = await this.DisplayAlert("Alert", "Do you realy want to exit", "Yes", "No");
            //    if (result)
            //    {
                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

                    }
                    Navigation.PushAsync(new ServicePage());
                    int pCount = Navigation.NavigationStack.Count();
                    for (int i = 0; i < pCount; i++)
                    {
                        if (i == 3)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[i]);
                        }
                    }
            //    }

            //});
        }
        protected override bool OnBackButtonPressed()
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    var result = await this.DisplayAlert("Alert", "Do you realy want to exit", "Yes", "No");
            //    if (result)
            //    {
                    for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
                    }
                    Navigation.PushAsync(new ServicePage());
                    int pCount = Navigation.NavigationStack.Count();
                    for (int i = 0; i < pCount; i++)
                    {
                        if (i == 3)
                        {
                            Navigation.RemovePage(Navigation.NavigationStack[i]);
                        }
                    }
            //    }

            //});
            return true;



        }


    }
}