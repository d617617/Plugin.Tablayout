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
        int _nowScrollX;
        bool _scrollDire;

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
                _viewPager.PageScrollStateChanged += _viewPager_PageScrollStateChanged;
                isFirst = true;
            }
        }

        int _pointState = -1;
        private void _viewPager_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            _pointState = e.State;
            if (_pointState==0)
            {
                _xFViewPager.SetPageIndexByRender(_viewPager.CurrentItem);
            }
        }

        private void _viewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            //Log.Debug("22", $"PageSelected：{_viewPager.CurrentItem},xf中的索引为：{_xFViewPager.PageIndex}");
        }

        
        void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            PagerScrollEventArgs scrollEvent = new PagerScrollEventArgs()
            {
                StartIndex = XFPagerIndex,
                NowIndex = e.Position
            };
            var pageWidth = Width;
            var currItem = _viewPager.CurrentItem;
            var nowIndex = _nowScrollX / pageWidth;
            var direction = 1;
            if (_pointState == 1) //手指按下的状态
            {
                if (currItem == nowIndex)
                {
                    direction = 1;
                }
                else if (currItem == (nowIndex + 1))
                {
                    direction = -1;
                }
                else //临界情况
                {
                    direction = 0;
                }

            }
            else if (_pointState == 2) //手指抬起的状态
            {
                double rate = e.PositionOffset;
                if (currItem == nowIndex) //当前索引
                {                  
                    if (_xFViewPager.PageIndex==currItem)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = -1;
                    }
                }
                else if (currItem == nowIndex + 1)
                {
                    if (_scrollDire)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = -1;
                    }
                }
                else
                {
                    if (currItem < nowIndex) //向左
                    {
                        direction = -1;
                    }
                    else if (currItem > nowIndex + 1)
                    {
                        direction = 1;
                    }
                }


            }

            #region 赋值
            if (direction == 1)
            {
                scrollEvent.NowIndex = nowIndex;
                scrollEvent.NextPosition = nowIndex + 1;
                scrollEvent.OffsetDirection = 1;
                scrollEvent.OffsetRate = (_nowScrollX - nowIndex * pageWidth) / (double)pageWidth;
            }
            else if (direction == -1)
            {
                scrollEvent.NextPosition = nowIndex;
                scrollEvent.NowIndex = nowIndex + 1;
                scrollEvent.OffsetRate = Math.Abs(_nowScrollX - scrollEvent.NowIndex * pageWidth) / (double)pageWidth;
                scrollEvent.OffsetDirection = -1;
            }
            else
            {
                scrollEvent.NowIndex = nowIndex;
                scrollEvent.OffsetRate = 0;
                scrollEvent.OffsetDirection = 0;
            }
            #endregion

            _xFViewPager.PagerScrollEventDone(scrollEvent);
            
            Log.Debug("22", $"手指状态{_pointState},方向{scrollEvent.OffsetDirection} 当前Item{scrollEvent.NowIndex},NextPosition{scrollEvent.NextPosition},rate{scrollEvent.OffsetRate}");           
        }



       
      
        void ViewPager_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
            _nowScrollX = e.ScrollX;
            _scrollDire = e.ScrollX > e.OldScrollX;            
        }


    }
  
}