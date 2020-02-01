using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.TablayoutPlugin.Shared
{
    public interface ITabLayoutElement
    {



        event Action<object, EventArgs> TabItemTapped;

        event Action<object, EventArgs> ViewPagerScroll;
    }
}
