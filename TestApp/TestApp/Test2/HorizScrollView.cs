using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class HorizScrollView : ScrollView 
    {
        public HorizScrollView()
        {
            Orientation = ScrollOrientation.Horizontal;
        }

        /// <summary>
        /// 滚动至指定位置，默认为中间
        /// </summary>
        /// <param name="index">指定索引</param>
        /// <param name="offset">偏移</param>
        public void ScrollToIndex(int index,double offset) 
        {
            
        }
    }
}
