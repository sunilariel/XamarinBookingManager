using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
	public partial class AddNotesPage : ContentPage
    {
        public Customer objCust = null;      
        public AddNotesPage ()
		{
			InitializeComponent ();
            
            //objCust = new Customer();
            //objCust = Cust;
            //BindingContext = objCust;
        }

        public void GetSelectedCustomerById()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetCustomerById?id=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objCust = JsonConvert.DeserializeObject<Customer>(result);
            }
            catch (Exception e)
            {

            }

        }
        public void SaveNotes(object sender, SelectedItemChangedEventArgs e)
        {
            GetSelectedCustomerById();
            Notes obj = new Notes();
            if (objCust != null)
            {
                obj.CustomerId = objCust.Id;
            }
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.Description = CustomerNote.Text;
            obj.WhoAddedThis = "";            
            obj.CreationDate = "2017-11-08T12:19:27.628Z";

            var data = JsonConvert.SerializeObject(obj);
            var Url = Application.Current.Properties["DomainUrl"] + "api/customer/AddNote";
            var ApiMethod = "POST";

            var result = PostData(ApiMethod, data, Url);
           
                Navigation.PopAsync(true);
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