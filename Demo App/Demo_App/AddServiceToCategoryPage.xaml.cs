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
	public partial class AddServiceToCategoryPage : ContentPage
	{
        #region GlobleFields
        ObservableCollection<AssignedServicetoStaff> ListofAllService = new ObservableCollection<AssignedServicetoStaff>();
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;
        string CategoryName = "";
        #endregion

        public AddServiceToCategoryPage (ObservableCollection<AssignedServicetoStaff> ListofServices,int  CategoryId,string categoryName)
		{
			InitializeComponent();
            ListofAllService = ListofServices;
            CategoryID = CategoryId;
            CategoryName = categoryName.ToString();
            ListofAllServiceData.ItemsSource = ListofAllService;
        }
     
        public void AddServicestoCategory()
        {
            try
            {
                foreach (var item in ListofAllService)
                {
                    if (item.isAssigned == true)
                    {
                        var Url = Application.Current.Properties["DomainUrl"] + "api/services/AssignCategoryToService?companyId=" + CompanyId + "&categoryId=" + CategoryID + "&serviceId=" + item.Id;
                        //AssignServiceToCategory obj = new AssignServiceToCategory();
                        //obj.CompanyId = Convert.ToInt32(CompanyId);
                        //obj.Id = item.Id;
                        //obj.CreationDate = DateTime.Now.ToString();
                        //var SerializedData = JsonConvert.SerializeObject(obj);
                        var result = PostData("PUT", "", Url);
                    }
                    else
                    {
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/DeAllocateCategoryFromService?companyId=" + CompanyId + "&categoryId=" + CategoryID + "&serviceId=" + item.Id;
                        var result = PostData("POST", "", apiUrl);
                    }
                }

                Navigation.PushAsync(new CategoryDetailsPage(CategoryID, CategoryName));
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public void GetService(string CompanyId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                List<Service> ListofServices = JsonConvert.DeserializeObject<List<Service>>(result);
                ListofAllServiceData.ItemsSource = ListofServices;
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