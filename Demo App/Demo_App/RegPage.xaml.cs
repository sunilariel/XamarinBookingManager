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
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

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
        
        public async void OnRegClicked(object sender, EventArgs args)
        {
            if (!IsValid()) return;
            
            var DomainUrl = "http://bookingmanager29-001-site1.gtempurl.com/";
            
            Application.Current.Properties["DomainUrl"] = DomainUrl;
            var regdata = reg;
            string email = reg.Email;
           var response= UserExist(email);
            if (response == "true")
            {
                var confirmed = DisplayAlert("Confirm", "User is already exist", "ok");

            }
            else
            {
                string RegisterUrl = Application.Current.Properties["DomainUrl"] + "api/companyregistration/CreateAccount";
                var result = await RegisterMethod(RegisterUrl, regdata);
            }
        }

        private async Task<string> RegisterMethod(string url, RequestData item)
        {
            string result="";
            try
            {
                //HttpClient client = new HttpClient();

                RequestData objRequestData = new RequestData();
                //objRequestData.Id = -1;
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
                        dynamic ResponseValue = jsonObject["ReturnObject"]["CompanyId"];
                        dynamic ResponseToken = jsonObject["ReturnObject"]["AuthToken"];
                        Application.Current.Properties["Token"] = ResponseToken.Value;
                        Application.Current.Properties["CompanyId"] = ResponseValue.Value;
                        await Navigation.PushAsync(new CreateAccountUser(objRequestData));
                    }

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
               
            }
            return result;
        }
        public void OnLabelTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LoginPage());
            // Your code here
            // Example:
            // DisplayAlert("Message", "You clicked on the label", "OK");
        }
        public string UserExist(string email)
        {
            try
            {
                string apiUrl = Application.Current.Properties["DomainUrl"] + "api/companyregistration/AlreadyExistsCompany?email=" + email;
                string result = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return result;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        private void redirectToPage()
        {
            {
                try
                {
                    Navigation.PushAsync(new CreateAccountUser());
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(nametxt.Text))
            {
                DisplayAlert("Error", "Enter Your Name", "OK");
                return false;
            }
           

            if (string.IsNullOrEmpty(Emailtxt.Text))
            {
                DisplayAlert("Error", "Enter Email Address", "OK");
                return false;
            }
            if (!IsEmailValid(Emailtxt.Text.Trim()))
            {
                DisplayAlert("Error", "Please enter correct email address", "Ok");
                return false;
            }

            if (string.IsNullOrEmpty(Passwordtxt.Text))
            {
                DisplayAlert("Error", "Enter Password", "OK");
                return false;
            }

            //if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            //{
            //    DisplayAlert("Error", "The password and confirmation password do not match.", "OK");
            //    return false;
            //}

            return true;
        }

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
        

    }
}