using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.TablayoutPlugin.Shared
{
    /// <summary>
    /// 负责转换页
    /// </summary>
    public class XFViewPager : Layout<View>
    {
        /// <summary>
        /// 页集合
        /// </summary>
        public IList<Page> Pages { get; set; }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            
        }
    }
}
