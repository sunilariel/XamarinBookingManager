﻿using System;
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
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCustomerPage : ContentPage
	{
		public NewCustomerPage ()
		{
			InitializeComponent ();
		}
        private void AddressClick(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new AddressPage());
        }


        public void AddCustomer()
        {                    
            var CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            Customer obj = new Customer();
            obj.Id = 0;
            obj.CompanyId = CompanyId;
            obj.UserName = CustomerEmail.Text;
            obj.Password = "123456";
            obj.FirstName = CustomerName.Text;
            obj.LastName = "";
            obj.Address = CustomerAddress.Text;
            obj.Email = CustomerEmail.Text;
            obj.TelephoneNo = CustomerPhoneNumber.Text;
            obj.CreationDate = Convert.ToString(DateTime.Now);
            var data = JsonConvert.SerializeObject(obj);
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/clientreservation/CreateCustomer";
           var result = PostData("POST", data, apiUrl);

            dynamic successData = JObject.Parse(result);
            var msg = Convert.ToString(successData.Message);
            DisplayAlert("Success", msg, "ok");
            Navigation.PushAsync(new CustomerPage());
        }

        public string DeleteCustomer(string CompanyId, string CustomerId)
        {
            var apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/DeleteCustomer?companyId=" + "CompanyId" + "&customerId=" + "CustomerId";
            var result = PostData("DELETE", "", apiUrl);
            return result;
        }


        public string GetAllCustomer()
        {
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomers?companyId=" + Application.Current.Properties["CompanyId"];
            var result = PostData("GET", "", apiURL);
            return result;
        }
      
              
        public string GetSelectedService(string ServiceId)
        {
          
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/services/GetServiceById?id=" + ServiceId;
            var result = PostData("GET", "", apiURL);            
            return result;
        }

        public string GetCompanyDetails(string companyId)
        {
            // int Id = Convert.ToInt32(CompanyId);
            string apiURL = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyDetails?companyId=" + companyId;
            var result = PostData("GET", "", apiURL);           
            return result;
        }
       
        public string SetStatusOfAppointment(string status, string BookingId)
        {         
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/SetStatus?status=" + status + "&bookingId=" + BookingId;
                var result = PostData("POST", "", apiUrl);               
                return result;     
        }

        public string UpdateBooking(BookAppointment appointment)
        {            
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/UpdateBooking";
                var JsonString = JsonConvert.SerializeObject(appointment);

                var result = PostData("POST", JsonString, apiUrl);                          
                return result;                  
        }
      
        public string GetCustomerStats(string CompanyId, string CustomerId, string Year, string Month)
        {
                   
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/booking/GetCustomerStats?companyId=" + CompanyId + "&customerId=" + CustomerId + "&year=" + Year + "&month=" + Month;
                var result = PostData("GET", "", apiUrl);               
                return result;
           
        }

        public string AddCustomerNote(Notes notesdetail)
        {
         
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/AddNote";
                var JsonString = JsonConvert.SerializeObject(notesdetail);
                var result = PostData("POST", JsonString, apiUrl);             
                return result;
            
        }

        public string DeleteCustomerNote(string CompanyId, string CustomerNoteId)
        {           
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/DeleteCustomerNote?companyId=" + CompanyId + "&customerNoteId=" + CustomerNoteId;
                var result = PostData("DELETE", "", apiUrl);             
                return result;           
        }

        public string GetCustomerNotes(string CompanyId, string CustomerId)
        {           
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + CustomerId;
                var result = PostData("GET","", apiUrl);              
                return result;           
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

                if (SerializedData != null)
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