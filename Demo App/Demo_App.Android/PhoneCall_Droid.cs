using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Demo_App.Droid;
using Demo_App.Model;
using Android.Telephony;
using Demo_App.Model.Helper;


[assembly: Dependency(typeof(PhoneCall_Droid))]
namespace Demo_App.Droid
{
    public class PhoneCall_Droid : IPhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", PhoneNumber));
                var intent = new Intent(Intent.ActionCall,uri);                
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, args) =>
                {
                    //User pressed OK
                })
                .SetMessage(ex.ToString())
                .SetTitle("Android Exception")
                .Show();
            }
        }


        //private static bool IsIntentAvailable(Context context, Intent intent)
        //{
        //    var packageManager = context.PackageManager;

        //    var list = packageManager.QueryIntentServices(intent, 0)
        //        .Union(packageManager.QueryIntentActivities(intent, 0));
        //    if (list.Any())
        //        return true;

        //    TelephonyManager mgr = TelephonyManager.FromContext(context);
        //    return mgr.PhoneType != PhoneType.None;
        //}
    }
}