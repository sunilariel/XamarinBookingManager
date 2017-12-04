using Demo_App.Model;
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
	public partial class CutomerProfilePage : ContentPage
	{
        //string phonNumber;

        public CutomerProfilePage (Customer Cust)
		{
            //this.phonNumber = Cust.TelephoneNo;
             BindingContext = Cust;

            InitializeComponent ();
		}
        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && CustomerProfile.HeightRequest > 30 && CustomerProfile.HeightRequest < 151)
            {
                CustomerProfile.HeightRequest = CustomerProfile.HeightRequest + e.TotalY;
                if (CustomerProfile.HeightRequest < 31)
                    CustomerProfile.HeightRequest = 31;
                if (CustomerProfile.HeightRequest > 150)
                    CustomerProfile.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }

        private void AddNotesClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNotesPage());
        }
        private void AppointmentsClicks(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAppointmentsPage());
        }
        private void EditCustomerClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditCustomerPage());
        }
    }
}