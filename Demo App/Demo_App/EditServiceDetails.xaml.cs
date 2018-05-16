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
    public partial class EditServiceDetails : ContentPage
    {
        string DurationOfService = "";
        string ServiceId;
        string EserviceName;
        public EditServiceDetails(ServiceDetails service, ObservableCollection<object> todaycollection, ObservableCollection<object> todaycollectionBuffer)
        {
            try
            {
                Application.Current.Properties["EserviceId"] = service.Id.ToString();
               
                Application.Current.Properties["EserviceBufferTime"] = service.BufferTimeInMinutes;


                InitializeComponent();
                ServiceId = service.Id.ToString();
                EserviceName = service.Name;

                EditserviceProfileTitle.Text = service.Name;
                EditServiceName.Text = service.Name;
                EditServiceCost.Text = service.Cost.ToString();
                duration.Text = service.DurationInMinutes;
                //BufferTime.Text = service.BufferTimeInMinutes;

                if (todaycollection.Count > 0)
                {
                   
                    int Minutes = Convert.ToInt32(todaycollection[0]) * 60 + Convert.ToInt32(todaycollection[1]);
                    DurationOfService = Minutes + " min";

                   
                    duration.Text = DurationOfService;
                    
                }

                if (todaycollectionBuffer.Count > 0)
                {
                    int Min = Convert.ToInt32(todaycollectionBuffer[0]) * 60 + Convert.ToInt32(todaycollectionBuffer[1]);
                    string BufferTimeOfService = Min + " min";
                   
                    if (Application.Current.Properties["ServiceDurationTime"] != null)
                    {
                        duration.Text = Convert.ToString(Application.Current.Properties["ServiceDurationTime"]);
                    }
                   //BufferTime.Text = BufferTimeOfService;
                }


            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["EditServiceName"] = EditServiceName.Text;
            Application.Current.Properties["EditserviceProfileTitle"] = EditServiceName.Text;
            Application.Current.Properties["EditServiceCost"] = EditServiceCost.Text;
            //Application.Current.Properties["EserviceBufferTime"] = BufferTime.Text;
           
            date.IsOpen = !date.IsOpen;

        }
        private void BufferButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["EditServiceName"] = EditServiceName.Text;
            Application.Current.Properties["EditserviceProfileTitle"] = EditServiceName.Text;
            Application.Current.Properties["EditServiceCost"] = EditServiceCost.Text;
            Application.Current.Properties["ServiceDurationTime"] = duration.Text;
            
            //buffer.IsOpen = !buffer.IsOpen;

        }
        private void CrossClick(object sender, EventArgs e)
        {
            //for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 6; PageIndex--)
            //{
            //    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

            //}
            Navigation.PushAsync(new ServiceDetailsPage());
            //int pCount = Navigation.NavigationStack.Count();
            //for (int i = 0; i < pCount; i++)
            //{
            //    if (i == 3)
            //    {
            //        Navigation.RemovePage(Navigation.NavigationStack[i]);
            //    }
            //}
            //Navigation.PushAsync(new ServiceDetailsPage());
            
        }
        public void EditServiceInformation()
        {
            try
            {
                if (EditServiceName.Text == "")
                    return;

                if (duration.Text != "" | EditServiceCost.Text != "")
                {
                    string[] ServiceBufferTime = { };
                    string[] ServiceDuration = { };
                    string Duration = duration.Text;
                    //string bufferTime = BufferTime.Text;
                    if (Duration != null)
                    {
                        ServiceDuration = Duration.Split(' ');
                    }
                    //if (bufferTime != null)
                    //{
                    //    ServiceBufferTime = bufferTime.Split(' ');
                    //}

                    var minutes = ServiceDuration[0];
                    var totalMinutes = Convert.ToInt32(minutes);
                    var time = TimeSpan.FromMinutes(totalMinutes);

                    var durationHours = string.Format("{0:00}", (int)time.Hours);
                    var durationMinutes = string.Format("{0:00}", (int)time.Minutes);

                    Service obj = new Service();
                    obj.Id = Convert.ToInt32(ServiceId);
                    obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                    obj.Name = EditServiceName.Text;
                    obj.CategoryName = "";
                    obj.CategoryId = 0;
                    obj.DurationInMinutes = Convert.ToInt32(durationMinutes);
                    obj.DurationInHours = Convert.ToInt32(durationHours);

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
                    //obj.Buffer = Convert.ToInt32(ServiceBufferTime[0]);
                    obj.Buffer = 0;
                    obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.sszzz");
                    obj.Description = "";

                   

                    var apiUrl = Application.Current.Properties["DomainUrl"] + "api/services/UpdateService";
                    var serailizeddata = JsonConvert.SerializeObject(obj);

                    var result = PostData("POST", serailizeddata, apiUrl);

                    //for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                    //{
                    //    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

                    //}

                   

                    Navigation.PushAsync(new ServiceDetailsPage());


                    //int pCount = Navigation.NavigationStack.Count();

                    //for (int i = 0; i < pCount; i++)
                    //{
                    //    if (i == 3)
                    //    {
                    //        Navigation.RemovePage(Navigation.NavigationStack[i]);
                    //    }
                    //}
                }
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