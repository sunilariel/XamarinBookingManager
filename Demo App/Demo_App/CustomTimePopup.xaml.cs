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
	public partial class CustomTimePopup : PopupPage
    {
        public ObservableCollection<object> Time { get; set; }

        //Minute is the collection of minute numbers

        public ObservableCollection<object> Minute;

        //Hour is the collection of hour numbers

        public ObservableCollection<object> Hour;

        //Format is the collection of AM and PM

        public ObservableCollection<object> Format;
        public CustomTimePopup ()
		{
			InitializeComponent ();
            //PopulateTimeCollection();


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

        private void PopulateTimeCollection()
        {

            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Format = new ObservableCollection<object>();
            Time = new ObservableCollection<object>();

            //Populate Hour
            for (int i = 0; i <= 11; i++)
            {
                Hour.Add(i.ToString());
            }

            //Populate Minute
            for (int j = 0; j < 59; j++)
            {
                if (j < 10)
                {
                    Minute.Add("0" + j);
                }
                else
                    Minute.Add(j.ToString());
            }

            //Populate Format
            Format.Add("AM");
            Format.Add("PM");
            Time.Add(Hour);
            Time.Add(Minute);
            Time.Add(Format);           
        }

        private void FromClick(object sender,EventArgs e)
        {
            PopulateTimeCollection();
        }
        private void ToClick(object sender, EventArgs e)
        {
            FromLabel.BackgroundColor = Color.White;
            ToLabel.BackgroundColor = Color.Blue;
            PopulateTimeCollection();
        }
    }
}