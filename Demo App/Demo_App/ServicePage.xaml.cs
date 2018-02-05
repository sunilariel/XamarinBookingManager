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
	public partial class ServicePage : ContentPage
	{
        string result = "";
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);

        public ServicePage ()
		{
			InitializeComponent ();
            GetService();
        }

        //Get Token//
        //private string GetToken()
        //{
        //    var token = "";
        //    if (Application.Current.Properties.ContainsKey("Token"))
        //    {
        //        token = Convert.ToString(Application.Current.Properties["Token"]);

        //    }
        //    return token;
        //}

        public void AddServiceNavigation()
        {
            try
            {
                Application.Current.Properties["ServiceName"] = null;
                Application.Current.Properties["ServiceDurationTime"] = null;
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                ObservableCollection<object> todaycollectionBuffer = new ObservableCollection<object>();
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer));
            }
            catch(Exception e)
            {
                e.ToString();
            }
           
        }


        public void GetService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<Service> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<Service>>(result);
                ListofAllServices.ItemsSource = ListofServices;
            }
            catch(Exception e)
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

        private void EditServiceClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var Servicedata = e.SelectedItem as Service;
                Application.Current.Properties["ServiceID"] = Servicedata.Id;
                Navigation.PushAsync(new ServiceDetailsPage());
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
    }
}