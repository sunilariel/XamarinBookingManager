using Android.Widget;
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
	public partial class AppointmentSlotPage : ContentPage
	{

		public AppointmentSlotPage ()
		{
			InitializeComponent ();
            string[] hours = { "00 hours", "01 hours", "02 hours", "03 hours", "04 hours", "05 hours", "06 hours", "07 hours", "08 hours", "09 hours", "10 hours", "11 hours", "12 hours", "13 hours", "14 hours", "15 hours", "16 hours", "17 hours", "18 hours", "19 hours", "20 hours", "21 hours", "22 hours", "23 hours" };
            string[] mins = { "00 mins", "05 mins", "10 mins", "15 mins", "20 mins", "25 mins", "30 mins", "35 mins", "40 mins", "45 mins", "50 mins", "55 mins" };
            for (var i = 0; i < hours.Length; i++)
            {
                pickHour.Items.Add(hours[i]);
            }
            for (var i = 0; i < mins.Length; i++)
            {
                pickMins.Items.Add(mins[i]);
            }
        }

        //private void onSelectedIndexChanged(object sender, EventArgs args)
        //{
        //    var name = mainPicker.Items[mainPicker.SelectedIndex];
        //    DisplayAlert(name, "seleted value", "ok");
        //}

    }
}