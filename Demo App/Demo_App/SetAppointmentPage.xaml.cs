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
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using Demo_App.Model;
using Newtonsoft.Json;

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
        #region GloblesFields
        public int i = 0;
        public int y = 0;
        public bool IsStaffListVisible = false;
        public bool IsFloatActionRotated = false;
        #endregion

        public SetAppointmentPage()
        {
            
            InitializeComponent();
           
                //CompanyId = Application.Current.Properties["CompanyId"].ToString();

            GetStaff();
            var page = new CalenderPage();
            Placeholder.Content = page.Content;
            this.Title = "Calender";
                 
        }

        void Icon1_Tapped(object sender, EventArgs args)
        {
            y++;
            var page = new CalenderPage();
            Placeholder.Content = page.Content;            
            this.Title = "Calender";          
            //if (!this.ToolbarItems.Contains(CalendarIconButton))
            //{

            //    this.ToolbarItems.Add(CalendarIconButton);
            //}
         
        }

        void Icon2_Tapped(object sender, EventArgs args)
        {
            var page = new CustomerPage();
            Placeholder.Content = page.Content;
            this.Title = "Customer";
           // this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon3_Tapped(object sender, EventArgs args)
        {
            var page = new ActivityPage();
            Placeholder.Content = page.Content;
            this.Title = "Activity";

           // this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon4_Tapped(object sender, EventArgs args)
        {
            var page = new AccountPage();
            Placeholder.Content = page.Content;
            this.Title = "Account";
            //this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon5_Tapped(object sender, EventArgs args)
        {
            var page = new MorePage();
            Placeholder.Content = page.Content;
            this.Title = "More";
           // this.ToolbarItems.Remove(CalendarIconButton);
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
  
        }
        private async void OnOpenPupup(object sender, EventArgs e)
        {
            IsFloatActionRotated = !IsFloatActionRotated;
            var page = new FloatingButtonPopup();
            //await Navigation.PushPopupAsync(page);
            if (IsFloatActionRotated)
            {
                await PopupNavigation.PushAsync(page);
                floataction.RotateTo(45, 200, Easing.SinInOut);
            }
            else {
                await PopupNavigation.PopAsync(true);
                floataction.RotateTo(0, 200, Easing.SinInOut);
            }
            
        }

        private void StaffDataPage(object sender,EventArgs e)
        {
            IsStaffListVisible = !IsStaffListVisible;
            if (IsStaffListVisible)
            {
                dropdownArrow.RotateTo(180, 200, Easing.SinInOut);
                listData.IsVisible = true;
                Placeholder.IsVisible = false;
            }
            else {
                dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                listData.IsVisible = false;
                Placeholder.IsVisible = true;
            }
            
        }

        private void StaffSelectedForAppointment(object sender,SelectedItemChangedEventArgs e)
        {
            //var selectedStaff = e.SelectedItem as Staff;            
            //shedulerStaff.Text = selectedStaff.ToString();
        }

        public void GetStaff()
        {
           
              var  CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, null, Url);
                ObservableCollection<Staff> AllStaffList = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(result);
                StaffList.ItemsSource = AllStaffList;
            
        }

        public string PostData(string Method, string SerializedData, string Url)
        {
            try
            {
                var result = "";
                HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(Url);
                httpRequest.Method = Method;
                httpRequest.ContentType = "application/json";
                httpRequest.ProtocolVersion = HttpVersion.Version10;
                httpRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                if (SerializedData != null)
                {
                    var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                    streamWriter.Write(SerializedData);
                    streamWriter.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var StreamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return result = StreamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}