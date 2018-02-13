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
	public partial class EditCustomerPage : ContentPage
	{
        #region GlobleFields
        int CustomerId;
        Notes objnotes = new Notes();
        public Customer objCust = null;
        #endregion

        public EditCustomerPage ()
		{
            GetSelectedCustomerById();          
            InitializeComponent ();
            CustomerId = objCust.Id;
            BindingContext = objCust;
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
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (e.TotalY != 0 && EditCustomer.HeightRequest > 30 && EditCustomer.HeightRequest < 151)
                {
                    EditCustomer.HeightRequest = EditCustomer.HeightRequest + e.TotalY;
                    if (EditCustomer.HeightRequest < 31)
                        EditCustomer.HeightRequest = 31;
                    if (EditCustomer.HeightRequest > 150)
                        EditCustomer.HeightRequest = 150;
                }
                //stack.HeightRequest = 20;
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        //private void AddressClick(object sender, EventArgs args)
        //{
        //    Navigation.PushAsync(new AddressPage());
        //}

        public void EditCustomerInformation()
        {
            try
            {
                Customer obj = new Customer();
                obj.Id = CustomerId;
                obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                obj.FirstName = EditCustomerName.Text;
                obj.LastName = "";

                obj.UserName = "";
                obj.Password = "";
                obj.Email = EditCustomerEmail.Text;
                obj.TelephoneNo = EditCustomerPhoneNo.Text;
                obj.CreationDate = "2017-11-08T12:19:27.628Z";

                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/customer/Update";
                var serailizeddata = JsonConvert.SerializeObject(obj);

                var result = PostData("POST", serailizeddata, apiUrl);

                Navigation.PushAsync(new CutomerProfilePage());
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

        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}