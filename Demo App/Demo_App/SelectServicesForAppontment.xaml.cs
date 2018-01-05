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
	public partial class SelectServicesForAppontment : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;       
        int ServiceID;
        ObservableCollection<AssignedServicetoStaff> ListOfAssignServiceData = new ObservableCollection<AssignedServicetoStaff>();
        public Customer objCust = null;
        public Notes objNotes = null;
        string PageName = "";
        public SelectServicesForAppontment (int CategoryId,Customer Cust,string pagename)
		{
			InitializeComponent ();
            PageName = pagename;
            objCust = new Customer();
            objCust.Id = Cust.Id;
            objCust.FirstName = Cust.FirstName;
            objCust.LastName = Cust.LastName;
            objCust.UserName = Cust.UserName;
            objCust.Email = Cust.Email;
            objCust.TelephoneNo = Cust.TelephoneNo;
            objCust.Address = Cust.Address;
            CategoryID = CategoryId;
            GetSelectedService();
        }

        //private void AddNewAppointment(object sender,SelectedItemChangedEventArgs e)
        //{
        //    var service = e.SelectedItem as Service;            
        //    Navigation.PushAsync(new CreateNewAppointmentsPage(service));
        //}

        private void SelectStaffForCustomer(object sender,SelectedItemChangedEventArgs e)
        {           
            var servicedata = e.SelectedItem as AssignedServicetoStaff;
            Service service = new Service();
            service.Name = servicedata.Name;
            service.Id = servicedata.Id;
            service.Cost = servicedata.Cost;
            Navigation.PushAsync(new SelectStaffForAppointmentPage(service, objCust, PageName,objNotes));
        }

       

        public ObservableCollection<AssignedServicetoStaff> GetSelectedService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + CategoryID;
            var result = PostData("GET", "", apiUrl);

            ListOfAssignServiceData = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);           
            ListofAllServices.ItemsSource = ListOfAssignServiceData;
            return ListOfAssignServiceData;
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