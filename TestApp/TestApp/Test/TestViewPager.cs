using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test
{
    public class TestViewPager:Layout<View>
    {
        #region PageIndex
    
        public int PageIndex
        {
            get;private set;
        }


        #endregion
       

        public void SetPageIndexByRender(int pageIndex)
        {
            PageIndex = pageIndex;
           
        }

       
        public event Action<object, EventArgs> PageIndexChanged;
        public event Action<object, PagerScrollEventArgs> PagerScroll;

        public void PagerScrollEventDone(PagerScrollEventArgs pagerScrollEvent) 
        {
            PagerScroll?.Invoke(this,pagerScrollEvent);
        }


        public double Rate { get; set; }

      

        #region 设置页面索引
        public Action<int, bool> SetPageIndexAction;
        public void SetPageIndex(int targetIndex, bool isSmooth)
        {
            //if (targetIndex == PageIndex || targetIndex < 0 || targetIndex >= Children.Count)
            //{
            //    return;
            //}
            SetPageIndexAction(targetIndex, isSmooth);
        }

        #endregion

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (Children.Count == 0)
            {
                return base.OnMeasure(widthConstraint, heightConstraint);
            }
            else
            {
                if (double.IsInfinity(heightConstraint))
                {
                    return new SizeRequest(new Size(widthConstraint, 0));
                }
                else
                {
                    return new SizeRequest(new Size(widthConstraint, heightConstraint));
                }
            }

        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (Children.Count == 0)
            {
                return;
            }
            double itemX = x;
            foreach (var item in Children)
            {
                item.Layout(new Rectangle(itemX, y, width, height));
                itemX += width;
            }
        }
    }
}
