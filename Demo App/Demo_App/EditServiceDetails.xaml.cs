using Demo_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
	public partial class EditServiceDetails : ContentPage
	{
        string ServiceId;
		public EditServiceDetails (ServiceDetails service)
		{
			InitializeComponent ();
            ServiceId = service.Id.ToString();
            EditServiceName.Text = service.Name;
            EditServiceCost.Text = service.Cost.ToString();
            int minutes = Convert.ToInt32(service.DurationInMinutes.Split(' ')[0]);         
            TimeSpan serviceDuration = new TimeSpan(0,minutes, 0);
            EditServiceDuration.Time = serviceDuration;
            int bufferminutes = Convert.ToInt32(service.BufferTimeInMinutes.Split(' ')[0]);
            TimeSpan serviceBufferDuration = new TimeSpan(0, bufferminutes, 0);
            EditServiceBufferTime.Time = serviceBufferDuration;
        }

        public void EditServiceInformation()
        {
            Service obj = new Service();
            obj.Id = Convert.ToInt32(ServiceId);
            obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
            obj.Name = EditServiceName.Text;
            obj.CategoryName = "";
            obj.CategoryId = 0;
            obj.DurationInMinutes = Convert.ToInt32(EditServiceDuration.Time.TotalMinutes);
            obj.DurationInHours = 0;
            if (EditServiceCost.Text.Contains("$"))
            {
                var index = EditServiceCost.Text.IndexOf("$");              
                obj.Cost = Convert.ToDouble(EditServiceCost.Text.Remove(index, 1));
            }
            else
            {
                obj.Cost = Convert.ToDouble(EditServiceCost.Text);
            }                     
            obj.Currency = "";
            obj.Colour = "";
            obj.Buffer = Convert.ToInt32(EditServiceBufferTime.Time.TotalMinutes);
            obj.CreationDate = "2017-11-08T12:19:27.628Z";
            obj.Description = "";

            var apiUrl=Application.Current.Properties["DomainUrl"] + "api/services/UpdateService";
            var serailizeddata = JsonConvert.SerializeObject(obj);

           var result= PostData("POST", serailizeddata, apiUrl);

            Navigation.PushAsync(new ServiceDetailsPage(obj));
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