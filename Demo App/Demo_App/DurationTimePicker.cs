﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static Demo_App.NewServicePage;


namespace Demo_App
{
    class DurationTimePicker : SfPicker
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
        public DurationTimePicker()
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
            string PageName;
            PageName = Convert.ToString(Application.Current.Properties["selectPage"]);

            if (PageName == "ServiceCreateAfterRegistration")
            {
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "ServiceCreateAfterRegistration"));
            }
            else if (PageName == "CalenderPage")
            {
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "CalenderPage"));
            }
            else if (PageName == "CustomerPage")
            {
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "CustomerPage"));
            }
            else if (PageName == "ActivityPage")
            {
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "ActivityPage"));
            }
            else if (PageName == "AccountPage")
            {
                Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "AccountPage"));
            }
            else 
            {
            Navigation.PushAsync(new NewServicePage(todaycollection, todaycollectionBuffer, "ServiceCreateAfterLogin"));
            }
            
            
            throw new NotImplementedException();
        }
    }
}
