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
	public partial class WelcomePage : ContentPage
	{       
        public WelcomePage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void NavigateToRegisterPage(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RegPage());
            }
            catch (Exception ex) {

            }
            
        }

        public void NavigateToLoginPage(object sender, EventArgs e) {
            try
            {
                Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex) {
            }
            
        }
    }
    
}