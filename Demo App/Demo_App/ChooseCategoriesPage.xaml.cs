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
        #region GlobleFields
        public int ServiceId;
        string PAgeName;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<AssignCategory> ListofAllCategories = new ObservableCollection<AssignCategory>();
        public Service ServiceDetail;
        #endregion

        public ChooseCategoriesPage (string pageName,ObservableCollection<AssignCategory> ListofCategory)
		{
            try
            {
                InitializeComponent();
                ListofAllCategories = ListofCategory;
                PAgeName = pageName;
                ServiceId = Convert.ToInt32(Application.Current.Properties["ServiceID"]);
                CategoriesData.ItemsSource = ListofAllCategories;
            }
            catch(Exception e)
            {
                e.ToString();
            }

        }
        
        private void AssignCategorytoService(object sender, EventArgs args)
        {
            try
            {
                foreach (var item in ListofAllCategories)
                {
                    if (item.Confirmed == true)
                    {
                       
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/AssignCategoryToService?companyId=" + CompanyId + "&categoryId=" + item.Id + "&serviceId=" + ServiceId;

                        var result = PostData("PUT", "", apiUrl);
                    }
                    else
                    {
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeAllocateCategoryFromService?companyId=" + CompanyId + "&categoryId=" + item.Id + "&serviceId=" + ServiceId;

                        var result = PostData("POST", "", apiUrl);
                    }
                }
            }
            catch(Exception e)
            {
                e.ToString();

            }
            if (PAgeName== "CalenderPage" || PAgeName== "CustomerPage" || PAgeName== "ActivityPage" || PAgeName== "AccountPage")
            {
                Application.Current.Properties.Remove("ServiceName");
                Navigation.PushAsync(new SetAppointmentPage(PAgeName));
            }
            else
            {
                for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

                }
                Navigation.PushAsync(new ServicePage());


                int pCount = Navigation.NavigationStack.Count();

                for (int i = 0; i < pCount; i++)
                {
                    if (i == 3)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[i]);
                    }
                }
            }

            
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