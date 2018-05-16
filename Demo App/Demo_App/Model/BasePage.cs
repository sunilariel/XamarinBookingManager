using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Demo_App.Model
{
    public class BasePage : ContentPage
    {
        public void Show()
        {
            DependencyService.Get<IProgressInterface>().Show();
        }
        public void Hide()
        {
            DependencyService.Get<IProgressInterface>().Hide();
        }
        public BasePage()
        {

        }
    }
}
