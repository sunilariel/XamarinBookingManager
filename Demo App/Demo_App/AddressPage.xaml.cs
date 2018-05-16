using Demo_App.Model;
using Newtonsoft.Json;
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
	public partial class AddressPage : ContentPage
	{
		public AddressPage ()
		{
            //BindingContext = CustObj;
            InitializeComponent ();
		}


        private async Task SaveCustomerAddress(object sender, SelectedItemChangedEventArgs e)
        {
            Address obj = new Address();
            obj.Id = 0;
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.StreetName = txtstreet.Text;
            obj.CityName = txtCity.Text;
            obj.ZipCode = Convert.ToInt32(txtZipCode.Text);
            obj.CreationDate = "2017-11-08T12:19:27.628Z";

            //DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

            //var data = JsonConvert.SerializeObject(obj);
            //var Url = Application.Current.Properties["DomainUrl"] + "/api/customer/Create";
            //var ApiMethod = "POST";

            //var result = PostData(ApiMethod, data, Url);

            MessagingCenter.Send<AddressPage, Address>(this,
         "address", obj);
           // await Navigation.PushAsync(new NewCustomerPage(obj));

        }

        //public string PostData(string Method, string SerializedData, string Url)
        //{
        //    try
        //    {
        //        var result = "";
        //        HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(Url);
        //        httpRequest.Method = Method;
        //        httpRequest.ContentType = "application/json";
        //        httpRequest.ProtocolVersion = HttpVersion.Version10;
        //        httpRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

        //        if (SerializedData != null)
        //        {
        //            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
        //            streamWriter.Write(SerializedData);
        //            streamWriter.Close();
        //        }

        //        var httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();

        //        using (var StreamReader = new StreamReader(httpWebResponse.GetResponseStream()))
        //        {
        //            return result = StreamReader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.ToString();
        //    }
        //}
    }
}