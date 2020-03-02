﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.TablayoutPlugin.Shared;

namespace Plugin.TablayoutPlugin.Shared
{
    /// <summary>
    /// 负责转换页
    /// </summary>
    public class XFViewPager : Layout<View>
    {
        #region PageIndex

        public int PageIndex
        {
            get; private set;
        }


        #endregion




        public Action<object, EventArgs> PageScrollStopped;
        public event Action<object, EventArgs> PageIndexChanged;
        public event Action<object, PagerScrollEventArgs> PagerScroll;

        #region 由渲染器调用
        public void PageScrollStoppedDone()
        {
            PageScrollStopped?.Invoke(this, null);
        }
        public void PageIndexChangedDone()
        {
            PageIndexChanged?.Invoke(this, null);
        }

        public void PagerScrollEventDone(PagerScrollEventArgs pagerScrollEvent)
        {
            PagerScroll?.Invoke(this, pagerScrollEvent);
        }

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

    public class PagerScrollEventArgs
    {
        /// <summary>
        /// 比例
        /// </summary>
        public double Rate { get; set; }

        public int StartIndex { get; set; }

        /// <summary>
        /// 当前的索引值，在滑动中会变化
        /// </summary>
        public int NowIndex { get; set; }

        /// <summary>
        /// 目标Index
        /// </summary>
        public int TargetIndex { get; set; }
    }
}
