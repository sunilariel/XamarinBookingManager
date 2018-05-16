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
	public partial class ServicesProviderPage : ContentPage
	{
        #region GlobleFields
        string result = "";
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int EmployeeId;
        ObservableCollection<AssignedServicetoStaff> ListofServices = new ObservableCollection<AssignedServicetoStaff>();
        #endregion

        public ServicesProviderPage (int StaffId,ObservableCollection<AssignedServicetoStaff> ListofAllocatedServices)
		{
            EmployeeId = StaffId;
			InitializeComponent ();
            ListofServices = ListofAllocatedServices;
            ListofAllServices.ItemsSource = ListofServices;

            // GetService();
        }

        //public void GetService()
        //{
        //    var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
        //    var result = PostData("GET", "", apiUrl);

        //    ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
        //    ListofAllServices.ItemsSource = ListofServices;
        //}
        //public void AssignProvider(object Sender, EventArgs args)
        //{
        //    try
        //    {
        //        for (int i = 0; i < ListofServices.Count; i++)
        //        {
        //            if (ListofServices[i].isAssigned == false)
        //            {
        //                foreach (var item in ListofServices)
        //                {
        //                    item.isAssigned = false;
        //                }
        //                break;
        //            }
        //            else
        //            {

        //                ListofServices[i].isAssigned = true;
        //                // AllStaffChecked.Checked = true;
        //            }
        //        }
        //        //AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Staff selected";
        //    }
        //    catch (Exception e)
        //    {
        //        e.ToString();
        //    }

        //}
        public void AssignServicetoStaff()
        {
            var data = ListofServices;
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/staff/AllocateService";
            foreach (var item in ListofServices)
            {
                if (item.isAssigned == true)
                {
                    AssignServiceToStaff obj = new AssignServiceToStaff();
                    obj.CompanyId = Convert.ToInt32(CompanyId);
                    obj.EmployeeId = EmployeeId;
                    obj.ServiceId = item.Id;
                    obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                    var SerializedData = JsonConvert.SerializeObject(obj);
                   
                    var result = PostData("POST", SerializedData, apiUrl);
                }
               else
                {
                    AssignServiceToStaff obj = new AssignServiceToStaff();
                    var SerializedData = JsonConvert.SerializeObject(obj);
                    var Url = Application.Current.Properties["DomainUrl"] + "/api/staff/DeAllocateServiceForEmployee?companyId=" + CompanyId + "&employeeId=" + EmployeeId + "&serviceId=" + item.Id;
                    var result = PostData("POST", SerializedData, Url);
                }
            }            
            
            Navigation.PushAsync(new StaffProfileDetailsPage());
        }

        public Staff GetSelectedStaff()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + EmployeeId;

            var result = PostData("GET", "", apiUrl);

            Staff obj = JsonConvert.DeserializeObject<Staff>(result);

            return obj;

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