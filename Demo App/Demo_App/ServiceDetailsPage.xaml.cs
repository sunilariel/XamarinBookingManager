using System;
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
        string CompanyId = Application.Current.Properties["CompanyId"].ToString();
        string ServiceId;
        public ServiceDetails service = null;
        public ServiceDetailsPage (Service Servicedata)
		{
			InitializeComponent ();
            ServiceId = (Servicedata.Id).ToString();
             service = new ServiceDetails();
            service.Id = Servicedata.Id;
            service.DurationInMinutes =  Servicedata.DurationInMinutes + " " + "min";
            service.BufferTimeInMinutes = Servicedata.Buffer + " " + "min";
            service.Cost = "$" + Servicedata.Cost;
            service.Name = Servicedata.Name;
            var category = GetCategoriesAssignedtoService();
            var CategoryString = "";
            foreach( var item in category)
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
            foreach( var item in provider)
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
            BindingContext = service;
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
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetCategoriesAssignedToService?companyId=" + CompanyId + "&serviceId=" + ServiceId;
            var result = PostData("GET", "", apiUrl);

            ObservableCollection<AssignCategory> ListofAssignedCategories = JsonConvert.DeserializeObject<ObservableCollection<AssignCategory>>(result);

            ObservableCollection<AssignCategory> ListofCategories = GetCategories();

           foreach ( var category in ListofCategories)
            {
                foreach( var assigncategory in ListofAssignedCategories)
                {
                    if(category.Id== assigncategory.Id)
                    {
                        category.Confirmed = true;
                    }
                }
            }
            return ListofCategories;           
        }


        public ObservableCollection<AssignProvider> GetServiceProvider()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetEmployeeAllocatedToService?serviceId=" + ServiceId;

            var result = PostData("GET", "", apiUrl);

            ObservableCollection<AssignProvider> ListOfAssignProvider = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);

            ObservableCollection<AssignProvider> ListofProvider = GetStaff();

            foreach(var provider in ListofProvider)
            {
                foreach( var AssignProvider in ListOfAssignProvider)
                {
                    if (provider.Id == AssignProvider.Id)
                    {
                        provider.confirmed = true;
                    }
                }
            }

            return ListofProvider;
        }

        public ObservableCollection<AssignCategory> GetCategories()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetServiceCategoriesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);
          ObservableCollection<AssignCategory>  ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<AssignCategory>>(result);

            return ListofAllCategories;
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {

            var Url = Application.Current.Properties["DomainUrl"] + "api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
            var Method = "GET";

            var result = PostData(Method, "", Url);

            ObservableCollection<AssignProvider> ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);


            return ListofServiceProviders;
        }

        private void Setnewcost(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewServicePage());
        }
        private void EditService(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditServiceDetails(service));
        }
        private void EditCategories(object sender, EventArgs args)
        {
           // Service service = new Service();
            Navigation.PushAsync(new ChooseCategoriesPage(GetCategoriesAssignedtoService(),Convert.ToInt32(ServiceId)));
        }
        private void SetnewDuration(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewServicePage());
        }
        private void EditServiceProvider(object sender, EventArgs args)
        {           
            Navigation.PushAsync(new ServiceProviderPage(GetServiceProvider(), Convert.ToInt32(ServiceId),"EditService"));
        }
        private void AddNotes(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddNotesPage());
        }

        private void DeleteService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeleteService?companyId=" + ServiceId;
            var result = PostData("DELETE", "", apiUrl);

            Navigation.PushAsync(new ServicePage());
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

    }
}