using Demo_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerCommentsForAppointmentPage : ContentPage
	{
        #region GloblesVariables
        string day = "";
        DateTime dateOfBooking;
        public Customer objCust = null;
        public AddAppointments obj = null;
        string PageName = "";
        #endregion

        public CustomerCommentsForAppointmentPage (AddAppointments objAddAppointments, Customer Cust, string Day, DateTime DateOfBooking,string pagename)
		{
            try
            {
                InitializeComponent();
                PageName = pagename;
                day = Day;
                dateOfBooking = DateOfBooking;
                obj = new AddAppointments();
                obj.CompanyId = objAddAppointments.CompanyId;
                obj.ServiceId = objAddAppointments.ServiceId;
                obj.EmployeeId = objAddAppointments.EmployeeId;
                obj.EmployeeName = objAddAppointments.EmployeeName;
                obj.ServiceName = objAddAppointments.ServiceName;
                obj.Cost = objAddAppointments.Cost;
                obj.StartTime = objAddAppointments.StartTime;
                obj.EndTime = objAddAppointments.EndTime;
                obj.TimePeriod = objAddAppointments.TimePeriod;
                objCust = new Customer();
                objCust.Id = Cust.Id;
                objCust.FirstName = Cust.FirstName;
                objCust.LastName = Cust.LastName;
                objCust.UserName = Cust.UserName;
                objCust.Email = Cust.Email;
                objCust.TelephoneNo = Cust.TelephoneNo;
                objCust.Address = Cust.Address;
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public void CustomerSaveNotes(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Notes objNotes = new Notes();
                objNotes.CustomerId = objCust.Id;
                objNotes.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                objNotes.Description = CustomerNote.Text;
                objNotes.WhoAddedThis = "";
                objNotes.CreationDate = "2017-11-08T12:19:27.628Z";

                var data = JsonConvert.SerializeObject(objNotes);
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/AddNote";
                var ApiMethod = "POST";

                var result = PostData(ApiMethod, data, Url);

                if (PageName == "EditAppointment")
                {
                    Navigation.PushAsync(new UpdateAppointmentDetailsPage(obj, day, dateOfBooking));
                }
                else
                {
                    Navigation.PushAsync(new NewAppointmentPage(obj, day, dateOfBooking));
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
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