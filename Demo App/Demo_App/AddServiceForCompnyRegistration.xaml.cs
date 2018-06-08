using Demo_App.Model;
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
	public partial class AddServiceForCompnyRegistration : ContentPage
	{
        string result = "";
        string pagename;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public AddServiceForCompnyRegistration (string pagename)
		{
			InitializeComponent ();
            GetService();
        }
        private void AddNewService(object sender,EventArgs ex)
        {
            try
            {
                Application.Current.Properties["ServiceName"] = null;
                Application.Current.Properties["ServiceDurationTime"] = null;
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                ObservableCollection<object> todaycollectionBuffer = new ObservableCollection<object>();
                
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "ServiceCreateAfterRegistration"));
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        private void NextClick(object sender, EventArgs e)
        {
            for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 1; PageIndex--)
            {
                Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);
            }
            Navigation.PushAsync(new SetAppointmentPage("","",""));
        }

        public void GetService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<Service> ListofServices = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Service>>(result);
                ListofAllServices.ItemsSource = ListofServices;
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

        private void EditServiceClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var Servicedata = e.SelectedItem as Service;
                Application.Current.Properties["ServiceID"] = Servicedata.Id;
                Navigation.PushAsync(new ServiceDetailsPage());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}