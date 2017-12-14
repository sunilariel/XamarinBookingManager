﻿using Demo_App.Model;
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
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public SelectServiceCategory ()
		{
			InitializeComponent ();
            GetCategories(CompanyId);
        }

        public void GetCategories(string Id)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceCategoriesForCompany?companyId=" + Id;
            var result = PostData("GET", "", apiUrl);
            ObservableCollection<Category> ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(result);
            ListofCategoriesData.ItemsSource = ListofAllCategories;
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