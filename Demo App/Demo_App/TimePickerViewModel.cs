using System;
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_App
{
    public class TimePickerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> _selectedtime;

        public ObservableCollection<object> SelectedTime
        {

            get { return _selectedtime; }
            set { _selectedtime = value; RaisePropertyChanged("SelectedTime"); }
        }

        public TimePickerViewModel()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            DateTime daytime = DateTime.Now;
            daytime.ToString("dd/mm/yyy HH:mm:ss");

            //Current hour is selected if hour is less than 13 else it is subtracted by 12 to maintain 12hour format
            if (daytime.Hour < 13)
            {
                todaycollection.Add(DateTime.Now.Hour.ToString());
            }
            else
            {
                todaycollection.Add((DateTime.Now.Hour - 12).ToString());
            }

            //Current minute is selected
            todaycollection.Add(DateTime.Now.Minute.ToString());

            //Format is selected as AM if hour is less than 12 else PM is selected
            if (daytime.Hour < 12)
            {
                todaycollection.Add("AM");
            }
            else
            {
                todaycollection.Add("PM");
            }

            //Update the current time
            this.SelectedTime = todaycollection;
        }

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
