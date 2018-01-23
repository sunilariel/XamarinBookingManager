using Demo_App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CutomerProfilePage : ContentPage
	{
        #region GlobleFields
        //string phonNumber;
        int CustomerId;
        public Customer objCust = null;
        public Notes obj = null;
        public BookAppointment objBookAppointment = null;
        ObservableCollection<Notes> ListNotes = new ObservableCollection<Notes>();
        #endregion
       
        public CutomerProfilePage ()
		{                       
            InitializeComponent ();
            GetSelectedCustomerById();
            CustomerId = objCust.Id;
            var notesList = GetAllCustomerNotes();
            //objCust = new Customer();
            //objCust.Id = Cust.Id;
            //objCust.FirstName = Cust.FirstName;
            //objCust.LastName = Cust.LastName;
            //objCust.UserName = Cust.UserName;
            //objCust.Email = Cust.Email;
            //objCust.TelephoneNo = Cust.TelephoneNo;
            //objCust.Address = Cust.Address;
            BindingContext = objCust;
            foreach (var item in notesList)
            {
                Noteslbl.Text = item.Description;
            }                      

        }
        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
        
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
            Navigation.PushAsync(new AddNotesPage());
        }
        private void AppointmentsClicks(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAppointmentsPage(objBookAppointment));
        }
        private void EditCustomerClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditCustomerPage());
        }

        public void DeleteCustomer()
        {

            var CompanyId = Application.Current.Properties["CompanyId"];
            var Method = "DELETE";
            var Url = Application.Current.Properties["DomainUrl"] + "api/customer/DeleteCustomer?companyId=" + CompanyId + "&customerId=" + CustomerId;
            PostData(Method, "", Url);

            Navigation.PushAsync(new CustomerPage());
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

        public ObservableCollection<Notes> GetAllCustomerNotes()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + CustomerId;
                var Method = "GET";

                var result = PostData(Method, "", Url);
                ListNotes = JsonConvert.DeserializeObject<ObservableCollection<Notes>>(result);
                return ListNotes;
            }
            catch(Exception e)
            {
                return null;
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