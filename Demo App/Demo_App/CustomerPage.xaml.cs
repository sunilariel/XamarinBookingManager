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
	public partial class CustomerPage : ContentPage
	{
          
        public CustomerPage ()
		{           
                NavigationPage.SetHasBackButton(this, false);
                NavigationPage.SetBackButtonTitle(this, "Customer");
                InitializeComponent();
                var customerlist = GetAllCustomer();
                if (customerlist.Count > 5)
                {
                    CustomerSearchBar.IsVisible = true;
                }           
          
        }
        public ObservableCollection<Customer> GetAllCustomer()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
                var result = PostData("GET", "", apiURL);
                ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                CustomersList.ItemsSource = ListOfCustomer;
                return ListOfCustomer;
            }
            catch(Exception e)
            {
                return null;
            }
        }
       
        public void SearchCustomersByTerm()
        {
            try
            {
                var result = "";
                string apiUrl = Application.Current.Properties["DomainUrl"] + "api/customer/SearchCustomersByTerm?companyId=" + Application.Current.Properties["CompanyId"] + "&searchTerm=" + CustomerSearchBar.Text;
                var httpWebRequest = HttpWebRequest.Create(apiUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                var httpResponse = httpWebRequest.GetResponse();
                using (var StreamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = StreamReader.ReadToEnd();
                }
                ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                CustomersList.ItemsSource = ListOfCustomer;
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

        private void CustomerProfileClick(object sender, SelectedItemChangedEventArgs e)
        {
            var Cust = e.SelectedItem as Customer;
            Application.Current.Properties["SelectedCustomerId"] = Cust.Id;
            Notes obj =new Notes();
            Application.Current.MainPage.Navigation.PushAsync(new CutomerProfilePage(Cust, obj));
        }
    }
}