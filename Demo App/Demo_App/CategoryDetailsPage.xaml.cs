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
	public partial class CategoryDetailsPage : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);       
        int CategoryID;
        string NameofCategory;
        ObservableCollection<AssignedServicetoStaff> ListOfAssignService = new ObservableCollection<AssignedServicetoStaff>();       
        public CategoryDetailsPage(int categoryId,string categoryName)
		{
			InitializeComponent ();
            CategoryID = categoryId;
            NameofCategory = categoryName;
            var provider = GetSelectedService();

            var ServiceString = "";
            foreach (var item in provider)
            {
                if (item.isAssigned == false)
                {
                    ServiceString = ServiceString + item.Name + ",";
                }
            }
            if (ServiceString.Length > 0)
            {
                CategoryServices.Text = ServiceString.Substring(0, ServiceString.Length - 1);
            }

            CategoryName.Text = NameofCategory;
        }

        public ObservableCollection<AssignedServicetoStaff> GetSelectedService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + CategoryID;
            var result = PostData("GET", "", apiUrl);

             ListOfAssignService = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
            return ListOfAssignService; 
        }

        public void  SaveCategoryDetails()
        {
            Navigation.PushAsync(new ServiceCategoriesPage(CategoryID));
        }

        public void EditCategoryService()
        {
            var AllServices = GetService();
            foreach( var item in ListOfAssignService)
            {
                foreach(var service in AllServices)
                {
                    if (item.Id == service.Id)
                    {
                        service.isAssigned = true;
                        break;
                    }
                   
                }
            }
            Navigation.PushAsync(new AddServiceToCategoryPage(AllServices, CategoryID, NameofCategory));
        }

        public ObservableCollection<AssignedServicetoStaff>  GetService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);

            ObservableCollection<AssignedServicetoStaff> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);

            return ListofServices;
        }

        public void EditCategory()
        {
            Navigation.PushAsync(new UpdateCategory(CategoryID,NameofCategory));
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