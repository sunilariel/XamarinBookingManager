using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Demo_App.Model;
using System.Collections.ObjectModel;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class FloatingButtonPopup : PopupPage
    {
        
        public AddAppointments objAddAppointment = null;

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
            if (Application.Current.Properties.ContainsKey("FloatingCalenderPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingCalenderPageName"]);
                Navigation.PushAsync(new NewCustomerPage(objAddAppointment, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingCustomerPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingCustomerPageName"]);
                Navigation.PushAsync(new NewCustomerPage(objAddAppointment, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingActivityPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingActivityPageName"]);
                Navigation.PushAsync(new NewCustomerPage(objAddAppointment, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingAccountPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingAccountPageName"]);
                Navigation.PushAsync(new NewCustomerPage(objAddAppointment, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }

            //var NewService = new NewCustomerPage(objAddAppointment,"");

        }

        private void AddNewServices()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            ObservableCollection<object> todaycollectionBuffer = new ObservableCollection<object>();
            //Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "ServiceCreateAfterLogin"));
            //this.IsVisible = false;
            //OnDisappearing();


            if (Application.Current.Properties.ContainsKey("FloatingCalenderPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingCalenderPageName"]);
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingCustomerPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingCustomerPageName"]);
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingActivityPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingActivityPageName"]);
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
            else if (Application.Current.Properties.ContainsKey("FloatingAccountPageName") == true)
            {
                var pageName = Convert.ToString(Application.Current.Properties["FloatingAccountPageName"]);
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, pageName));
                this.IsVisible = false;
                OnDisappearing();
            }
        }

        private void AddNewStaff(object sender,EventArgs e)
        {
            Navigation.PushAsync(new NewStaffPage("StaffCreateAfterLogin"));
            this.IsVisible = false;
            OnDisappearing();
        }


        private void AddServiceCategory(object sender,EventArgs e)
        {
            Navigation.PushAsync(new NewCategoryPage());
            this.IsVisible = false;
            OnDisappearing();
        }

    }
}