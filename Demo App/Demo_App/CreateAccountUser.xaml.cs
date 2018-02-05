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
	public partial class CreateAccountUser : ContentPage
	{
		public CreateAccountUser ()
		{
			InitializeComponent ();
		}
        public CreateAccountUser(string Data)
        {
            InitializeComponent();
            TimeZoneLabel.Text = Data;
            //Currencylabel.Text = Data;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void GetTimeZoneClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new Timezone());
        }
        private void GetCurrencyClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new Currency());
        }
        private void GetIndustoryClick(object sender,EventArgs e)
        {
            Navigation.PushAsync(new IndustryPage());
        }

    }
}