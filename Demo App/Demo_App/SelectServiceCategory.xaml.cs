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
        #region GloblesFields
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<ServicesAllocatedToCategory> ListOfAssignServiceCount = new ObservableCollection<ServicesAllocatedToCategory>();
        public ServicesAllocatedToCategory serviceCount = null;
        public Customer objCust = null;
        string PageName = "";
        #endregion

        public SelectServiceCategory(string pagename)
        {
            InitializeComponent();
            PageName = pagename;
            GetCategories(CompanyId);
            BindingContext = serviceCount;
        }

        public void GetSelectedCustomerById()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetCustomerById?id=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objCust = JsonConvert.DeserializeObject<Customer>(result);
            }
            catch (Exception e)
            {

            }

        }

        private void SelectServiceForCustomerClick(object sender, SelectedItemChangedEventArgs e)
        {

            var Category = e.SelectedItem as ServicesAllocatedToCategory;
            Application.Current.Properties["CategoryID"] = Category.CategoryId;
            Navigation.PushAsync(new SelectServicesForAppontment(PageName));
        }

        public void GetCategories(string Id)
        {
            try
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
            catch (Exception e)
            {
                e.ToString();
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