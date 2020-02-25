using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using TestApp.Test;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TestViewPager), typeof(TestApp.Droid.Test.TestViewPagerRender))]
namespace TestApp.Droid.Test
{
    public class TestViewPagerRender : ViewRenderer<TestViewPager, ViewPager>
    {
        ViewPager _viewPager = null;
        TestViewPager _xFViewPager = null;

        int XFPagerIndex => _xFViewPager.PageIndex;
        

        bool isFirst;
        public TestViewPagerRender(Context context)
            : base(context)
        {
            SetWillNotDraw(false);
        }



        protected override void OnElementChanged(ElementChangedEventArgs<TestViewPager> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    _viewPager = new ViewPager(Context);
                    SetNativeControl(_viewPager);
                }
                // Configure the control and subscribe to event handlers
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var propName = e.PropertyName;
            if (propName == "Renderer")
            {
                if (_xFViewPager == null)
                {
                    _xFViewPager = sender as TestViewPager;
                    _xFViewPager.SetPageIndexAction = (index, isSmooth) =>
                    {
                        _viewPager.SetCurrentItem(index, isSmooth);
                    };
                }
            }
        }

        protected override void OnAttachedToWindow()
        {

            base.OnAttachedToWindow();
            if (_viewPager.Adapter == null)
            {
                var fm = Context.GetFragmentManager();
                ViewPagerAdapter pagerAdapter = new ViewPagerAdapter(fm, _xFViewPager.Children);
                _viewPager.Adapter = pagerAdapter;

            }

        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);

            if (!isFirst)
            {
                _viewPager.ScrollChange += _viewPager_ScrollChange;
                _viewPager.PageScrolled += _viewPager_PageScrolled;
                //if (_xFViewPager.PageIndex != (int)TestViewPager.PageIndexProperty.DefaultValue)
                //{
                //    _viewPager.SetCurrentItem(_xFViewPager.PageIndex, false);                    
                //}
                isFirst = true;
            }
        }

        private void _viewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            //PagerScrollEventArgs scrollEvent = new PagerScrollEventArgs();
            //var rate = e.PositionOffset;
            //if (e.PositionOffset == 0) //若为0，则停止滑动
            //{
            //    rate = 1;//校正滑动停止值
            //}
            //scrollEvent.Rate = rate;
            //_xFViewPager.PagerScrollEventDone(scrollEvent);

            Log.Debug("22", $"{e.PositionOffset},postion:{e.Position},{this.Width}");
        }

        private void _viewPager_ScrollChange(object sender, ScrollChangeEventArgs e)
        {

            var pageWidth = this.Width;
            var nowScrollX = e.ScrollX;
            var nowPageScrollX = pageWidth * XFPagerIndex;
            PagerScrollEventArgs scrollEvent = new PagerScrollEventArgs()
            {
                StartIndex = XFPagerIndex,
                NowIndex = nowScrollX / pageWidth
            };
            var viewPagerItem = _viewPager.CurrentItem;
            var targetIndex = XFPagerIndex;
            if (Math.Abs(viewPagerItem - XFPagerIndex) > 1)
            {
                targetIndex = viewPagerItem;
            }
            else
            {
                if (nowScrollX >= nowPageScrollX)
                {
                    targetIndex = XFPagerIndex + 1;
                }
                else
                {
                    targetIndex = XFPagerIndex - 1;
                }
            }
            scrollEvent.TargetIndex = targetIndex;
            var diffX =Math.Abs( targetIndex * pageWidth - nowPageScrollX);
            var moveX = Math.Abs(e.ScrollX - nowPageScrollX);
            scrollEvent.Rate = moveX / (double)diffX;
        }
    }
}