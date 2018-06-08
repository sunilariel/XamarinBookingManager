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
        #region globles
        ObservableCollection<AssignProvider> ListofProvider = new ObservableCollection<AssignProvider>();
        //int EmployeeId;
        int ServiceId;
        int DurationInMinutes;
        int DurationInHours;

        int statusID;
        string ServiceName = "";
        double Cost;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public AssignedServicetoStaff serviceobj = null;
        ObservableCollection<AssignedServicetoStaff> ListofData = new ObservableCollection<AssignedServicetoStaff>();
        public Customer objCust = null;
        public Notes objNotes = null;
        string PageName = "";
        string selectedDateofBooking;
        //int TotalDurationHoursAndDurationMinutes;
        #endregion

        public SelectStaffForAppointmentPage(Service service, string pagename, string DateofBooking, int statusId)
        {
            InitializeComponent();
            PageName = pagename;
            Cost = service.Cost;
            ServiceId = service.Id;
            DurationInHours = service.DurationInHours;
            DurationInMinutes = service.DurationInMinutes;
            statusID = statusId;


            selectedDateofBooking = DateofBooking;
            ServiceName = service.Name;
            var staffData = GetServiceProvider();
            //GetSelectedStaff();
            nemeStafftext.IsVisible = false;
            if (staffData.Count == 0)
            {
                nemeStafftext.IsVisible = true;
                nemeStafftext.Text = "Please first add staff";
            }
            else
            {
                foreach (var item in staffData)
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

        }

        private void AddNewAppointmentForCustomerClick(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;

                AssignedServicetoStaff EmployeeData = new AssignedServicetoStaff();
                EmployeeData = e.SelectedItem as AssignedServicetoStaff;
                Navigation.PushAsync(new CreateNewAppointmentsPage(ServiceId, ServiceName, EmployeeData.Id, EmployeeData.Name, Cost, DurationInHours, DurationInMinutes, PageName, selectedDateofBooking, statusID));
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public ObservableCollection<AssignProvider> GetServiceProvider()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/clientreservation/GetEmployeeAllocatedToService?serviceId=" + ServiceId;

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
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public ObservableCollection<AssignProvider> GetStaff()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, "", Url);

                ObservableCollection<AssignProvider> ListofServiceProviders = JsonConvert.DeserializeObject<ObservableCollection<AssignProvider>>(result);


                return ListofServiceProviders;
            }
            catch (Exception e)
            {
                e.ToString();
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