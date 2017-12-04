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
        Register reg;
        public RegPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = reg = new Register();
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

        private async Task<string> RegisterMethod(string url, Register item)
        {
            string result = "";
            try
            {
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(item);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);



                //HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp("http://bookingmanager20-001-site1.btempurl.com/api/companyregistration/CreateAccount");
                //httpWebRequest.Method = "POST";
                //httpWebRequest.ContentType = "application/json";


                //var StreamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                //StreamWriter.Write(data);
                //StreamWriter.Flush();

                //var StreamReader = new StreamReader();




                //var StreamReader = new StreamReader(response.GetResponsetrtea);
              
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