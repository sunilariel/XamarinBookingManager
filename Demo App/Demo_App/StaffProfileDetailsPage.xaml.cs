using Demo_App.Model;
using System;
using System.Collections.Generic;
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
	public partial class StaffProfileDetailsPage : ContentPage
	{
        public int StaffId;
        public StaffProfileDetailsPage (Staff staff)
		{
            BindingContext = staff;
            StaffId = staff.Id;
            InitializeComponent ();
		}

        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
        private void BreaksClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BreaksPage());
        }
        private void WorkingDaysClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BusinessHoursPage(StaffId));
        }
        private void ServiceProvidedClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new SelectServiceProviderPage());
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && CustomerProfile.HeightRequest > 30 && CustomerProfile.HeightRequest < 151)
            {
                CustomerProfile.HeightRequest = CustomerProfile.HeightRequest + e.TotalY;
                if (CustomerProfile.HeightRequest < 31)
                    CustomerProfile.HeightRequest = 31;
                if (CustomerProfile.HeightRequest > 150)
                    CustomerProfile.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }

        public void DeleteStaff(int Id)
        {

            var CompanyId = Application.Current.Properties["CompanyId"];
            var Method = "DELETE";
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/DeleteStaff?id=" + StaffId;
            var result= PostData(Method, null, Url);
            Navigation.PushAsync(new StaffPage());

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

                if (SerializedData != null)
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