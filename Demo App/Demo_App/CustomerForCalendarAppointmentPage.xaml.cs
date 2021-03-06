﻿using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class CustomerForCalendarAppointmentPage : ContentPage
    {
        public AddAppointments objaddAppointment = null;
        public CustomerForCalendarAppointmentPage(AddAppointments obj,string page)
        {
            try
            {
                InitializeComponent();
                //var hrs = obj.DurationInMinutes;
                //var min = obj.DurationInMinutes;
                objaddAppointment = new AddAppointments();
                objaddAppointment.ServiceId = obj.ServiceId;
                objaddAppointment.ServiceName = obj.ServiceName;
                objaddAppointment.EmployeeId = obj.EmployeeId;
                objaddAppointment.EmployeeName = obj.EmployeeName;
                objaddAppointment.Cost = obj.Cost;
                objaddAppointment.StartTime = obj.StartTime;
                objaddAppointment.CompanyId = obj.CompanyId;

                objaddAppointment.EndTime = obj.EndTime;
                objaddAppointment.TimePeriod = obj.TimePeriod;
                objaddAppointment.DurationInHours = obj.DurationInHours;
                objaddAppointment.DurationInMinutes = obj.DurationInMinutes;
                objaddAppointment.DateOfBooking = obj.DateOfBooking;
                var customerlist = GetAllCustomer();
                if (customerlist.Count > 5)
                {
                    CustomerSearchBar.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        private void AddNewCustomerForBookingTime(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewCustomerPage(objaddAppointment, "AddCustomerBookingTime"));
        }

        private void selectedCustomerForAppointment(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            
            var data = e.SelectedItem as Customer;
            Application.Current.Properties["SelectedCustomerId"] = data.Id;
            Navigation.PushAsync(new CalendarCreateAppointmentPage(objaddAppointment));
            ((ListView)sender).SelectedItem = null;
        }

        public ObservableCollection<Customer> GetAllCustomer()
        {
            try
            {
                string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
                var result = PostData("GET", "", apiURL);
                ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                CustomersList.ItemsSource = ListOfCustomer;
                return ListOfCustomer;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public void SearchCustomersByTerm()
        {
            try
            {
                var result = "";
                string apiUrl = Application.Current.Properties["DomainUrl"] + "api/customer/SearchCustomersByTerm?companyId=" + Application.Current.Properties["CompanyId"] + "&searchTerm=" + CustomerSearchBar.Text;
                var httpWebRequest = HttpWebRequest.Create(apiUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                var httpResponse = httpWebRequest.GetResponse();
                using (var StreamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = StreamReader.ReadToEnd();
                }
                ObservableCollection<Customer> ListOfCustomer = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(result);
                CustomersList.ItemsSource = ListOfCustomer;
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