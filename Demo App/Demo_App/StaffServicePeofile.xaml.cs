using Demo_App.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StaffServicePeofile : ContentPage
	{
        public int StaffId;
		public StaffServicePeofile (int EmployeeId,Staff staffdata)
		{
			InitializeComponent ();
            StaffId = EmployeeId;
            BindingContext = staffdata;
        }
        private void WorkingDays(object sender, EventArgs args)
        {
            Navigation.PushAsync(new BusinessHoursPage(StaffId));
        }
        private void ServiceProvided(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new ServicesProviderPage(StaffId,ObservableCollection< AssignedServicetoStaff> obj));
        }
        private void BreaksClick(object sender, EventArgs args)
        {

        }
    }
}