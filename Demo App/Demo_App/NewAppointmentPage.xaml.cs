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
	public partial class NewAppointmentPage : ContentPage
	{
		public NewAppointmentPage (AddAppointments objAddAppointments)
		{
			InitializeComponent ();
            BindingContext = objAddAppointments;
            string[] Data = { "No Label", "Pending", "Confirmed", "Done", "No-Show", "Paid", "Running Late", "Custom Label" };
            for (var i = 0; i < Data.Length; i++)
            {
                newAppointmentsPicker.Items.Add(Data[i]);
            }
        }
	}
}