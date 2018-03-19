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
using Demo_App;
using Demo_App.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomRenderer))]
namespace Demo_App.Droid
{
    public class CustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Xamarin.Forms.Forms.Context.GetDrawable(Resource.Drawable.CustomEntery);
                Control.SetPadding(10, 10, 10, 3);
                //Control.Gravity = GravityFlags.CenterHorizontal;
            }
        }        

    }
}