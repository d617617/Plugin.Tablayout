using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class ScrollWrapper : ScrollView
    {
        public ScrollWrapper()
        {
            Orientation = ScrollOrientation.Horizontal;
        }




        /// <summary>
        /// 滚动至指定位置，默认为中间
        /// </summary>
        /// <param name="index">指定索引</param>
        /// <param name="offset">偏移</param>
        public void ScrollToIndex(int index, double offset = 0)
        {
            if (Content is TabLayout tablayout)
            {

                if (tablayout.TabIndex == index || tablayout.Width <= this.Width)
                {
                    return;
                }
                double maxScrollX = tablayout.Width - Width;
                var rect = tablayout.GetRect(index);
                var point = rect.Location;
                var centerDistance = (Width - rect.Width) / 2;                                 
                var theoryScrollX = point.X - centerDistance;                
                if (theoryScrollX<0||theoryScrollX>maxScrollX)
                {
                    return;
                }
                else
                {
                    ScrollToAsync(theoryScrollX, 0, true);
                }
            }
        }
    }
}
