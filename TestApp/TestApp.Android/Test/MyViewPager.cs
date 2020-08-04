using Android.Content;
using Android.Support.V4.View;
using Android.Views;
using System;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Droid.Test
{
    public class MyViewPager : ViewPager, IPagerElement
    {
        public bool IsNotScrollByTouch { get; set; }      

        public event Action<int, int> ViewPagerLayoutEvent;

        public MyViewPager(Context context) : base(context)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ev"></param>
        /// <returns>false为不拦截，true为拦截</returns>
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            return IsNotScrollByTouch ? false : base.OnInterceptTouchEvent(ev);
        }

        /// <summary>
        /// 是否消费事件
        /// </summary>
        /// <param name="e"></param>
        /// <returns>false:可行,不消费,传给父控件 true:可行,消费,拦截事件</returns>
        public override bool OnTouchEvent(MotionEvent e)
        {
            return IsNotScrollByTouch ? false : base.OnTouchEvent(e);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            ViewPagerLayoutEvent?.Invoke(Math.Abs(r - l), Math.Abs(b - t));
        }
    }

    public interface IPagerElement
    {  

        event Action<int, int> ViewPagerLayoutEvent;

    }
}
