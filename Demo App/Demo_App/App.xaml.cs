using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Demo_App
{
	public partial class App : Application
	{
		public App ()
		{          
            InitializeComponent();        
            SetMainPage();           
        }

        public static void SetMainPage()
        {
            new NavigationPage(new RegPage());
            new NavigationPage(new LoginPage());
            new NavigationPage(new SetAppointmentPage());
             Current.MainPage = new NavigationPage(new WelcomePage());

            //Current.MainPage = new NavigationPage(new ServicesProviderPage(40));

          // Current.MainPage = new NavigationPage(new BusinessHoursPage(21));

             //Current.MainPage = new NavigationPage(new TestPage());

            //Current.MainPage = new NavigationPage(new SetAppointmentPage());

            //Current.MainPage.On<Xamarin.Forms.PlatformConfiguration.Windows>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            //Current.MainPage = new WelcomePage();

            //Current.MainPage = new TabbedPage
            //{
            //    Children =
            //    {
            //        new NavigationPage(new LoginPage())
            //        {
            //            Title = "Login",
            //            Icon = Device.OnPlatform<string>("Icon.png",null,null),
            //        },
            //        new NavigationPage(new RegPage())
            //        {
            //            Title = "Registration",
            //            Icon = Device.OnPlatform<string>("Icon.png",null,null)
            //        }
            //    }
            //};
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
