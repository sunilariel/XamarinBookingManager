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
        //string CategoryID = Convert.ToString(Application.Current.Properties["CategoryID"]);


        int StatusId;
        int CategoryID;
        string CategoryName;
        int ServiceID;
        ObservableCollection<AssignedServicetoStaff> ListOfAssignServiceData = new ObservableCollection<AssignedServicetoStaff>();
        public Customer objCust = null;
        public Notes objNotes = null;
        string PageName = "";
        string selectedDateofBooking;
        #endregion

        public SelectServicesForAppontment (string pagename,int categoryID,string categoryName,string DateofBooking,int statusid)
		{
			InitializeComponent ();
            PageName = pagename;
            CategoryID = categoryID;
            CategoryName = categoryName;
            StatusId = statusid;
            selectedDateofBooking = DateofBooking;
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

                service.CategoryId = CategoryID;
                service.CategoryName = CategoryName;

                //service.CategoryId = Convert.ToInt32(Application.Current.Properties["CategoryID"]);
                
                //service.CategoryName = servicedata.CategoryName;
                service.Buffer = servicedata.Buffer;
                service.CompanyId = servicedata.CompanyId;
                service.CreationDate = servicedata.CreationDate;
                service.Currency = servicedata.Currency;

                //var totalHours = Convert.ToInt32(servicedata.DurationInHours);
                var totalMinutes = Convert.ToInt32(servicedata.DurationInMinutes);
                //var hour = TimeSpan.FromHours(totalHours);
                //var time = TimeSpan.FromMinutes(totalMinutes);
                //var durationHours = string.Format("{0:00}", (int)hour.Hours);
                //var durationMinutes = string.Format("{0:00}", (int)time.Minutes);

                service.DurationInHours = 0;
                service.DurationInMinutes = Convert.ToInt32(totalMinutes);
                
                Navigation.PushAsync(new SelectStaffForAppointmentPage(service, PageName,selectedDateofBooking, StatusId));
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
                    //var totalHours = Convert.ToInt32(item.DurationInHours);
                    var totalMinutes = Convert.ToInt32(item.DurationInMinutes);
                    //var hour = TimeSpan.FromHours(totalHours);
                    var time = TimeSpan.FromMinutes(totalMinutes);
                    var durationHours = string.Format("{0:00}", (int)time.Hours);
                    var durationMinutes = string.Format("{0:00}", (int)time.Minutes);

                    var details = durationHours + "hrs " + durationMinutes + "mins" + " " + " $ " + item.Cost;
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