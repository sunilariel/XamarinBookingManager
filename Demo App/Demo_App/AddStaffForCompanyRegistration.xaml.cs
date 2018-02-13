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
	public partial class AddStaffForCompanyRegistration : ContentPage
	{
		public AddStaffForCompanyRegistration ()
		{
			InitializeComponent ();
            GetStaff();
        }
        private void NewStaffClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewStaffPage("StaffCreateAfterRegistration"));           
        }

        private void NextClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new SetAppointmentPage());
        }

        public void GetStaff()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, "", Url);

                ObservableCollection<Staff> ListofAllStaff = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(result);
                ListofStaffData.ItemsSource = ListofAllStaff;
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        private void ServiceprofileDetailClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var staff = e.SelectedItem as Staff;
                Application.Current.Properties["SelectedEmployeeID"] = staff.Id;
                Navigation.PushAsync(new StaffProfileDetailsPage());
            }
            catch (Exception ex)
            {
                ex.ToString();
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