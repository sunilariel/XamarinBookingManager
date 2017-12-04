using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MorePage : ContentPage
	{
		public MorePage ()
		{
			InitializeComponent();
		}

        private void Profile_Tapped(object sender, EventArgs args)
        {
           Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }
        private void About_Tapped(object sender, EventArgs args)
        {
           Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
        }
        private void Feedback_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new FeedbackPage());
        }
        private void Chat_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new SupportPage());
        }
        private void ImportStaff_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ImportStaffPage());
        }
        private void ImportCustomer_Tapped(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ImportCustomerPage());
        }
        private void Logout_Tapped(object sender, EventArgs args)
        {
            var result = DisplayAlert("Logout", "Are you sure want to logout?", "Yes", "Cancel");
            Debug.WriteLine("Answer: " + result);
        }
      
    }
}