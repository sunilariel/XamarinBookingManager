using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telerik.XamarinForms.Input;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using Demo_App;
using Rg.Plugins.Popup.Services;

namespace Demo_App
{
    public class title
    {
        public string pageTitle { get; set; }
        public string titleCust { get; set; }
    }
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetAppointmentPage : ContentPage
	{
        public int  i=0;
        public int y = 0;
        //title bindingValue;
        public SetAppointmentPage()
        {
            InitializeComponent();
            var page = new CalenderPage();
            Placeholder.Content = page.Content;
            this.Title = "Calender";
            
      //var calendar = new RadCalendar();
            
            //calendar.ViewChanged += CalendarLoaded;
            //calendar.NativeControlLoaded += CalendarLoaded;

            //void CalendarLoaded(object sender, EventArgs args)
            // {
            //     (sender as RadCalendar).TrySetViewMode(CalendarViewMode.Day);
            // }
            //BindingContext = bindingValue = new title { pageTitle = "" };
        }

        void Icon1_Tapped(object sender, EventArgs args)
        {
            y++;
            var page = new CalenderPage();
            Placeholder.Content = page.Content;            
            this.Title = "Calender";
            if (!this.ToolbarItems.Contains(CalendarIconButton))
            {
                
                this.ToolbarItems.Add(CalendarIconButton);
            }
            //var isItemPresent = this.ToolbarItems.Where(s => s.Text == "plus").FirstOrDefault();
            //DisplayAlert("hell", isItemPresent.Text, "ok");            
            //    ToolbarItem tbi = new ToolbarItem();
            //    tbi.Text = "plus";
            //    tbi.Icon = "plus.png";
            //    tbi.Clicked += tbi_Clicked;
            //    this.ToolbarItems.Add(tbi);           


        }

        void Icon2_Tapped(object sender, EventArgs args)
        {
            var page = new CustomerPage();
            Placeholder.Content = page.Content;
            this.Title = "Customer";
            this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon3_Tapped(object sender, EventArgs args)
        {
            var page = new ActivityPage();
            Placeholder.Content = page.Content;
            this.Title = "Activity";

            this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon4_Tapped(object sender, EventArgs args)
        {
            var page = new AccountPage();
            Placeholder.Content = page.Content;
            this.Title = "Account";
            this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon5_Tapped(object sender, EventArgs args)
        {
            var page = new MorePage();
            Placeholder.Content = page.Content;
            this.Title = "More";
            this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon6_Tapped(object sender, EventArgs args)
        {          
            Navigation.PushAsync(new CustomerPage());
        }

        void tbi_Clicked(object sender, EventArgs e)
        {                  
            SfSchedule sfSchedule = CalenderPage.getScheduleObj();
            if (i == 0)
            {
                if (sfSchedule != null)
                {
                    var CurrentDate = DateTime.Now;
                    DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                    sfSchedule.NavigateTo(SpecificDate);
                    sfSchedule.ScheduleView = ScheduleView.WeekView;
                    i = 1;

                }
            }
            else
            {
                var CurrentDate = DateTime.Now;
                DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                sfSchedule.NavigateTo(SpecificDate);
                sfSchedule.ScheduleView = ScheduleView.MonthView;
                i = 0;
            }

            //var calendar = new RadCalendar();
            //calendar.NativeControlLoaded += CalendarLoaded;
            //void CalendarLoaded(object sender, EventArgs args)
            //{
            //    (sender as RadCalendar).TrySetViewMode(CalendarViewMode.Day);
            //}

            //calendar.TrySetViewMode(CalendarViewMode.Day);           
            //Navigation.PushAsync(new CustomerPage());



            //i++;

            
        }
        private async void OnOpenPupup(object sender, EventArgs e)
        {
            var page = new FloatingButtonPopup();
            //await Navigation.PushPopupAsync(page);
            await PopupNavigation.PushAsync(page);
            floataction.RotateTo(45, 200, Easing.SinInOut);
        }
    }
}