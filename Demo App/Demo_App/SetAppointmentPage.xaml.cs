﻿using Java.Util;
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
        #region GlobalFields
        public int i = 0;
        public int y = 0;
        public bool IsStaffListVisible = false;
        public bool IsFloatActionRotated = false;
        public int centerHeight = 0;
        public int EmpID;
        string EmpName = "";
        ObservableCollection<Staff> AllStaffList = new ObservableCollection<Staff>();
        #endregion

        public SetAppointmentPage()
        {
            
            InitializeComponent();           
            GetStaff();
            var page = new CalenderPage();
            Placeholder.Content = page.Content;
            this.Title = "Calender";
            middleF.HeightRequest = App.ScreenHeight-180;            
        }

        void Icon1_Tapped(object sender, EventArgs args)
        {
            y++;
            var page = new CalenderPage();
            Placeholder.Content = page.Content;
            if (Application.Current.Properties.ContainsKey("LastSelectedfStaff")==true) {
                shedulerStaff.Text = Application.Current.Properties["LastSelectedfStaff"].ToString();
            }
            else
            {
                shedulerStaff.Text = "All Scheduler";
            }
            
            dropdownArrow.IsVisible = true;
            CalendarIconButton.IsVisible = true;
        }

        void Icon2_Tapped(object sender, EventArgs args)
        {
            var page = new CustomerPage();
            Placeholder.Content = page.Content;
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
            //this.Title = "Account";
            shedulerStaff.Text = "Account";
            dropdownArrow.IsVisible = false;
            CalendarIconButton.IsVisible = false;
            //this.ToolbarItems.Remove(CalendarIconButton);
        }

        void Icon5_Tapped(object sender, EventArgs args)
        {
            var page = new MorePage();
            Placeholder.Content = page.Content;
            //this.Title = "More";
            shedulerStaff.Text = "More";
            dropdownArrow.IsVisible = false;
            CalendarIconButton.IsVisible = false;
            // this.ToolbarItems.Remove(CalendarIconButton);
        }

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
            var selectedStaff = e.SelectedItem as Staff;
            EmpID = Convert.ToInt32(selectedStaff.Id);
            EmpName = selectedStaff.FirstName;
            shedulerStaff.Text = selectedStaff.FirstName;
            Application.Current.Properties["LastSelectedStaff"] = selectedStaff.FirstName;
            listData.IsVisible = false;
            Placeholder.IsVisible = true;
            dropdownArrow.RotateTo(0, 200, Easing.SinInOut);
        }

        private void Schedulee_ScheduleCellTapped(object sender, ScheduleTappedEventArgs e)
        {
            
            Navigation.PushAsync(new GetAllocateServiceForEmployeePage(EmpID,EmpName));
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
            catch(Exception e)
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
    }
}