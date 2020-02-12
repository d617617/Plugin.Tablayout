using System;
using System.Collections.Generic;
using System.Text;

namespace TabLayout.Tab
{
    public interface ITabLayoutElement
    {
        int TabItemIndex { get; set; }

        event Action<object, EventArgs> TabItemTapped;

        event Action<object, EventArgs> ViewPagerScrolling;

        event Action<object, EventArgs> TabItemIndexChanged;
    }
}
