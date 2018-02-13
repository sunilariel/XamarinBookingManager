using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Demo_App
{
    public class CustomTimePicker : SfPicker
    {
        // Time api is used to modify the Hour collection as per change in Time
        /// <summary>
        /// Time is the acutal DataSource for SfPicker control which will holds the collection of Hour ,Minute and Format
        /// </summary>
        public ObservableCollection<object> Time { get; set; }

        //Minute is the collection of minute numbers

        public ObservableCollection<object> Minute;

        //Hour is the collection of hour numbers

        public ObservableCollection<object> Hour;

        //Format is the collection of AM and PM

        public ObservableCollection<object> Format;
        /// <summary>
        /// Header api is holds the column name for every column in time picker
        /// </summary>

        public ObservableCollection<string> Headers { get; set; }
        public CustomTimePicker()
        {
            Time = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();
            Format = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
            //Labels = new ObservableCollection<string>();
            if (Device.OS == TargetPlatform.Android)
            {
                Headers.Add("HOUR");
                Headers.Add("MINUTE");
                Headers.Add("FORMAT");
            }
            else
            {
                Headers.Add("Hour");
                Headers.Add("Minute");
                Headers.Add("Format");
            }

            //Enable Footer of SfPicker
            ShowFooter = true;

            //Enable Header of SfPicker
            ShowHeader = true;

            //Enable Column Header of SfPicker
            ShowColumnHeader = false;

            //SfPicker header text
            HeaderText = "TIME PICKER";
            Label fromlbl = new Label();
            Label TOlbl = new Label();
            Label lblFrom = new Label();
            Label lblTO = new Label();
            Grid grid = new Grid();
            VerticalOptions = LayoutOptions.FillAndExpand;

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(115, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(115, GridUnitType.Absolute) });
            grid.Children.Add(fromlbl = new Label
            {
                Text = "From",
                TextColor = Color.Black,
                BackgroundColor = Color.Blue,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,

            }, 0, 0);
            grid.Children.Add(TOlbl = new Label
            {
                Text = "To",
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,

            }, 1, 0);

            //this.SelectionChanged += CustomTimePicker_SelectionChanged;

            #region label for showing time

            grid.Children.Add(lblFrom = new Label
            {
                Text = "From",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,

            }, 0, 1);

            grid.Children.Add(lblTO = new Label
            {
                Text = "To",
                TextColor = Color.White,
                BackgroundColor = Color.Blue,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,

            }, 1, 1);
            #endregion


            this.HeaderView = grid;

            //Grid footerGrid = new Grid();
            //footerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //Button button = new Button
            //{
            //    Text = "Done",
            //    Font = Font.SystemFontOfSize(NamedSize.Large),
            //    BorderWidth = 1,
            //    WidthRequest = 100,
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.CenterAndExpand
            //};
            //button.Clicked += OnButtonClicked;

            //this.FooterView = button;

            PopulateTimeCollection();
            this.ItemsSource = Time;

            // Column header text collection
            //this.ColumnHeaderText = Headers;

            var Fromlabel_Tap = new TapGestureRecognizer();
            Fromlabel_Tap.Tapped += (s, e) =>
            {
                fromlbl.BackgroundColor = Color.Blue;
                TOlbl.BackgroundColor = Color.White;
                PopulateTimeCollection();
                
            };

            fromlbl.GestureRecognizers.Add(Fromlabel_Tap);
            ////for To////
            var Tolabel_Tap = new TapGestureRecognizer();
            Tolabel_Tap.Tapped += (s, e) =>
            {
                TOlbl.BackgroundColor = Color.Blue;
                fromlbl.BackgroundColor = Color.White;
                PopulateTimeCollection();
            };

            TOlbl.GestureRecognizers.Add(Tolabel_Tap);
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            TimePickerViewModel obj = new TimePickerViewModel();
            ObservableCollection<object> SelectedTimeValue = new ObservableCollection<object>();

            //Navigation.PushAsync(new BreaksPage(SelectedTimeValue));
        }

        //private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    UpdateDays(Date, e);
        //}

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

            this.OkButtonClicked += CustomeTimePicker_OkButtonClicked;
        }

        private void CustomeTimePicker_OkButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            dynamic SelectedtimefromDialog = e.NewValue;
            string str = string.Empty;
            int cout = 0;
            foreach (var item in SelectedtimefromDialog)
            {
                if(cout < 2)
                {
                    str = str + ":" + item;
                }
                else
                {
                    str = str + " " + item;
                }
                cout++;
            }
           string from= str.TrimStart(':');           
            throw new NotImplementedException();
        }
        

    }
}
