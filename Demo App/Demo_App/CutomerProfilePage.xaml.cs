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
            try
            {
                InitializeComponent();
                GetSelectedCustomerById();
                CustomerId = objCust.Id;
                var notesList = GetAllCustomerNotes();
               // var notesList1=   notesList.OrderByDescending(x => x.CreationDate);
                BindingContext = objCust;
                foreach (var item in notesList)
                {
                    Noteslbl.Text = item.Description;
                }
            }
            catch(Exception e)
            {
                e.ToString();
            }

        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var notesList = GetAllCustomerNotes();
                ObservableCollection<Notes> notesLst = new ObservableCollection<Notes>();
                foreach (var data in notesList)
                {
                    Notes obj = new Notes();
                    obj.CompanyId = data.CompanyId;
                    obj.CreationDate = Convert.ToDateTime(data.CreationDate);
                    obj.CustomerId = data.CustomerId;
                    obj.Description = data.Description;
                    obj.WhoAddedThis = data.WhoAddedThis;
                    notesLst.Add(obj);
                }
                notesLst.OrderByDescending(x => x.CreationDate);

                if (notesLst != null)
                {
                    Noteslbl.Text = notesLst[0].Description;
                }
            }
            catch(Exception e)
            {
                e.ToString();
            }
          
        }

        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
        
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                ex.ToString();
            }
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
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Method = "DELETE";
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/DeleteCustomer?companyId=" + CompanyId + "&customerId=" + CustomerId;
                PostData(Method, "", Url);

                Navigation.PushAsync(new CustomerPage());
            }
            catch(Exception e)
            {
                e.ToString();
            }
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
                e.ToString();
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
                ObservableCollection<Notes> objnotes = new ObservableCollection<Notes>();
                return objnotes;
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