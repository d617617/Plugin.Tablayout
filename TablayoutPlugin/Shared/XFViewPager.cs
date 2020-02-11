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
        Label lbl = new Label() { Text="233ew",FontSize=30};
        /// <summary>
        /// 页集合
        /// </summary>
        public IList<Page> Pages { get; set; }

        public XFViewPager()
        {
            Pages = new List<Xamarin.Forms.Page>();
            this.Children.Add(lbl);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return lbl.Measure(widthConstraint, heightConstraint);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            this.lbl.Layout(new Rectangle(x,y,width,height));
        }
    }
}
