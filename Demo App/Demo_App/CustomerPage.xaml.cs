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
        public AddAppointments objaddAppointment = null;
        //string aa;
        public CustomerPage()
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
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        private void AddNewCustomer(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewCustomerPage(objaddAppointment, "selectedPageCustomer"));

        }

        public void SearchCustomersByTerm()
        {
            try
            {
                if (CustomerSearchBar.Text == "")
                {
                    string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
                    var result = PostData("GET", "", apiURL);
                    ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                    CustomersList.ItemsSource = ListOfCustomer;
                }
                else
                {
                    string apiURL = Application.Current.Properties["DomainUrl"] + "api/customer/SearchCustomersByTerm?companyId=" + Application.Current.Properties["CompanyId"] + "&searchTerm=" + CustomerSearchBar.Text;

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));


                    string result = "";

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                    CustomersList.ItemsSource = ListOfCustomer;
                }

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
            if (e.SelectedItem==null)           
                return;            
            var Cust = e.SelectedItem as Customer;
            Application.Current.Properties["SelectedCustomerId"] = Cust.Id;
            Application.Current.MainPage.Navigation.PushAsync(new CutomerProfilePage());
            ((ListView)sender).SelectedItem=null;
        }
    }
}