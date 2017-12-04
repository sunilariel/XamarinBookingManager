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
	public partial class NewCategoryPage : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);

        public NewCategoryPage ()
		{
			InitializeComponent ();
          
        }
        private void AddServiceToCategory(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddServiceToCategoryPage());
        }

        public void AddCategory()
        {
            //category obj = new category();
            //obj.id = 0;
            //obj.companyid = companyid;
            //obj.name = categoryname.text;
            //obj.creationdate = "2017-11-08t12:19:27.628z";
            //obj.entitystatus = "0";

            //var serializeddata = jsonconvert.serializeobject(obj);
            //var apiurl = application.current.properties["domainurl"] + "/api/services/createcategory";

            //var result = postdata("post", serializeddata, apiurl);

            Navigation.PushAsync(new AddServiceToCategoryPage());

           // Navigation.PushAsync(new ChooseCategoriesPage());
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