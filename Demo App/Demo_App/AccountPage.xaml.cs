using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage : ContentPage
	{
        int CategoryID;
        public AccountPage ()
		{
            InitializeComponent();
		}

        private void Settings_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new SettingPage());
        }

        //private void PasscodeLock_Tapped(object sender, EventArgs args)
        //{
        //    Application.Current.MainPage.Navigation.PushAsync(new PasscodeLockPage());
        //}

        //private void Notification_Tapped(object sender, EventArgs args)
        //{
        //    Application.Current.MainPage.Navigation.PushAsync(new NotificationPage());
        //}

        private void ServiceCategories_Tapped(object sender, EventArgs args)
        {
            
            Application.Current.MainPage.Navigation.PushAsync(new ServiceCategoriesPage( CategoryID));
        }

        public async void LogOut_Tapped(object sender,EventArgs args)
        {
            var LogOut = await DisplayAlert("Log Out", "Are you sure you want to Log Out", "Log Out", "No");
            if (LogOut)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new WelcomePage());
            }
            else
            {
                //await Navigation.PushAsync(new StaffProfileDetailsPage());
            }

            //Application.Current.MainPage.Navigation.PushAsync(new WelcomePage());
        }

        private void Service_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ServicePage());
        }

        private void Staff_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new StaffPage());
            //Application.Current.MainPage.Navigation.PushAsync(new ServicesProviderPage(40));
        }
        
    }
}