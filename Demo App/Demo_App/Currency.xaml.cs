using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Currency : ContentPage
    {
        CurrencyAndCountryList obj1 = null;
        List<CurrencyAndCountryList> obj = new List<CurrencyAndCountryList>();
        public string CurrencySelected = null;
        public Currency()
        {
            InitializeComponent();
            var data = GetCountries();
            foreach (var item in data)
            {
                string currSymbol;
                string symbol = TryGetCurrencySymbol(item.Value, out currSymbol);
                obj1 = new CurrencyAndCountryList()
                {
                    Text = item.Value,
                    Value = item.Text,
                    Symbol = symbol,
                    currency=symbol+","+ item.Text+","+item.Value
                };
                obj.Add(obj1);
            }
            ListofCurrency.ItemsSource = obj;
        }

        public struct DDLStructure
        {
            public String Value { get; set; }
            public String Text { get; set; }

        }
        public class CurrencyAndCountryList
        {
            public String Value { get; set; }
            public String Text { get; set; }
            public String Symbol { get; set; }
            public String currency { get; set; }
        }
        public List<DDLStructure> GetCountries()
        {

            var region = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                            .Select(x => new RegionInfo(x.LCID));

            var countries = (from x in region
                             select new DDLStructure() { Value = x.ThreeLetterISORegionName, Text = x.CurrencyEnglishName })
                         .Distinct()
                         .OrderBy(x => x.Text)
                         .ToList<DDLStructure>();

            return countries;
        }


        public string TryGetCurrencySymbol(string ThreeLetterISORegionName, out string symbol)
        {           
                symbol = CultureInfo
                    .GetCultures(CultureTypes.AllCultures)
                    .Where(c => !c.IsNeutralCulture)
                    .Select(culture =>
                    {
                        try
                        {
                            return new RegionInfo(culture.LCID);
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    .Where(ri => ri != null && ri.ThreeLetterISORegionName == ThreeLetterISORegionName)
                    .Select(ri => ri.CurrencySymbol)
                    .FirstOrDefault();
                return symbol;           
        }
      
        private void SelectedCurrency(object sender,SelectedItemChangedEventArgs e)
        {
            CurrencySelected = e.SelectedItem.ToString();            
        }
        private void SaveCurrency(object sender,EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountUser(CurrencySelected));
        }
        private void SearchCurrencyByTerm(object sender, TextChangedEventArgs e)
        {
            try
            {
                //thats all you need to make a search                 
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    ListofCurrency.ItemsSource = obj;
                }

                else
                {
                    var listfilter = obj.Where(x => x.Text.ToLower().StartsWith(e.NewTextValue)).ToList();

                    ListofCurrency.ItemsSource = listfilter;


                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}