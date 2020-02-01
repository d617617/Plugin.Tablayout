using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.TablayoutPlugin.Shared
{
    public class BaseTabLayout : ContentView, ITabLayoutElement
    {
        public event Action<object, EventArgs> TabItemTapped;
        public event Action<object, EventArgs> ViewPagerScroll;


    }
}
