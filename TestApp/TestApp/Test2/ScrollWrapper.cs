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
        public void ScrollToIndex(int index,double offset=0) 
        {

            if (Content is TabLayout tablayout)
            {
                if (tablayout.TabIndex == index||tablayout.Width<=this.Width)
                {
                    return;
                }
                var centerWidth = this.Width / 2;
                var point = tablayout.GetPoint(index);
                var x1 = point.X - ScrollX;
                if (x1>=0&&x1<=centerWidth)
                {
                    return;
                }
                else if (x1<0)
                {
                    ScrollToAsync(ScrollX-point.X, 0, true);
                }
                else
                {
                    ScrollToAsync(point.X - ScrollX - centerWidth, 0, true);
                }

            }
           
        }
    }
}
