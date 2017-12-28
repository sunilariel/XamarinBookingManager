using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo_App.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        RequestData reg;
        public RegPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = reg = new RequestData();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    this.Animate("", s => Layout(new Rectangle(((-1 + s) * Width), Y, Width, Height)), 16, 250, Easing.Linear, null, null);
        //}

        public async void OnRegClicked(object sender, EventArgs args)
        {
            var regdata = reg;
            string RegisterUrl = "http://bookingmanager20-001-site1.btempurl.com/api/companyregistration/CreateAccount";
            var result = await RegisterMethod(RegisterUrl, regdata);
        }

        private async Task<string> RegisterMethod(string url, RequestData item)
        {
            string result = "";
            try
            {
                HttpClient client = new HttpClient();

                RequestData objRequestData = new RequestData();
                objRequestData.Id = -1;
                objRequestData.Name = item.Name;
                objRequestData.Address = "aa";
                objRequestData.Email = item.Email;
                objRequestData.Telephone = "123654789";
                objRequestData.PostCode = "a";
                objRequestData.Website = "a";
                objRequestData.County = "aaa";
                objRequestData.Town = "aaaaa";
                objRequestData.Description = "aa";
                objRequestData.Password = item.Password;
                objRequestData.CreationDate = "2017-05-22T05:55:21.9148617+00:00";

                var data = JsonConvert.SerializeObject(objRequestData);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    //result = await response.Content.ReadAsStringAsync();
                    //var product = JsonConvert.DeserializeObject<Register>(result);
                    //successfulRegisterMsg();                    
                    redirectToLoginPage();
                }
            }
            catch (Exception ex)
            {
                var a = ex;
               
            }
            return result;
        }

        //private void successfulRegisterMsg()
        //{
        //   var answer = DisplayAlert(null, "Congratulation ! You are successfully register.", null, "ok");
        //    Debug.WriteLine("Answer: " + answer);
        //}

        private void redirectToLoginPage()
        {
            {
                try
                {
                    Navigation.PushAsync(new LoginPage());
                }
                catch (Exception ex)
                {
                }
            }

        }

       

    }
}