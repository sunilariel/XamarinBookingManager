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
            
            InitializeComponent ();
            GetAllCustomer();
          
        }
        public void GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
            CustomersList.ItemsSource = ListOfCustomer;
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
            //Navigation.PushAsync(new CutomerProfilePage(PhoneNumber));
            Application.Current.MainPage.Navigation.PushAsync(new CutomerProfilePage(Cust));
        }
    }
}