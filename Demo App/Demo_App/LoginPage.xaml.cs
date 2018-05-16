using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;

using Demo_App.Model;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Globalization;


namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        Login bindingValue;
        public LoginPage()
        {
            
            InitializeComponent();

           
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = bindingValue = new Login();
            var DomainUrl = "http://bookingmanager27-001-site1.itempurl.com/";

            Application.Current.Properties["DomainUrl"] = DomainUrl;

            
        }
        
       
        public async void OnLoginClicked(object sender, EventArgs args)
        {
            
            DependencyService.Get<IProgressInterface>().Show();
            await Task.Delay(5000);

            var loginData = bindingValue;
            string loginUrl = Application.Current.Properties["DomainUrl"] + "api/Authenticate/login";            
            var result = await logInMethod(loginUrl, loginData);

            
        }

        

        async void NavigateToRegisterPage(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RegPage());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        private async Task<string> logInMethod(string url, Login item)
        {
            string result = "";
            try
            {
                
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(item.userName + ":" + item.password);
                HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.ContentLength = data.Length;
                httpRequest.ProtocolVersion = HttpVersion.Version10;
                httpRequest.Timeout = 200000;
                string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(item.userName + ":" + item.password));
                httpRequest.Headers.Add("Authorization", "Basic " + encoded);
                //var content = new StringContent(data, Encoding.UTF8, "application/json");
                var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                streamWriter.Write(data);
                streamWriter.Close();
                var res = httpRequest.GetResponse();
                var response = (HttpWebResponse)httpRequest.GetResponse();
                var role = res.Headers["RoleType"];

                using (var StreamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = StreamReader.ReadToEnd();
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string token = res.Headers["Token"];
                    Application.Current.Properties["Token"] = token;

                    var CompanyId = res.Headers["CompanyId"];
                    Application.Current.Properties["CompanyId"] = CompanyId;
                    var EmployeeId = res.Headers["EmployeeId"];
                    Application.Current.Properties["EmployeeId"] = EmployeeId;

                    redirectToSetAppoitmentPage();
                    
                }
            }
            catch (Exception ex)
            {
                WrongCredentials();

            }
            return result;
        }

        private void redirectToSetAppoitmentPage()
        {
            {
                try
                {
                    DependencyService.Get<IProgressInterface>().Show();
                    Navigation.PushAsync(new SetAppointmentPage(""));
                    
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    
                }
            }

        }

        private async void WrongCredentials()
        {
            try
            {
                var answer = await DisplayAlert(null, "Sorry! Your credentials are wrong.", null, "ok");
                Debug.WriteLine("Answer: " + answer);
                DependencyService.Get<IProgressInterface>().Hide();
            }
            catch (Exception ex)
            {
                ex.ToString();
                DependencyService.Get<IProgressInterface>().Hide();
            }

        }

        private void Forgot_Password(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }

    }
}