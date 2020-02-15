﻿using System;
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
        #region PageIndex
        public static readonly BindableProperty PageIndexProperty =
 BindableProperty.Create(
     nameof(PageIndex),
     typeof(int),
     typeof(XFViewPager),
     0,
     BindingMode.Default,
     propertyChanged: (obj, o, n) =>
     {
         //((XFViewPager)obj).PageIndexChanged?.Invoke(obj, new EventArgs());
     }
  );

        public int PageIndex
        {
            get => (int)GetValue(PageIndexProperty);
            set => SetValue(PageIndexProperty, value);
        }

        void PageIndexChanged() 
        { 
            
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
