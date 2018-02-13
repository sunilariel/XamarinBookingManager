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
using System.Configuration;
using System.Globalization;
using System.Collections.ObjectModel;


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffPage : ContentPage
    {
        #region GlobleFields
        ObservableCollection<Staff> ListofAllStaff = new ObservableCollection<Staff>();
        #endregion
        public StaffPage()
        {
            InitializeComponent();
           
            var Stafflist = GetStaff();
            if (Stafflist.Count > 5)
            {
                StaffSearchBar.IsVisible = true;
            }
        }

        private void NewStaffClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewStaffPage("StaffCreateAfterLogin"));
           // Navigation.PushAsync(new BusinessHoursPage()); 

        }

        public ObservableCollection<Staff> GetStaff()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, "", Url);

                ListofAllStaff = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(result);
                ListofStaffData.ItemsSource = ListofAllStaff;
                return ListofAllStaff;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        private void SearchStaffByTerm(object sender, TextChangedEventArgs e)
        {
            try
            {
                //thats all you need to make a search                 
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    ListofStaffData.ItemsSource = ListofAllStaff;
                }

                else
                {
                    var listfilter = ListofAllStaff.Where(x => x.FirstName.ToLower().StartsWith(e.NewTextValue)).ToList();

                    ListofStaffData.ItemsSource = listfilter;


                }
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

        private void ServiceprofileDetailClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var staff = e.SelectedItem as Staff;
                Application.Current.Properties["SelectedEmployeeID"] = staff.Id;
                Navigation.PushAsync(new StaffProfileDetailsPage());
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
    }
}