﻿using System;
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

        public StaffPage()
        {
            InitializeComponent();
            GetStaff();                   
        }

        private void NewStaffClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewStaffPage());
           // Navigation.PushAsync(new BusinessHoursPage()); 

        }

        public void GetStaff()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
            var Method = "GET";

            var result = PostData(Method, "", Url);

            ObservableCollection<Staff> ListofAllStaff = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(result);
            ListofStaffData.ItemsSource = ListofAllStaff;

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
            var staff = e.SelectedItem as Staff;
            Navigation.PushAsync(new StaffProfileDetailsPage(staff));
        }
    }
}