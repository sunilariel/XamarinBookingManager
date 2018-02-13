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
        #region GloblesFields
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;
        int ServiceID;
        ObservableCollection<AssignedServicetoStaff> ListOfAssignServiceData = new ObservableCollection<AssignedServicetoStaff>();
        public Customer objCust = null;
        public Notes objNotes = null;
        string PageName = "";
        #endregion

        public SelectServicesForAppontment (string pagename)
		{
			InitializeComponent ();
            PageName = pagename;           
            GetSelectedService();
        }

        //private void AddNewAppointment(object sender,SelectedItemChangedEventArgs e)
        //{
        //    var service = e.SelectedItem as Service;            
        //    Navigation.PushAsync(new CreateNewAppointmentsPage(service));
        //}

        private void SelectStaffForCustomer(object sender,SelectedItemChangedEventArgs e)
        {
            try
            {
                var servicedata = e.SelectedItem as AssignedServicetoStaff;
                Service service = new Service();
                service.Name = servicedata.Name;
                service.Id = servicedata.Id;
                service.Cost = servicedata.Cost;
                Navigation.PushAsync(new SelectStaffForAppointmentPage(service, PageName));
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

       

        public ObservableCollection<AssignedServicetoStaff> GetSelectedService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/GetAllServicesForCategory?companyId=" + CompanyId + "&categoryId=" + Application.Current.Properties["CategoryID"];
                var result = PostData("GET", "", apiUrl);

                ListOfAssignServiceData = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);

                foreach (var item in ListOfAssignServiceData)
                {
                    var details = item.DurationInMinutes / 60 + "hrs " + item.DurationInMinutes % 60 + "mins" + " " + item.Cost;
                    item.ServiceDetails = details;
                }
                ListofAllServices.ItemsSource = ListOfAssignServiceData;
                return ListOfAssignServiceData;
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