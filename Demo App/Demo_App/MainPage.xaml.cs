using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Net.Http;

using Demo_App.Model;
using Newtonsoft.Json;

namespace Demo_App
{
    public partial class MainPage : TabbedPage
    {        
        Login bindingValue;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = bindingValue = new Login();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        var result = await this.DisplayAlert("Alert", "Do you realy want to exit", "yes", "no");
        //        if (result) await this.Navigation.PopAsync();
        //    });
        //    return true;
        //}



        async void OnLoginClicked(object sender, EventArgs args)
        {


            //await Navigation.PushAsync(new TabbedPage());
            var test = bindingValue;
            //var masterPage = this.Parent as TabbedPage;
            //masterPage.CurrentPage = masterPage.Children[1]; //Go to Page2
            //masterPage.CurrentPage.Focus();
            string loginUrl = "https://jsonplaceholder.typicode.com/posts";
            var result = await logInMethod(loginUrl, test);
        }

        async Task<int> logInMethod(string url, Login item)
        {
            HttpClient client = new HttpClient();
            //var data = JsonConvert.SerializeObject(item.userName+":"+item.password);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync(url, content);
            int result = 1;
            try
            {

                var response = await client.GetAsync(url);
                result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            return result;
        }



    }
}
