using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Collections.ObjectModel;


namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryDetailsPage : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public CategoryDetailsPage ()
		{
			InitializeComponent ();
         
        }
    }
}