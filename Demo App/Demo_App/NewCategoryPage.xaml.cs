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
using Newtonsoft.Json.Linq;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCategoryPage : ContentPage
	{               
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);        
        public NewCategoryPage ()
		{
			InitializeComponent ();
          
        }
       
        public ObservableCollection<AssignedServicetoStaff> GetAllServices(string CompanyId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);


                ObservableCollection<AssignedServicetoStaff> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);

                return ListofServices;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public void AddCategory()
        {
            try
            {
                Category obj = new Category();
                obj.Id = 0;
                obj.CompanyId = CompanyId;
                obj.Name = CategoryName.Text;
                obj.CreationDate = "2017-11-08t12:19:27.628z";


                var SerializedData = JsonConvert.SerializeObject(obj);
                var apiurl = Application.Current.Properties["DomainUrl"] + "/api/services/CreateCategory";

                var result = PostData("POST", SerializedData, apiurl);

                JObject responsedata = JObject.Parse(result);
                dynamic ResponseValue = responsedata["ReturnObject"]["CategoryId"];
                int CategoryId = Convert.ToInt32(ResponseValue.Value);
                string categoryName = obj.Name;

                Navigation.PushAsync(new AddServiceToCategoryPage(GetAllServices(CompanyId), CategoryId, categoryName));
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


    }
}