using Demo_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class SelectStaffForAppointmentPage : ContentPage
	{
        ObservableCollection<AssignProvider> ListofProvider = new ObservableCollection<AssignProvider>();      
        //int EmployeeId;
        int ServiceId;
        string ServiceName = "";
        double Cost;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public AssignedServicetoStaff serviceobj = null;
        ObservableCollection<AssignedServicetoStaff> ListofData = new ObservableCollection<AssignedServicetoStaff>();
        public Customer objCust = null;
        string PageName = "";
        public SelectStaffForAppointmentPage (Service service,Customer Cust,string pagename)
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
            Cost = service.Cost;
            ServiceId = service.Id;
            ServiceName = service.Name;
                  var staffData=GetServiceProvider();           
            //GetSelectedStaff();
            foreach(var item in staffData)
            {
                serviceobj = new AssignedServicetoStaff();
                serviceobj.Id = item.Id;
                if (item.confirmed == true)
                {                   
                    serviceobj.Name = item.FirstName;
                    ListofData.Add(serviceobj);
                }
                //var result = serviceobj;
               
            }
            ListofSelectedStaff.ItemsSource = ListofData;
        }

        private void AddNewAppointmentForCustomerClick(object sender,SelectedItemChangedEventArgs e)
        {
            AssignedServicetoStaff EmployeeData = new AssignedServicetoStaff();
             EmployeeData = e.SelectedItem as AssignedServicetoStaff;
            Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceId, ServiceName,EmployeeData.Id,EmployeeData.Name, objCust,Cost, PageName));
        }

        public ObservableCollection<AssignProvider> GetServiceProvider()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetEmployeeAllocatedToService?serviceId=" + ServiceId;

            var result = PostData("GET", "", apiUrl);

            ObservableCollection<AssignProvider> ListOfAssignProvider = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);

            ObservableCollection<AssignProvider> ListofProvider = GetStaff();

            foreach (var provider in ListofProvider)
            {
                foreach (var AssignProvider in ListOfAssignProvider)
                {
                    if (provider.Id == AssignProvider.Id)
                    {
                        provider.confirmed = true;
                    }
                }              
            }            
            return ListofProvider;
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {

            var Url = Application.Current.Properties["DomainUrl"] + "api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
            var Method = "GET";

            var result = PostData(Method, "", Url);

            ObservableCollection<AssignProvider> ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);


            return ListofServiceProviders;
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