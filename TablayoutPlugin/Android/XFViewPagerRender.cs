using Android.Content;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Plugin.TablayoutPlugin.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XFViewPager), typeof(Plugin.TablayoutPlugin.Android.XFViewPagerRender))]
namespace Plugin.TablayoutPlugin.Android
{
  
    public class XFViewPagerRender : ViewRenderer<XFViewPager, ViewPager>
    {
        ViewPager _viewPager = null;
        XFViewPager _xFViewPager = null;

        int XFPagerIndex => _xFViewPager.PageIndex;


        bool isFirst;
        public XFViewPagerRender(Context context)
            : base(context)
        {
            SetWillNotDraw(false);
        }


      
        protected override void OnElementChanged(ElementChangedEventArgs<XFViewPager> e )
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
                    _xFViewPager = sender as XFViewPager;
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
                isFirst = true;
            }
        }

        void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            if (e.PositionOffset == 0)
            {
                _xFViewPager.SetPageIndexByRender(e.Position);
                _xFViewPager.PageIndexChangedDone();
                if (e.Position == _viewPager.CurrentItem)
                {
                    _xFViewPager.PageScrollStoppedDone();
                }
            }
        }

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
            _xFViewPager.PagerScrollEventDone(scrollEvent);
        }


    }
}
