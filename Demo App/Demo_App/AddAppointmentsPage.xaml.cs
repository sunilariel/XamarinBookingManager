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
	public partial class AddAppointmentsPage : ContentPage
	{
		public AddAppointmentsPage ()
		{
			InitializeComponent ();
		}
        private void AppointmentsdetailsClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AppointmentDetailsPage());
        }
    }
}