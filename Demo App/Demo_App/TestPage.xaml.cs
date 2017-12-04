using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;

using Demo_App.Model;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
 
	public partial class TestPage : ContentPage
	{
		public TestPage ()
		{
			InitializeComponent ();
            //listView.ItemsSource = TestModelView.listofTestModels;
            //var data = TestModelView.TestModelView_();
            //BindingContext = Items;

        }
        private void WednesdayToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
               // lblWednesday.TextColor = Color.Black;
            }
            else
            {
               // lblWednesday.TextColor = Color.Gray;
            }
        }
    }
}