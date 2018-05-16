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
    public partial class GetAllocateServiceForEmployeePage : ContentPage
    {
        #region GloblesFields
        int EmpID;
        string EmpName;
        string TimePeriods;
        DateTime dateofBooking;
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        ObservableCollection<AssignedServicetoStaff> ListOfAllocatedServiceForEmployee = new ObservableCollection<AssignedServicetoStaff>();
        #endregion

        public GetAllocateServiceForEmployeePage()
        {
            InitializeComponent();
            GetAllocatedServiceForEmployee();

        }

        private void GetTimeforNewAppointmentClick(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;            
            var Data = e.SelectedItem as AddAppointments;
            Navigation.PushAsync(new CalendarTimeSlotsPage(Data, "CalandarAppointment"));
            ((ListView)sender).SelectedItem = null;
        }

        public ObservableCollection<AssignedServicetoStaff> GetAllocatedServiceForEmployee()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetAllocateServiceForEmployee?empid=" + Application.Current.Properties["SelectedEmpId"] + "&compid=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                ListOfAllocatedServiceForEmployee = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);

                foreach (var item in ListOfAllocatedServiceForEmployee)
                {
                    var details = item.DurationInMinutes / 60 + "hrs " + item.DurationInMinutes % 60 + "mins" + " " + item.Cost;
                    item.ServiceDetails = details;
                }

                ListofAllocatedServicesforEmployee.ItemsSource = ListOfAllocatedServiceForEmployee;
                return ListOfAllocatedServiceForEmployee;
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