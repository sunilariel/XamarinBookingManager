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
	public partial class SettingPage : ContentPage
	{
		public SettingPage ()
		{
            InitializeComponent ();
            //var table = new TableView();
            //table.Intent = TableIntent.Settings;
            //var layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            //layout.Children.Add(new Image() { Source = "Icon.png" });
            //layout.Children.Add(new Label()
            //{
            //    Text = "Business Hours",
            //    TextColor = Color.Black,
            //    VerticalOptions = LayoutOptions.Center
            //});
            //layout.Children.Add(new Label()
            //{
            //    Text = ">",
            //    TextColor = Color.FromHex("#503026"),
            //    VerticalOptions = LayoutOptions.Center,
            //    HorizontalOptions = LayoutOptions.EndAndExpand
            //});
            //table.Root = new TableRoot()
            //{
            //    new TableSection("GettingStarted")
            //    {
            //        new ViewCell(){View=layout}
            //    }
            //};
            //Content = table;
        }

        private void business_Hours(object sender, EventArgs args)
        {
            Application.Current.MainPage.Navigation.PushAsync(new BusinessHoursPage("SettingsCompanyHours"));
        }

        //private void appointment_slot(object sender, EventArgs args)
        //{
        //    Navigation.PushAsync(new AppointmentSlotPage());
        //}

        private void Currency_Tapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Currency());
        }

        private void Timezone_Tapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Timezone());
        }
    }
}