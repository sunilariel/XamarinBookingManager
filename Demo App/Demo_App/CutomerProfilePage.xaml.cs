using Demo_App.Model;
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
	public partial class CutomerProfilePage : ContentPage
	{
        //string phonNumber;
        int CustomerId;      
        public Customer objCust = null;
        public BookAppointment objBookAppointment = null;
        public CutomerProfilePage (Customer Cust,Notes obj)
		{
            //this.phonNumber = Cust.TelephoneNo;
             BindingContext = Cust;
            CustomerId = Cust.Id;
            InitializeComponent ();
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            BindingContext = objCust;
            Noteslbl.Text = obj.Description;

            GetAllCustomerNotes();

        }
        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
        //private void OnOpenPupup(object sender,EventArgs e)
        //{
        //    deletelbl.IsVisible = true;
        //    plusimsge.IsVisible = false;
        //}

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && CustomerProfile.HeightRequest > 30 && CustomerProfile.HeightRequest < 151)
            {
                CustomerProfile.HeightRequest = CustomerProfile.HeightRequest + e.TotalY;
                if (CustomerProfile.HeightRequest < 31)
                    CustomerProfile.HeightRequest = 31;
                if (CustomerProfile.HeightRequest > 150)
                    CustomerProfile.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }

        private void AddNotesClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNotesPage(objCust));
        }
        private void AppointmentsClicks(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAppointmentsPage(objCust, objBookAppointment));
        }
        private void EditCustomerClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditCustomerPage(objCust));
        }

        public void DeleteCustomer()
        {

            var CompanyId = Application.Current.Properties["CompanyId"];
            var Method = "DELETE";
            var Url = Application.Current.Properties["DomainUrl"] + "api/customer/DeleteCustomer?companyId=" + CompanyId + "&customerId=" + CustomerId;
            PostData(Method, "", Url);

            Navigation.PushAsync(new CustomerPage());
        }



        public void GetAllCustomerNotes()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetAllCustomerNotes?companyId=" + CompanyId+ "&customerId=" + CustomerId;
            var Method = "GET";

            var result = PostData(Method, "", Url);
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

                if (SerializedData != 
                    "")
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