using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Syncfusion.SfSchedule;
using Telerik.XamarinForms.Input;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using Demo_App.Model;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using XamForms.Controls;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {

        ObservableCollection<Staff> ListofAllStaff = new ObservableCollection<Staff>();



        public FeedbackPage()
        {
            InitializeComponent();

            
           
        }
        public void aaa(object sender,DateTimeEventArgs e)
        {
            var dd = e.DateTime;
        }
    }
}