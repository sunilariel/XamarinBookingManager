using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfSchedule.XForms;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using Demo_App.Model;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNewAppointmentsPage : ContentPage
	{
        string CompanyId = Convert.ToString(Application.Current.Properties["CompanyId"]);
        public static SfSchedule schedulee;
        int serviceID;
        public static WorkingHoursofEmployee objbookingSlot;
        public CreateNewAppointmentsPage ()
		{
			InitializeComponent ();
            //objbookingSlot = new WorkingHoursofEmployee();
            //objbookingSlot.ServiceId = service.Id;
            //objbookingSlot.CompanyId = Convert.ToInt32(CompanyId);
            schedulee = new SfSchedule();
            
            var CurrentDate = DateTime.Now;
            DateTime SpecificDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            schedulee.NavigateTo(SpecificDate);

            schedulee.VisibleDatesChangedEvent += Schedule_VisibleDatesChangedEvent;
            schedulee.OnAppointmentLoadedEvent += Schedule_OnAppointmentLoadedEvent;
            schedule.CellTapped += GetAvailableTimeForAppointments;

            //schedulee.OnMonthCellLoadedEvent += Schedule_onMonthRenderedEvent;


            string[] Data = { "8:00 AM", "8:15 AM", "8:30 AM", "8:45 AM" };

            ObservableCollection<string> TimeList = new ObservableCollection<string>();

            foreach (var item in Data)
            {
                TimeList.Add(item);
            }

            //dataGrid.ItemsSource = TimeList;
            BindingContext = TimeList;
        }

        public static SfSchedule getScheduleObj()
        {
            return schedulee;
        }

        private void GetAvailableTimeForAppointments(object sender, CellTappedEventArgs e)
        {
           
            //DisplayAlert("CustomizeHeader", "888888888", "cancel");
            var currentDay = e.Datetime.Day;
            var dateOfBooking = e.Datetime.Date;
            //var apiUrl = Application.Current.Properties["DomainUrl"] + "api/clientreservation/GetFreeBookingSlotsForEmployee?uniqueCompnayReference=" +CompanyId + "&serviceId=" + serviceID + "&employeeId=" + 2 + "&dateOfBooking=" + currentDay + "&day="+"";
            //var result = PostData("GET", "", apiUrl);
        }

        private void Schedule_onMonthRenderedEvent(object sender, MonthCellLoadedEventArgs args)
        {
           
        }

        private void Schedule_OnAppointmentLoadedEvent(object sender, AppointmentLoadedEventArgs args)
        {
            
        }

        private void Schedule_VisibleDatesChangedEvent(object sender, VisibleDatesChangedEventArgs args)
        {

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

                if (SerializedData != "")
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