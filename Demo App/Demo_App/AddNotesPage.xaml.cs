using Demo_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddNotesPage : ContentPage
    {
        #region GlobleFields
        int CustomerId;
        public Customer objCust = null;
        ObservableCollection<CustomerNotesDetail> ListNotes = new ObservableCollection<CustomerNotesDetail>();
        //ObservableCollection<Notes> notesList = new ObservableCollection<Notes>();
        #endregion

        public AddNotesPage()
        {
            try
            {
                InitializeComponent();
                GetSelectedCustomerById();
                CustomerId = objCust.Id;
                var notesList = GetAllCustomerNotes();

                ObservableCollection<Notes> notesLst = new ObservableCollection<Notes>();
                foreach (var data in notesList)
                {
                    Notes obj = new Notes();
                    obj.CompanyId = data.CompanyId;
                    obj.CreationDate = data.CreationDate;

                    //DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
                    obj.CustomerId = data.CustomerId;
                    obj.Description = data.Description;
                    obj.WhoAddedThis = data.WhoAddedThis;
                    notesLst.Add(obj);
                }
                notesLst.OrderByDescending(x => x.CreationDate);

                string mystring = notesLst[0].Description;

                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<.+?>|&nbsp;");
                mystring = rx.Replace(mystring, "");
                CustomerNote.Text = mystring;

                //if (mystring.Length >= 5)
                //{
                //    string str = mystring.Substring(0, 5);
                //    if (str == "<div>")
                //    {
                //        string mystrings = mystring.Remove(mystring.Length - 6, 6);
                //        string Descriptions = mystrings.Substring(17);
                //        CustomerNote.Text = Descriptions;
                //    }
                //    else
                //    {
                //        CustomerNote.Text = mystring;
                //    }
                //}
                //else
                //{
                //    CustomerNote.Text = mystring;
                //}              
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void GetSelectedCustomerById()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetCustomerById?id=" + Application.Current.Properties["SelectedCustomerId"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objCust = JsonConvert.DeserializeObject<Customer>(result);

            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public ObservableCollection<CustomerNotesDetail> GetAllCustomerNotes()
        {
            try
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var Url = Application.Current.Properties["DomainUrl"] + "api/customer/GetAllCustomerNotes?companyId=" + CompanyId + "&customerId=" + CustomerId;
                var Method = "GET";

                var result = PostData(Method, "", Url);
                ListNotes = JsonConvert.DeserializeObject<ObservableCollection<CustomerNotesDetail>>(result);
                return ListNotes;
            }
            catch (Exception e)
            {
                e.ToString();
                //ObservableCollection<CustomerNotesDetail> objnotes = new ObservableCollection<CustomerNotesDetail>();
                return null;

            }

        }

        public void SaveNotes(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                GetSelectedCustomerById();

                //var l = ListNotes;

                //var Method = "DELETE";
                //var Url = Application.Current.Properties["DomainUrl"] + "api/customer/DeleteCustomerNote?companyId=" + Convert.ToInt32(Application.Current.Properties["CompanyId"]) + "&customerNoteId=" + objCust.Id;
                //PostData(Method, "", Url);

                Notes obj = new Notes();
                //obj.Id = -1;
                if (objCust != null)
                {
                    obj.CustomerId = objCust.Id;
                }
                obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                obj.Description = CustomerNote.Text;
                obj.WhoAddedThis = objCust.FirstName;
                obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                var SerializedData = JsonConvert.SerializeObject(obj);
                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/customer/AddNote";
                var result = PostData("POST", SerializedData, apiUrl);

                for (int PageIndex = Navigation.NavigationStack.Count - 1; PageIndex >= 4; PageIndex--)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[PageIndex]);

                }
                Navigation.PushAsync(new CutomerProfilePage());
                int pCount = Navigation.NavigationStack.Count();

                for (int i = 0; i < pCount; i++)
                {
                    if (i == 3)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[i]);
                    }
                }
                //Navigation.PopAsync(true);
            }
            catch (Exception ex)
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