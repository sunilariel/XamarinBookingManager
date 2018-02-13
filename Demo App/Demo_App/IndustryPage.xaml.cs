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
	public partial class IndustryPage : ContentPage
	{
        Dictionary<string, int> Industrylist = null;
        public string SelectedIndustry = null;
        public IndustryPage ()
		{
			InitializeComponent ();
            //Industrylist = new Dictionary<string, int>
            //{
            //   { "Hair Salon/Barbershop", 1 }, { "Nail Salon", 2 }, { "Computers/Technology/IT", 3 }, { "Spa/Massage/Waxing", 4 },               
            //};
            List<string> industryList = new List<string>();
            industryList.Add("Hair Salon/Barbershop");
            industryList.Add("Nail Salon");
            industryList.Add("Computers/Technology/IT");
            industryList.Add("Spa/Massage/Waxing");
            //foreach (var item in Industrylist)
            //{
            //    industryList.Add(item.Key);
            //}
            ListofIndustry.ItemsSource = industryList;
        }

        private void SelectIndustry(object sender,SelectedItemChangedEventArgs e)
        {
            try
            {
                SelectedIndustry = e.SelectedItem.ToString();
                Application.Current.Properties["IndustryName"] = SelectedIndustry;
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        private void SaveSelectedIndustry(object sender,SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new CreateAccountUser());
        }


    }
}