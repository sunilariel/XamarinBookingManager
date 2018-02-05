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
	public partial class AssignServiceToStaffPage : ContentPage
	{
        #region GlobleFields
        string result = "";
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int EmployeeId;
        ObservableCollection<AssignedServicetoStaff> ListofServices = new ObservableCollection<AssignedServicetoStaff>();
        public Staff obj = null;
        #endregion
        public AssignServiceToStaffPage (Staff ObjStaff)
		{
            try
            {
                InitializeComponent();
                obj = new Staff();
                obj.Id = ObjStaff.Id;
                obj.FirstName = ObjStaff.FirstName;
                obj.LastName = ObjStaff.LastName;
                obj.Email = ObjStaff.Email;
                obj.Address = ObjStaff.Address;
                obj.TelephoneNo = ObjStaff.TelephoneNo;
                EmployeeId = ObjStaff.Id;
                ListofServices = GetAllService();
                ListofAllServices.ItemsSource = ListofServices;
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public ObservableCollection<AssignedServicetoStaff> GetAllService()
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                ObservableCollection<AssignedServicetoStaff> ListofServices = JsonConvert.DeserializeObject<ObservableCollection<AssignedServicetoStaff>>(result);
                return ListofServices;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public void ServiceAssigntoStaff()
        {
            try
            {
                var data = ListofServices;
                foreach (var item in data)
                {
                    if (item.isAssigned == true)
                    {
                        AssignServiceToStaff obj = new AssignServiceToStaff();
                        obj.CompanyId = Convert.ToInt32(CompanyId);
                        obj.EmployeeId = EmployeeId;
                        obj.ServiceId = item.Id;
                        obj.CreationDate = DateTime.Now.ToString();


                        var SerializedData = JsonConvert.SerializeObject(obj);

                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AssignServiceToStaff";
                        var result = PostData("POST", SerializedData, apiUrl);
                    }
                }

                Navigation.PushAsync(new StaffServicePeofile());
            }
            catch(Exception e)
            {
                e.ToString();
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