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
	public partial class ChooseCategoriesPage : ContentPage
	{
        public int ServiceId;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<AssignCategory> ListofAllCategories = new ObservableCollection<AssignCategory>();
        public Service ServiceDetail;
        public ChooseCategoriesPage (ObservableCollection<AssignCategory> ListofCategory,int serviceId)
		{
                 
            InitializeComponent ();
            ListofAllCategories = ListofCategory;
            ServiceId = serviceId;
            CategoriesData.ItemsSource = ListofAllCategories;

        }

        
        private void AssignCategorytoService(object sender, EventArgs args)
        {
            foreach( var item in ListofAllCategories)
            {
                if (item.Confirmed == true)
                {
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/AssignCategoryToService?companyId=" + CompanyId + "&categoryId=" + item.Id + "&serviceId=" + ServiceId;

                   var result= PostData("PUT", "", apiUrl);
                }
                else
                {
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeAllocateCategoryFromService?companyId=" + CompanyId + "&categoryId=" + item.Id + "&serviceId=" + ServiceId;

                    var result = PostData("POST", "", apiUrl);
                }
            }         
           Navigation.PushAsync(new ServiceDetailsPage(GetSelectedService()));
        }

        public Service GetSelectedService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetServiceById?id=" + ServiceId;

           var result= PostData("GET", "", apiUrl);

            Service ServiceDetail = JsonConvert.DeserializeObject<Service>(result);

            return ServiceDetail;
        }


        private void AddNewCategory(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewCategoryPage());
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
                httpRequest.ContentLength = 0;

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