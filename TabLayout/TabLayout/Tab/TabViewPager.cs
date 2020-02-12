using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabLayout.Tab
{
    public class TabViewPager : Layout<View>
    {
        public int Position { get; private set; }

        public double ScrollX { get; private set; }

        public double PageWidth { get; private set; }

        public double PageHeight { get; private set; }

        #region 属性

        #region DefaultPostion
        public static readonly BindableProperty DefaultPostionProperty = BindableProperty.Create(
            nameof(DefaultPostion),
            typeof(int),
            typeof(TabViewPager),
            0
           );

        public int DefaultPostion
        {
            get
            {
                return (int)GetValue(DefaultPostionProperty);
            }
            set
            {
                if (value == DefaultPostion)
                {
                    return;
                }
                if (value < 0)
                {
                    value = 0;
                }
                SetValue(DefaultPostionProperty, value);
            }
        }

        #endregion

        #endregion

        #region 事件
        /// <summary>
        /// 核心滚动事件
        /// </summary>
        public event Action<object, EventArgs> ScrollEvent;

        /// <summary>
        /// 位置移动事件
        /// </summary>

        public event Action<object, EventArgs> PositionChangedEvent;
        #endregion

        #region 由渲染器调用
        /// <summary>
        /// 设定索引
        /// </summary>
        /// <param name="position"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetPostionByRender(int position)
        {
            if (position < 0 || position >= Children.Count)
            {
                return;
            }
            Position = position;
        }

        /// <summary>
        /// 设定滚动X
        /// </summary>
        /// <param name="scrollX"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetScrollXByRender(int scrollX)
        {
            this.ScrollX = scrollX / DeviceDisplay.MainDisplayInfo.Density;
        }

        /// <summary>
        /// 索引变更事件触发
        /// </summary>
        /// <param name="eventArgs"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void PositionChangedEventDone(EventArgs eventArgs)
        {
            PositionChangedEvent?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// 滚动事件触发
        /// </summary>
        /// <param name="eventArgs"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ScrollEventDone(EventArgs eventArgs)
        {
            ScrollEvent?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// 滚动至指定的索引
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Action<int, bool> ScrollToByIndexAction;

        /// <summary>
        /// 设定子元素
        /// </summary>
        /// <param name="position"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void LayoutChild(int position, View view = null)
        {
            Rectangle rect = new Rectangle(position * PageWidth, 0, PageWidth, PageHeight);
            View child = view;
            if (view == null)
            {
                child = Children[position];
            }
            child.Layout(rect);
        }
        #endregion



        #region 滚动到指定索引

        public void SetCurrentItem(int position, bool isAnimate = true)
        {
            if (Position == position || position < 0 || position >= Children.Count)
            {
                return;
            }
            ScrollToByIndexAction(position, isAnimate);
        }

        #endregion

        #region 添加子元素
        public void AddView(View view)
        {
            Children.Add(view);
        }
        #endregion

        #region 重载布局


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            PageWidth = width;
            PageHeight = height;
            int count = 0;
            foreach (var item in Children)
            {
                LayoutChild(count, item);
                count++;
            }
        }


        #endregion
    }
}
