using Plugin.TablayoutPlugin.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public interface IAnimateHelper
    {        
        /// <summary>
        /// 添加到Tablayout
        /// </summary>
        /// <param name="parent"></param>
        void Attatch(View parent);

        /// <summary>
        /// 设定ViewPager的事件
        /// </summary>
        /// <param name="viewPager"></param>
        void SetViewPagerAnimate(TestViewPager viewPager);

        /// <summary>
        /// 定位动画元素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void LayoutAnimateElement(double x, double y, double width, double height);

      
    }
}
