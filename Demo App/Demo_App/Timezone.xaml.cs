using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Timezone : ContentPage
	{
        public string SelectedTimeZone = null;
		public Timezone ()
		{
			InitializeComponent ();
            var Zone = TimeZoneInfo.GetSystemTimeZones();
            ListofTimeZone.ItemsSource = Zone;
        }

        private void SelectTimeZone(object sender,SelectedItemChangedEventArgs e)
        {
            SelectedTimeZone = e.SelectedItem.ToString();
        }

        private void SaveTimeZone(object sender,EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountUser(SelectedTimeZone));
        }
        private void SearchTimeZoneByText(object sender, TextChangedEventArgs e)
        {
            try
            {
                //thats all you need to make a search  
                var Zonny = TimeZoneInfo.GetSystemTimeZones();
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    ListofTimeZone.ItemsSource = Zonny;
                }

                else
                {
                    var listfilter = Zonny.Where(x => x.DisplayName.ToLower().StartsWith(e.NewTextValue)).ToList();

                    ListofTimeZone.ItemsSource = listfilter;


                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }


    }
}