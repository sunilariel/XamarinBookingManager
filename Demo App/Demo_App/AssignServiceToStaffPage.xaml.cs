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
using XLabs.Forms.Controls;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssignServiceToStaffPage : ContentPage
	{
        #region GlobleFields
        string result = "";
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int EmployeeId;
        public AssignedServicetoStaff objs = null;
        //ObservableCollection<AssignedServicetoStaff> ListofServices = new ObservableCollection<AssignedServicetoStaff>();
        ObservableCollection<AssignedServicetoStaff> ListofServices = null;



        public Staff obj = null;
        #endregion
        public AssignServiceToStaffPage (Staff ObjStaff, ObservableCollection<AssignedServicetoStaff> ListofAllocatedServices)
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
                ListofServices = ListofAllocatedServices;
                //ListofServices = GetAllService();
                AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Service selected";
                ListofAllServices.ItemsSource = ListofServices;
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public void AssignAllProvider(object Sender, EventArgs args)
        {
            try
            {

                CheckBox AllProvider = (CheckBox)Sender;
                if (AllProvider.Checked == true)
                {
                    foreach (var item in ListofServices)
                    {
                        //  AllStaffChecked.Checked = true;    
                        item.AllAssigned = true;
                        item.isAssigned = true;
                    }
                }
                else
                {
                    foreach (var item in ListofServices)
                    {
                        //  AllStaffChecked.Checked = false;
                        item.AllAssigned = true;
                        item.isAssigned = false;
                    }
                }
                AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Service selected";
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void AssignProvider(object Sender, EventArgs args)
        {
            try
            {
                for (int i = 0; i < ListofServices.Count; i++)
                {
                    if (ListofServices[i].isAssigned == false)
                    {
                        foreach (var item in ListofServices)
                        {
                            item.AllAssigned = false;
                        }
                        break;
                    }
                    else
                    {

                        ListofServices[i].AllAssigned = true;
                        // AllStaffChecked.Checked = true;
                    }
                }
                AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Service selected";
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public int GetAllocatedServiceCount()
        {
            try
            {
                int count = 0;
                foreach (var item in ListofServices)
                {
                    if (item.isAssigned == true)
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                e.ToString();
                return 0;
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
                e.ToString();
                return null;
            }
        }

        public void ServiceAssigntoStaff()
        {
            try
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


                //foreach (var item in data)
                //{
                //    if (item.isAssigned == true)
                //    {
                //        AssignServiceToStaff obj = new AssignServiceToStaff();
                //        obj.CompanyId = Convert.ToInt32(CompanyId);
                //        obj.EmployeeId = EmployeeId;
                //        obj.ServiceId = item.Id;
                //        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");


                //        var SerializedData = JsonConvert.SerializeObject(obj);

                //        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/AssignServiceToStaff";
                //        var result = PostData("POST", SerializedData, apiUrl);
                //    }
                //}

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