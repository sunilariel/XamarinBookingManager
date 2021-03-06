﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo_App.Model;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountUser : ContentPage
    {
        string businessName;
        RequestData objRequestData = new RequestData();
        public CreateAccountUser()
        {
            try
            {
                InitializeComponent();


                

                BussinessNametxt.Text = Application.Current.Properties["BussinessNameName"].ToString();
                bussinessNotxt.Text= Application.Current.Properties["BussinessNo"].ToString();
                if (Application.Current.Properties.ContainsKey("IndustryName") == true)
                {
                    IndustryNameLabel.Text = Application.Current.Properties["IndustryName"].ToString();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public CreateAccountUser(RequestData req)
        {
            try
            {
                objRequestData = req;
                InitializeComponent();

                if (Application.Current.Properties.ContainsKey("IndustryName") == true)
                {
                    IndustryNameLabel.Text = Application.Current.Properties["IndustryName"].ToString();
                }
                //if (Application.Current.Properties.ContainsKey("Currency") == true)
                //{
                //    Currencylabel.Text = Application.Current.Properties["Currency"].ToString();
                //}
                //if (Application.Current.Properties.ContainsKey("TimeZone") == true)
                //{
                //    TimeZoneLabel.Text = Application.Current.Properties["TimeZone"].ToString();
                //}
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        //private void GetTimeZoneClick(object sender,EventArgs e)
        //{
        //    Navigation.PushAsync(new Timezone());
        //}
        //private void GetCurrencyClick(object sender,EventArgs e)
        //{
        //    Navigation.PushAsync(new Currency());
        //}
        private void GetIndustoryClick(object sender, EventArgs e)
        {
            Application.Current.Properties["BussinessNameName"] = BussinessNametxt.Text;
            Application.Current.Properties["BussinessNo"] = bussinessNotxt.Text;
            Navigation.PushAsync(new IndustryPage());
        }

        private async void NextClick(object sender, EventArgs e)
        {



            string RegisterUrl = "http://bookingmanager29-001-site1.gtempurl.com/api/companyregistration/CreateAccount";

            var req = objRequestData;
            var result = await RegisterMethod(RegisterUrl, req);
        }

        private async Task<string> RegisterMethod(string url, RequestData item)
        {
            string result = "";
            try
            {
                //HttpClient client = new HttpClient();

                objRequestData = new RequestData();
                objRequestData.Id = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                objRequestData.Name = BussinessNametxt.Text;
                objRequestData.Address = "aa";
                objRequestData.Email = item.Email;
                objRequestData.IndustryName = IndustryNameLabel.Text;
                objRequestData.Telephone = bussinessNotxt.Text;
                objRequestData.PostCode = "a";
                objRequestData.Website = "a";
                objRequestData.County = "aaa";
                objRequestData.Town = "aaaaa";
                objRequestData.Description = "aa";
                objRequestData.Password = item.Password;
                objRequestData.CreationDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                var data = JsonConvert.SerializeObject(objRequestData);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = content;

                using (var client = new HttpClient())
                {
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var strRes = await response.Content.ReadAsStringAsync();

                        var jsonObject = JObject.Parse(strRes);
                        await Navigation.PushAsync(new BusinessHoursPage("CompanyHours"));
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return result;
        }

    }
}