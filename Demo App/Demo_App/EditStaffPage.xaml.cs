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
    public partial class EditStaffPage : ContentPage
    {
        public EditStaffPage()
        {
            InitializeComponent();
        }
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && Staffstack.HeightRequest > 30 && Staffstack.HeightRequest < 151)
            {
                Staffstack.HeightRequest = Staffstack.HeightRequest + e.TotalY;
                if (Staffstack.HeightRequest < 31)
                    Staffstack.HeightRequest = 31;
                if (Staffstack.HeightRequest > 150)
                    Staffstack.HeightRequest = 150;
            }
            //stack.HeightRequest = 20;
        }
        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}