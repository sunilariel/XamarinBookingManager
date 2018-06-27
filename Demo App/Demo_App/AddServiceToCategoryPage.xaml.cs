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
//using Android.Widget;
using XLabs.Forms.Controls;


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddServiceToCategoryPage : ContentPage
    {
        #region GlobleFields
        ObservableCollection<AssignedServicetoStaff> ListofAllService = new ObservableCollection<AssignedServicetoStaff>();
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        int CategoryID;
        string CategoryName = "";
        string btnname = "";
        #endregion

        public AddServiceToCategoryPage(ObservableCollection<AssignedServicetoStaff> ListofServices, int CategoryId, string categoryName,string btnName)
        {
            InitializeComponent();
            btnname = btnName;
            ListofAllService = ListofServices;
            CategoryID = CategoryId;
            CategoryName = categoryName.ToString();
            AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + " selected";
            ListofAllServiceData.ItemsSource = ListofAllService;
        }

        public void AddServicestoCategory()
        {
            try
            {
                foreach (var item in ListofAllService)
                {
                    if (item.isAssigned == true)
                    {                        
                        AssignServiceToCategory obj = new AssignServiceToCategory();
                        obj.CategoryName = CategoryName;
                        obj.CompanyId = Convert.ToInt32(CompanyId);
                        obj.CategoryID = Convert.ToInt32(CategoryID);
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.ServiceId = item.Id;
                        //obj.isAssigned = true;
                        var Url = Application.Current.Properties["DomainUrl"] + "/api/services/AssignCategoryToService?companyId=" + CompanyId + "&categoryId=" + CategoryID + "&serviceId=" + item.Id;
                        var SerializedData = JsonConvert.SerializeObject(obj);
                        var result = PostData("PUT", SerializedData, Url);

                    }
                    else
                    {
                        AssignServiceToCategory obj = new AssignServiceToCategory();
                        obj.CategoryName = CategoryName;
                        obj.CompanyId = Convert.ToInt32(CompanyId);
                        obj.CategoryID = Convert.ToInt32(CategoryID);
                        obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                        obj.ServiceId = item.Id;
                        //obj.isAssigned = true;
                        //api/services/DeAllocateCategoryFromService?companyId={companyId}&categoryId={categoryId}&serviceId={serviceId}


                        var SerializedData = JsonConvert.SerializeObject(obj);
                        var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/DeAllocateCategoryFromService?companyId=" + CompanyId + "&categoryId=" + CategoryID + "&serviceId=" + item.Id;
                        var result = PostData("POST", SerializedData, apiUrl);
                    }
                }

                Navigation.PushAsync(new CategoryDetailsPage(CategoryID, CategoryName,btnname));
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void AssignAllProviders(object Sender,EventArgs args)
        {
            try
            {

                XLabs.Forms.Controls.CheckBox AllProviders = (XLabs.Forms.Controls.CheckBox)Sender;
                if (AllProviders.Checked == true)
                {
                    foreach (var item in ListofAllService)
                    {
                        //  AllStaffChecked.Checked = true;    
                        item.AllAssigned = true;
                        item.isAssigned = true;
                    }
                }
                else
                {
                    foreach (var item in ListofAllService)
                    {
                        //  AllStaffChecked.Checked = false;
                        item.AllAssigned = true;
                        item.isAssigned = false;
                    }
                }
                AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + " selected";
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
                foreach (var item in ListofAllService)
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

        public void AssignServiceToCategory(object Sender, EventArgs args)
        {
            try
            {
                for (int i = 0; i < ListofAllService.Count; i++)
                {
                    if (ListofAllService[i].isAssigned == false)
                    {
                        foreach (var item in ListofAllService)
                        {
                            item.AllAssigned = false;
                        }
                        break;
                    }
                    else
                    {

                        ListofAllService[i].AllAssigned = true;
                        // AllStaffChecked.Checked = true;
                    }
                }
                AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "selected";
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public void GetService(string CompanyId)
        {
            try
            {
                var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServicesForCompany?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);

                List<Service> ListofServices = JsonConvert.DeserializeObject<List<Service>>(result);
                ListofAllServiceData.ItemsSource = ListofServices;
            }
            catch (Exception e)
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