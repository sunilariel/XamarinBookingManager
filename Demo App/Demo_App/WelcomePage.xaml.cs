using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo_App.Model;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();            
            NavigationPage.SetHasNavigationBar(this, false);           
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
        public void NavigateToLoginPage(object sender, EventArgs e)
        {
            try
            {                
                //DependencyService.Get<IProgressInterface>().Show();
                Navigation.PushAsync(new LoginPage());
                //Navigation.PushAsync(new FeedbackPage());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }       
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "want to exit?", "Yes", "No");
                if (result)
                {
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    //var activity = (Android.App.Activity)Forms.Context;
                    //activity.FinishAffinity();
                }
            });
            return true;
        }
    }
}