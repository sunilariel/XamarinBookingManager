using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static Demo_App.EditServiceDetails;
using Demo_App.Model;

namespace Demo_App
{
    class EditDurationTimePicker : SfPicker
    {
        // Time api is used to modify the Hour collection as per change in Time
        /// <summary>
        /// Time is the acutal DataSource for SfPicker control which will holds the collection of Hour ,Minute and Format
        /// </summary>
        /// 


        //public ObservableCollection<object> pName { get; set; }
        public ObservableCollection<object> Time { get; set; }

        //Minute is the collection of minute numbers

        public ObservableCollection<object> Minute;

        //Hour is the collection of hour numbers

        public ObservableCollection<object> Hour;

        public ObservableCollection<object> SetTime;

        /// <summary>
        /// Header api is holds the column name for every column in time picker
        /// </summary>

        public ObservableCollection<string> Headers { get; set; }
        public EditDurationTimePicker()
        {
            Time = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
            SetTime = new ObservableCollection<object>();

            //pName = new ObservableCollection<object>();



            if (Device.OS == TargetPlatform.Android)
            {
                Headers.Add("HOUR");
                Headers.Add("MINUTE");
            }
            else
            {
                Headers.Add("Hour");
                Headers.Add("Minute");
            }

            //Enable Footer of SfPicker
            ShowFooter = true;

            //Enable Header of SfPicker
            ShowHeader = true;

            //Enable Column Header of SfPicker
            ShowColumnHeader = false;

            //SfPicker header text
            HeaderText = "Select Service Duration";

            PopulateTimeCollection();
            this.ItemsSource = Time;

            // Column header text collection
            //this.ColumnHeaderText = Headers;           
        }



        private void PopulateTimeCollection()
        {


            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Time = new ObservableCollection<object>();

            //pName = new ObservableCollection<object>();

            //Populate Hour
            for (int i = 0; i <= 23; i++)
            {
                Hour.Add(i.ToString());
            }

            //Populate Minute
            for (int j = 0; j < 60; j++)
            {
                if (j < 10)
                {
                    Minute.Add("0" + j);
                }
                else
                    Minute.Add(j.ToString());
            }

            Time.Add(Hour);
            Time.Add(Minute);
            this.OkButtonClicked += DurationTimePicker_OkButtonClicked;

        }

        private void DurationTimePicker_OkButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            ObservableCollection<object> todaycollectionBuffer = new ObservableCollection<object>();
            todaycollection = (ObservableCollection<Object>)e.NewValue;



            string EserviceId;
            string EserviceName;
            string EserviceCost;
            string EserviceBufferTime;
            EserviceId = Convert.ToString(Application.Current.Properties["EserviceId"]);
            EserviceName = Convert.ToString(Application.Current.Properties["EditServiceName"]);
            EserviceCost = Convert.ToString(Application.Current.Properties["EditServiceCost"]);
            EserviceBufferTime = Convert.ToString(Application.Current.Properties["EserviceBufferTime"]);

            ServiceDetails service = new ServiceDetails();
            service.Id = Convert.ToInt32(EserviceId);
            service.Name = EserviceName;
            service.Cost = EserviceCost;
            service.BufferTimeInMinutes = EserviceBufferTime;
            Navigation.PushAsync(new EditServiceDetails(service, todaycollection, todaycollectionBuffer));


            throw new NotImplementedException();
        }
    }
}
