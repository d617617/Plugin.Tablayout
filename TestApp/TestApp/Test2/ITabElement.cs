using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public interface ITabElement
    {
        Point GetPoint(int index);

        Size GetSize(int index);

        Rectangle GetRect(int index);
    }
}
