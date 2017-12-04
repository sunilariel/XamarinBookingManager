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
	public partial class EditCustomerPage : ContentPage
	{
		public EditCustomerPage ()
		{
			InitializeComponent ();
		}
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && EditCustomer.HeightRequest > 30 && EditCustomer.HeightRequest < 151)
            {
                EditCustomer.HeightRequest = EditCustomer.HeightRequest + e.TotalY;
                if (EditCustomer.HeightRequest < 31)
                    EditCustomer.HeightRequest = 31;
                if (EditCustomer.HeightRequest > 150)
                    EditCustomer.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }

        private void AddressClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddressPage());
        }
    }
}