using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using BigTed;
using Demo_App.Model;
using Xamarin.Forms;
using Demo_App.iOS;

[assembly:Dependency(typeof(ProgressLoader))]
namespace Demo_App.iOS
{
    public class ProgressLoader : IProgressInterface
    {
        public ProgressLoader()
        {

        }

        public void Hide()
        {
            BTProgressHUD.Dismiss();
        }

        public void Show(string title = "Loading")
        {
            BTProgressHUD.Show(title, maskType: ProgressHUD.MaskType.Black);
        }
    }
}