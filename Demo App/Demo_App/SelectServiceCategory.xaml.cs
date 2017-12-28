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
	public partial class SelectServiceCategory : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;
        ObservableCollection<ServicesAllocatedToCategory> ListOfAssignServiceCount = new ObservableCollection<ServicesAllocatedToCategory>();
        public ServicesAllocatedToCategory serviceCount = null;
        public Customer objCust = null;
        public SelectServiceCategory (int CategoryId,Customer Cust)
		{
			InitializeComponent ();
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            CategoryID = CategoryId;
            GetCategories(CompanyId);
            BindingContext = serviceCount;
        }

        private void SelectServiceForCustomerClick(object sender,SelectedItemChangedEventArgs e)
        {
            
                var Category = e.SelectedItem as ServicesAllocatedToCategory;           
                Navigation.PushAsync(new SelectServicesForAppontment(Category.CategoryId, objCust));
        }

        public void GetCategories(string Id)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceCategoriesForCompany?companyId=" + Id;
            var result = PostData("GET", "", apiUrl);
            ObservableCollection<Category> ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(result);
            foreach (var item in ListofAllCategories)
            {
                var ApiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + item.Id;
                var resultData = PostData("GET", "", ApiUrl);
                ObservableCollection<Service> ListOfAssignService = JsonConvert.DeserializeObject<ObservableCollection<Service>>(resultData);

                ServicesAllocatedToCategory AllocateServices = new ServicesAllocatedToCategory();
                AllocateServices.CategoryName = item.Name;
                AllocateServices.CategoryId = item.Id;
                AllocateServices.AllocatedServiceCount = ListOfAssignService.Count + " services";

                ListOfAssignServiceCount.Add(AllocateServices);
            }
            ListofCategoriesData.ItemsSource = ListOfAssignServiceCount;
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