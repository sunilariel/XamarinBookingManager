using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Demo_App.Model
{
    public class TestModel : INotifyPropertyChanged
    {
        public string forename, surname;
        public string day;


        public string Forename
        {
            get
            {
                return forename;
            }
            set
            {
                if (forename != value)
                {
                    forename = value;
                    OnPropertyChanged("Forename");
                }
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                if (day != value)
                {
                    day = value;
                    OnPropertyChanged("Day");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }       

    public static class TestModelView
    {
        public static List<TestModel> listofTestModels { get; set; }

        public static List<TestModel> TestModelView_()
        {
            for (var i = 0; i <= 3; i++)
            {
                //TestModel obj = new TestModel();
                //obj.Forename = "S" + i;
                //obj.Day = "D" + i;
                //obj.Surname = "Su" + i;

                TestModelView.listofTestModels.Add(new TestModel{Forename="s"+i, Day= "D" + i, Surname= "Su" + i });
            }
            return listofTestModels;
        }
    }
}

