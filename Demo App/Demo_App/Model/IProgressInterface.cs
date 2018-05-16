using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
    public interface IProgressInterface
    {
        void Show(string title = "Loading");
        void Hide();
    }
}
