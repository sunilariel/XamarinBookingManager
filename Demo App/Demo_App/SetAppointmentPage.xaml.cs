//using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Telerik.XamarinForms.Input;
using Syncfusion.SfSchedule.XForms;
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
        #region GlobalFields
        public int i = 0;
        //public int y = 0;
        public bool IsStaffListVisible = false;
        public bool IsFloatActionRotated = false;
        public int centerHeight = 0;
        public int EmpID;
        string EmpName;
        string selectesPageN;
        //string PageName;
        string DateDaysofWeek;
        ObservableCollection<Staff> AllStaffList = new ObservableCollection<Staff>();
        #endregion

        public SetAppointmentPage(string selectedName, string empName, string weekDateDays)
        {

            DateDaysofWeek = weekDateDays;
            selectesPageN = selectedName;
            InitializeComponent();
            GetStaff();
            EmpName = empName;
            if (selectedName == "selectedPageCustomer")
            {
                var pages = new CustomerPage();
                Placeholder.Content = pages.Content;
                //this.Title = "Customer";
                shedulerStaff.Text = "Customer";
                dropdownArrow.IsVisible = false;
                CalendarIconButton.IsVisible = false;
                // this.ToolbarItems.Remove(CalendarIconButton);
            }
            else if (selectedName == "CustomerPage")
            {
                var page = new CustomerPage();
                Placeholder.Content = page.Content;
                //this.Title = "Customer";
                shedulerStaff.Text = "Customer";
                dropdownArrow.IsVisible = false;
                CalendarIconButton.IsVisible = false;
            }
            else if (selectedName == "ActivityPage")
            {
                var page = new ActivityPage();
                Placeholder.Content = page.Content;
                //this.Title = "Activity";
                shedulerStaff.Text = "Activity";
                dropdownArrow.IsVisible = false;
                CalendarIconButton.IsVisible = false;
            }
            else if (selectedName == "AccountPage")
            {
                //Application.Current.Properties["FloatingAccountPageName"] = "AccountPage";
                var page = new AccountPage();
                Placeholder.Content = page.Content;
                //this.Title = "Account";
                shedulerStaff.Text = "Account";
                dropdownArrow.IsVisible = false;
                CalendarIconButton.IsVisible = false;
            }
            else
            {
                var page = new CalenderPage(DateDaysofWeek);
                Placeholder.Content = page.Content;
                this.Title = "Calender";
                if (empName != "")
                {
                    shedulerStaff.Text = empName;
                }
                else
                {
                    shedulerStaff.Text = "All Schedules";
                }

                Application.Current.Properties["FloatingCalenderPageName"] = "CalenderPage";
                
                middleF.HeightRequest = App.ScreenHeight - 180;
            }


        }

        //SchedulerAllStaff
        private void SchedulerAllStaff(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedStaff = e.SelectedItem as Staff;
            //EmpID = Convert.ToInt32(selectedStaff.Id);
            //EmpName = selectedStaff.FirstName;

            Application.Current.Properties.Remove("SelectedEmpId");
            Application.Current.Properties.Remove("LastSelectedStaff");

            //shedulerStaff.Text = "All Scheduler";
            //shedulerStaff.Text = selectedStaff.FirstName;
            //Application.Current.Properties["SelectedEmpId"] = selectedStaff.Id;
            //Application.Current.Properties["LastSelectedStaff"] = selectedStaff.FirstName;
            dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
            listData.IsVisible = false;
            AlllistData.IsVisible = false;
            Placeholder.IsVisible = true;
            Navigation.PushAsync(new SetAppointmentPage("", "", DateDaysofWeek));

        }


        private void StaffSelectedForAppointment(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedStaff = e.SelectedItem as Staff;
            EmpID = Convert.ToInt32(selectedStaff.Id);
            EmpName = selectedStaff.FirstName;
            shedulerStaff.Text = "All Schedules";
            shedulerStaff.Text = selectedStaff.FirstName;
            Application.Current.Properties["SelectedEmpId"] = selectedStaff.Id;
            Application.Current.Properties["LastSelectedStaff"] = selectedStaff.FirstName;
            dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
            listData.IsVisible = false;
            AlllistData.IsVisible = false;
            Placeholder.IsVisible = true;
            Navigation.PushAsync(new SetAppointmentPage("", EmpName, DateDaysofWeek));

        }

        void Icon1_Tapped(object sender, EventArgs args)
        {
            var page = new CalenderPage(DateDaysofWeek);
            Placeholder.Content = page.Content;
            //y++;
            CALENDERLabelColor.TextColor= Xamarin.Forms.Color.MediumTurquoise;
            CUSTOMERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACTIVITYLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACCOUNTLabelColor.TextColor = Xamarin.Forms.Color.Black;
            icon1Enable.IsEnabled = false;
            icon2Enable.IsEnabled = true;
            icon3Enable.IsEnabled = true;
            icon4Enable.IsEnabled = true;
            Application.Current.Properties.Remove("FloatingCustomerPageName");
            Application.Current.Properties.Remove("FloatingActivityPageName");
            Application.Current.Properties.Remove("FloatingAccountPageName");

            Application.Current.Properties["FloatingCalenderPageName"] = "CalenderPage";
            
            if (Application.Current.Properties.ContainsKey("LastSelectedStaff") == true)
            {
                shedulerStaff.Text = Application.Current.Properties["LastSelectedStaff"].ToString();
            }
            else
            {
                shedulerStaff.Text = "All Schedules";
            }

            dropdownArrow.IsVisible = true;
            CalendarIconButton.IsVisible = true;
        }

        void Icon2_Tapped(object sender, EventArgs args)
        {            
            var page = new CustomerPage();
            Placeholder.Content = page.Content;
            CALENDERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            CUSTOMERLabelColor.TextColor = Xamarin.Forms.Color.MediumTurquoise;
            ACTIVITYLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACCOUNTLabelColor.TextColor = Xamarin.Forms.Color.Black;
            icon1Enable.IsEnabled = true;
            icon2Enable.IsEnabled = false;
            icon3Enable.IsEnabled = true;
            icon4Enable.IsEnabled = true;
            Application.Current.Properties.Remove("FloatingCalenderPageName");
            Application.Current.Properties.Remove("FloatingActivityPageName");
            Application.Current.Properties.Remove("FloatingAccountPageName");
            Application.Current.Properties["FloatingCustomerPageName"] = "CustomerPage";
            
            //this.Title = "Customer";
            shedulerStaff.Text = "Customer";
            dropdownArrow.IsVisible = false;
            CalendarIconButton.IsVisible = false;
            // this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon3_Tapped(object sender, EventArgs args)
        {
            var page = new ActivityPage();
            Placeholder.Content = page.Content;
            CALENDERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            CUSTOMERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACTIVITYLabelColor.TextColor = Xamarin.Forms.Color.MediumTurquoise;
            ACCOUNTLabelColor.TextColor = Xamarin.Forms.Color.Black;
            icon1Enable.IsEnabled = true;
            icon2Enable.IsEnabled = true;
            icon3Enable.IsEnabled = false;
            icon4Enable.IsEnabled = true;
            Application.Current.Properties.Remove("FloatingCalenderPageName");
            Application.Current.Properties.Remove("FloatingCustomerPageName");
            Application.Current.Properties.Remove("FloatingAccountPageName");
            Application.Current.Properties["FloatingActivityPageName"] = "ActivityPage";
           
            //this.Title = "Activity";
            shedulerStaff.Text = "Activity";
            dropdownArrow.IsVisible = false;
            CalendarIconButton.IsVisible = false;
            // this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon4_Tapped(object sender, EventArgs args)
        {
            var page = new AccountPage();
            Placeholder.Content = page.Content;
            CALENDERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            CUSTOMERLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACTIVITYLabelColor.TextColor = Xamarin.Forms.Color.Black;
            ACCOUNTLabelColor.TextColor = Xamarin.Forms.Color.MediumTurquoise;
            icon1Enable.IsEnabled = true;
            icon2Enable.IsEnabled = true;
            icon3Enable.IsEnabled = true;
            icon4Enable.IsEnabled = false;
            Application.Current.Properties.Remove("FloatingCalenderPageName");
            Application.Current.Properties.Remove("FloatingCustomerPageName");
            Application.Current.Properties.Remove("FloatingActivityPageName");
            Application.Current.Properties["FloatingAccountPageName"] = "AccountPage";
            
            //this.Title = "Account";
            shedulerStaff.Text = "Account";
            dropdownArrow.IsVisible = false;
            CalendarIconButton.IsVisible = false;
            //this.ToolbarItems.Remove(CalendarIconButton);
        }

        //void Icon5_Tapped(object sender, EventArgs args)
        //{
        //    var page = new MorePage();
        //    Placeholder.Content = page.Content;
        //    //this.Title = "More";
        //    shedulerStaff.Text = "More";
        //    dropdownArrow.IsVisible = false;
        //    CalendarIconButton.IsVisible = false;
        //    // this.ToolbarItems.Remove(CalendarIconButton);
        //}

        void Icon6_Tapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new CustomerPage());
        }

        void tbi_Clicked(object sender, EventArgs e)
        {
            SfSchedule sfSchedule = CalenderPage.getScheduleObj();

            //SfSchedule sfSchedule = new SfSchedule();
            if (i == 0)
            {
                if (sfSchedule != null)
                {
                    sfSchedule.IsVisible = true;
                    var CurrentDate = DateTime.Now;
                    DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                    sfSchedule.NavigateTo(SpecificDate);
                    sfSchedule.ScheduleView = ScheduleView.WeekView;
                    i = 1;
                    sfSchedule.ScheduleCellTapped += Schedulee_ScheduleCellTapped;

                }
            }
            else
            {
                var CurrentDate = DateTime.Now;
                DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
                sfSchedule.NavigateTo(SpecificDate);
                sfSchedule.ScheduleView = ScheduleView.MonthView;
                i = 0;
                sfSchedule.ScheduleCellTapped -= Schedulee_ScheduleCellTapped;
            }

        }
        //private async void OnOpenPupup(object sender, EventArgs e)
        //{
        //    IsFloatActionRotated = !IsFloatActionRotated;
        //    var page = new FloatingButtonPopup();
        //    //await Navigation.PushPopupAsync(page);
        //    if (IsFloatActionRotated)
        //    {
        //        await PopupNavigation.PushAsync(page);
        //        await floataction.RotateTo(45, 200, Easing.SinInOut);
        //        await floataction.RotateTo(0, 200, Easing.SinInOut);
        //    }
        //    else
        //    {
        //        //await PopupNavigation.PopAsync(true);
        //        await floataction.RotateTo(0, 200, Easing.SinInOut);
        //    }

        //}

        private void StaffDataPage(object sender, EventArgs e)
        {
            IsStaffListVisible = !IsStaffListVisible;
            if (IsStaffListVisible)
            {
                dropdownArrow.RotateTo(180, 200, Easing.SinInOut);
                AlllistData.IsVisible = true;
                listData.IsVisible = true;
                Placeholder.IsVisible = false;
            }
            else
            {
                dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
                AlllistData.IsVisible = false;
                listData.IsVisible = false;
                Placeholder.IsVisible = true;
            }

        }



        private void Schedulee_ScheduleCellTapped(object sender, ScheduleTappedEventArgs e)
        {

            Navigation.PushAsync(new GetAllocateServiceForEmployeePage(EmpID, "", ""));
        }

        public void GetStaff()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "/api/companyregistration/GetCompanyEmployees?companyId=" + CompanyId;
                var Method = "GET";

                var result = PostData(Method, null, Url);
                AllStaffList = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(result);
                StaffList.ItemsSource = AllStaffList;

            }
            catch (Exception e)
            {
                e.ToString();
            }
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

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var LogOut = await DisplayAlert("Log Out", "Are you sure you want to Log Out", "Log Out", "No");
                if (LogOut)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new WelcomePage());
                }
            });
            return true;
        }
    }
}