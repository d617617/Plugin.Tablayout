﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test
{
    public class ViewPagerXF : Layout<View>
    {

        public ViewPagerXF()
        {

        }
        #region PageIndex

        public int PageIndex
        {
            get; private set;
        }


        #endregion

        #region PageCacheCount
        public static readonly BindableProperty PageCacheCountProperty =
     BindableProperty.Create(nameof(PageCacheCount), typeof(int), typeof(ViewPagerXF),
         1, propertyChanged: (obj, o, n) => ((ViewPagerXF)obj).PageCacheCountChanged((int)n));

        public int PageCacheCount
        {
            get => (int)GetValue(PageCacheCountProperty);
            set => SetValue(PageCacheCountProperty, value);
        }

        void PageCacheCountChanged(int newVal)
        {

        }
        #endregion

        public bool IsNotScrollByTouch { get; set; }

        public double ScrollX { get; private set; }

        public event Action<object, EventArgs> PageIndexChanged;
        public event Action<object, PagerScrollEventArgs> PagerScroll;


        #region 由渲染器调用

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void PageIndexChangedDoneByRender()
        {
            PageIndexChanged?.Invoke(this, null);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void PagerScrollEventDoneByRender(PagerScrollEventArgs pagerScrollEvent)
        {
            PagerScroll?.Invoke(this, pagerScrollEvent);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetPageIndexByRender(int pageIndex)
        {
            PageIndex = pageIndex;
        }

        #endregion


        #region 设置页面索引
        public Action<int, bool> SetPageIndexAction;
        public void SetPageIndex(int targetIndex, bool isSmooth)
        {
            if (targetIndex == PageIndex || targetIndex < 0 || targetIndex >= Children.Count)
            {
                return;
            }
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
