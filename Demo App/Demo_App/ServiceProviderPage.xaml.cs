﻿using Demo_App.Model;
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
	public partial class ServiceProviderPage : ContentPage
	{      
        string CompanyId = (Application.Current.Properties["CompanyId"]).ToString();
        int ServiceId;
        string PreviousPageName = "";
            
        ObservableCollection<AssignCategory> ListofAllCategories = new ObservableCollection<AssignCategory>();
         ObservableCollection<AssignProvider> ListofServiceProviders = new ObservableCollection<AssignProvider>();

        public ServiceProviderPage (ObservableCollection<AssignProvider> ListofProviders,int serviceId,string PageName)
		{
			InitializeComponent ();
            PreviousPageName = PageName;
            ServiceId = serviceId;          
            ListofServiceProviders = ListofProviders;
           
            AllocatedProviderCount.Text =  GetAllocatedServiceCount() + " " + "Staff selected" ;
            AllServiceProvider.ItemsSource = ListofServiceProviders;

        }
        
        public void AssignAllProvider(object Sender,EventArgs args)
        {
            
            CheckBox AllProvider = (CheckBox)Sender;
            if (AllProvider.Checked == true)
            {
                foreach( var item in ListofServiceProviders)
                {
                    //  AllStaffChecked.Checked = true;    
                    item.AllConfirmed = true;
                    item.confirmed = true;
                }
            }
            else
            {
                foreach (var item in ListofServiceProviders)
                {
                    //  AllStaffChecked.Checked = false;
                    item.AllConfirmed = true;
                    item.confirmed = false;
                }
            }
            AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Staff selected";
        }

        public void AssignProvider(object Sender, EventArgs args)
        {
            
                for (int i = 0; i < ListofServiceProviders.Count; i++)
                {
                    if (ListofServiceProviders[i].confirmed == false)
                    {
                  foreach( var item in ListofServiceProviders)
                    {                        
                        item.AllConfirmed = false;
                    }
                        break;
                    }
                    else
                    {
                 
                    ListofServiceProviders[i].AllConfirmed = true;
                    // AllStaffChecked.Checked = true;
                    }
                }
            AllocatedProviderCount.Text = GetAllocatedServiceCount() + " " + "Staff selected";

        }

        public int GetAllocatedServiceCount()
        {
            int count = 0;
            foreach(var item in ListofServiceProviders)
            {
                if(item.confirmed==true)
                {
                    count++;
                }
            }
            return count ;
        }

        public void AddProviderstoService()
        {
            var Url = Application.Current.Properties["DomainUrl"] + "api/companyregistration/AssignServiceToStaff";
         
            foreach( var item in ListofServiceProviders)
            {
                if (item.confirmed == true)
                {
                    AssignServiceToStaff obj = new AssignServiceToStaff();
                    obj.CompanyId = Convert.ToInt32(CompanyId);
                    obj.ServiceId = ServiceId;
                    obj.EmployeeId = item.Id;
                    obj.CreationDate = DateTime.Now.ToString();

                    var SerializedData = JsonConvert.SerializeObject(obj);
                    var result = PostData("POST", SerializedData, Url);
                }
                else
                {
                    var apiUrl = Application.Current.Properties["DomainUrl"] + "api/companyregistration/DeAllocateServiceForEmployee?companyId=" + CompanyId + "&employeeId=" + item.Id + "&serviceId=" + ServiceId;
                    var result = PostData("POST", "", apiUrl);
                }


            }
            if (PreviousPageName == "EditService")
            {
                Navigation.PushAsync(new ServiceDetailsPage(GetSelectedService()));
            }
            else if (PreviousPageName == "AddService")
            {
                Navigation.PushAsync(new ChooseCategoriesPage(GetCategories(), ServiceId));
            }
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            // add the checkmark in the event because the item was clicked
            // be able to check the item here
            DisplayAlert("Tapped", e.SelectedItem + " row was tapped", "OK");
            ((ListView)sender).SelectedItem = null;
        }
       
        public ObservableCollection<AssignCategory> GetCategories()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceCategoriesForCompany?companyId=" + CompanyId;
            var result = PostData("GET", "", apiUrl);
            ListofAllCategories = JsonConvert.DeserializeObject<ObservableCollection<AssignCategory>>(result);
            return ListofAllCategories;
        }

        public Service GetSelectedService()
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetServiceById?id=" + ServiceId;
            var result = PostData("GET", "", apiUrl);
            Service ServiceDetail = JsonConvert.DeserializeObject<Service>(result);
            return ServiceDetail;
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
                if (Url != "http://bookingmanager24-001-site1.ftempurl.com/api/companyregistration/AssignServiceToStaff")
                {
                    httpRequest.ContentLength = 0;
                }

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