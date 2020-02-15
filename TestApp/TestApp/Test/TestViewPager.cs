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
            get;
        }

      
        #endregion



        public double Rate { get; set; }


        public Action<int, bool> SetPageIndexAction;
        public void SetPageIndex(int targetIndex,bool isSmooth) 
        {
            if (targetIndex==PageIndex||targetIndex<0||targetIndex>=Children.Count)
            {
                return;
            }
            SetPageIndexAction(targetIndex,isSmooth);
        }

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
