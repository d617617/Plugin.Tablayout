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
                    _viewPager = new PageSelectedExt(Context);
                    SetNativeControl(_viewPager);
                }
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
                _viewPager.ScrollChange += ViewPager_ScrollChange;
                _viewPager.PageScrolled += ViewPager_PageScrolled;
                _viewPager.PageSelected += _viewPager_PageSelected;
                isFirst = true;
            }
        }

        private void _viewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            Log.Debug("22", $"PageSelected：{_viewPager.CurrentItem},xf中的索引为：{_xFViewPager.PageIndex}");
        }

        void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            if (e.PositionOffset == 0)
            {
                Log.Debug("22", $"事件前currentItem：{_viewPager.CurrentItem},xf中的索引为：{_xFViewPager.PageIndex}");
                _xFViewPager.SetPageIndexByRender(e.Position);
                Log.Debug("22", $"事件后currentItem：{_viewPager.CurrentItem},xf中的索引为：{_xFViewPager.PageIndex}");
                _xFViewPager.PageIndexChangedDone();
                if (e.Position == _viewPager.CurrentItem)
                {
                    _xFViewPager.PageScrollStoppedDone();
                }
            }
        }



        int cacheIndex = 0;
        void ViewPager_ScrollChange(object sender, ScrollChangeEventArgs e)
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
            var diffX = Math.Abs(targetIndex * pageWidth - nowPageScrollX);
            var moveX = Math.Abs(e.ScrollX - nowPageScrollX);
            scrollEvent.Rate = moveX / (double)diffX;


            var nowIndex = e.ScrollX / pageWidth;//当前的真实索引
            scrollEvent.NowIndex = cacheIndex;
            if (e.ScrollX>cacheIndex*pageWidth)
            {              
                scrollEvent.OffsetDirection = 1;
                scrollEvent.OffsetRate = (e.ScrollX - cacheIndex * pageWidth) / (double)pageWidth;
            }
            else if (e.ScrollX==cacheIndex*pageWidth)
            {
                scrollEvent.OffsetDirection = 0;
                scrollEvent.OffsetRate = 1;
            }
            else
            {
                scrollEvent.OffsetDirection = -1;
                scrollEvent.OffsetRate = Math.Abs(e.ScrollX - cacheIndex * pageWidth) / (double)pageWidth;
            }
            //var offset = e.ScrollX - nowIndex * pageWidth;
            //if (offset == 0)
            //{
            //    scrollEvent.OffsetRate = 1;
            //    scrollEvent.OffsetDirection = 0;
            //}
            //else
            //{
            //    if (offset < 0)
            //    {
            //        scrollEvent.OffsetDirection = -1;
            //    }
            //    else
            //    {
            //        scrollEvent.OffsetDirection = 1;
            //    }
            //    scrollEvent.OffsetRate = Math.Abs(offset) / (double)pageWidth;
            //}
            
            _xFViewPager.PagerScrollEventDone(scrollEvent);
            if (e.ScrollX % pageWidth == 0)
            {
                cacheIndex = nowIndex;
            }
        }


    }

    public class PageSelectedExt : ViewPager
    {
        public PageSelectedExt(Context context) : base(context)
        {
        }

        public override int CurrentItem
        {
            get => base.CurrentItem;
            set
            {
                Log.Debug("22", $"CurrentItem变化了 old:{base.CurrentItem},new:{value}");
                base.CurrentItem = value;
            }

        }
    }
}