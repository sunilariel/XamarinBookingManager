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

using Demo_App.Model;
using AndroidHUD;
using Xamarin.Forms;
using Demo_App.Droid;

[assembly: Dependency(typeof(ProgressLoader))]
namespace Demo_App.Droid
{
    public class ProgressLoader : IProgressInterface
    {
        public ProgressLoader()
        {

        }

        public void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Dismiss();
            });
        }        
        public void Show(string title = "Loading")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Show(Forms.Context, status: title, maskType: MaskType.Black);
            });
        }
    }
}