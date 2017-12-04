using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.IO;
using System.Globalization;
using System.Net;


namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
		public NotificationPage ()
		{
			InitializeComponent ();
		}


        public string GetWeeksSchedule(string CompanyId)
        {           
               
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/dashboard/GetWeeksSchedule?companyId=" + CompanyId;
                var result = PostData("GET","", apiUrl);
            
                return result;          
        }

        public string GetWeeksActivitySummary(string CompanyId)
        {                    
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/dashboard/GetWeeksActivitySummary?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);               
                return result;          
        }

        public string GetCurrentWeeksRevenueSummary(string CompanyId)
        {                        
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/dashboard/GetCurrentWeeksRevenueSummary?companyId=" + CompanyId;
                var result = PostData("GET", "", apiUrl);               
                return result;
           
        }

        public string GetCustomerById(string CustomerId)
        {        
               
                string apiUrl = Application.Current.Properties["DomainUrl"] + "/api/customer/GetCustomerById?id=" + CustomerId;
                var result = PostData("GET", "", apiUrl);              
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