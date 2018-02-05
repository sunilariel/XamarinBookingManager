using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FloatingButtonPopup : PopupPage
    {
        public FloatingButtonPopup()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        //Method for animation child in PopupPage
        //Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        //Method for animation child in PopupPage
        //Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        //Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }

        private void AddNewCustomer(object sender, EventArgs args)
        {
            var NewService = new NewCustomerPage();
            Navigation.PushAsync(new NewCustomerPage());
            this.IsVisible = false;
            OnDisappearing();
        }

        private void AddNewServices()
        {
            //Navigation.PushAsync(new NewServicePage());
        }

        private void AddNewStaff(object sender,EventArgs e)
        {
            Navigation.PushAsync(new NewStaffPage());
            this.IsVisible = false;
            OnDisappearing();
        }
    }
}