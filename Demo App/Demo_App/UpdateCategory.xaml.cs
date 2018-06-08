using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo_App.Model;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateCategory : ContentPage
    {
        int CategoryId;
        string CategorieName;
		public UpdateCategory (int Id,string Name)
		{
			InitializeComponent ();
            CategoryId = Id;
            CategorieName = Name;
            CategoryName.Text = Name;

        }

        public void UpdateCategories()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/UpdateCategory";

                Category obj = new Category();
                obj.Id = CategoryId;
                obj.CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
                obj.Name = CategoryName.Text;
                obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                var serializeddata = JsonConvert.SerializeObject(obj);

                var result = PostData("POST", serializeddata, apiUrl);

                Navigation.PushAsync(new CategoryDetailsPage(CategoryId, CategoryName.Text,"update"));
            }
            catch (Exception e)
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