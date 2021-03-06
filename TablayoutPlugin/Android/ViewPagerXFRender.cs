﻿using System;
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
using Plugin.TablayoutPlugin.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ViewPagerXF), typeof(Plugin.TablayoutPlugin.Android.ViewPagerXFRender))]
namespace Plugin.TablayoutPlugin.Android
{
    public class ViewPagerXFRender : ViewRenderer<ViewPagerXF, ViewPager>
    {
        MyViewPager _viewPager = null;
        ViewPagerXF _xFViewPager = null;

        int XFPagerIndex => _xFViewPager.PageIndex;


        int _nowScrollX;
        bool _scrollRightDire;
        int _pointState = -1;

        public ViewPagerXFRender(Context context)
            : base(context)
        {

        }


        protected override void OnElementChanged(ElementChangedEventArgs<ViewPagerXF> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                UnRegisterPageEvents();
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    _viewPager = new MyViewPager(Context);
                    SetNativeControl(_viewPager);
                }
                _viewPager.IsNotScrollByTouch = Element.IsNotScrollByTouch;
                _xFViewPager = e.NewElement;
                RegisterPageEvents();
            }

        }

        void UnRegisterPageEvents()
        {
            _viewPager.ScrollChange -= ScrollChange_Default;
            _viewPager.PageScrolled -= PageScrolled_Default;
            _viewPager.PageScrollStateChanged -= PageScrollStateChanged_Default;
            _viewPager.ClearOnPageChangeListeners();
        }

        void RegisterPageEvents()
        {
            _viewPager.ScrollChange += ScrollChange_Default;
            _viewPager.PageScrolled += PageScrolled_Default;
            _viewPager.PageScrollStateChanged += PageScrollStateChanged_Default;
            _xFViewPager.SetPageIndexAction = (index, isSmooth) =>
            {
                _viewPager.SetCurrentItem(index, isSmooth);
            };
        }

        #region OnElementPropertyChanged
        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //    var propName = e.PropertyName;
        //    if (propName == "Renderer")
        //    {
        //        if (_xFViewPager == null)
        //        {
        //            _xFViewPager = sender as ViewPagerXF;
        //            _xFViewPager.SetPageIndexAction = (index, isSmooth) =>
        //            {
        //                _viewPager.SetCurrentItem(index, isSmooth);
        //            };
        //        }
        //    }
        //} 
        #endregion

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (_viewPager.Adapter == null)
            {
                var fm = Context.GetFragmentManager();
                ViewPagerAdapter pagerAdapter = new ViewPagerAdapter(fm, _xFViewPager.PageElements);
                _viewPager.OffscreenPageLimit = _xFViewPager.PageCacheCount;
                _viewPager.Adapter = pagerAdapter;
            }
        }


        void PageScrollStateChanged_Default(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            _pointState = e.State;
            if (_pointState == 0)
            {
                var oldIndex = Element.PageIndex;
                _xFViewPager.SetPageIndexByRender(_viewPager.CurrentItem);
                if (oldIndex != _viewPager.CurrentItem)
                {
                    Element.PageIndexChangedDoneByRender();
                }
            }
        }



        void PageScrolled_Default(object sender, ViewPager.PageScrolledEventArgs e)
        {
            var direction = 1;
            var currItem = _viewPager.CurrentItem;
            PagerScrollEventArgs scrollEvent = new PagerScrollEventArgs()
            {
                StartIndex = XFPagerIndex,
                NowIndex = e.Position
            };
            var pageWidth = Width;

            var nowIndex = _nowScrollX / pageWidth;
            if (e.PositionOffset == 0) //
            {
                direction = 0;
                if (_pointState == 2)
                {
                    scrollEvent.TargetIndex = currItem;
                }
                else if (_pointState == 1)
                {
                    scrollEvent.TargetIndex = nowIndex;
                }
            }
            else
            {
                if (_pointState == 1) //手指按下的状态
                {
                    if (currItem == nowIndex)
                    {
                        direction = 1;
                        scrollEvent.TargetIndex = nowIndex + 1;
                    }
                    else if (currItem == (nowIndex + 1))
                    {
                        direction = -1;
                        scrollEvent.TargetIndex = nowIndex;
                    }
                }
                else if (_pointState == 2) //手指抬起的状态
                {
                    scrollEvent.TargetIndex = currItem;
                    if (currItem == nowIndex || currItem == (nowIndex + 1))
                    {
                        if (_scrollRightDire) //向右
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
            }

            #region 赋值
            if (direction == 1)
            {
                scrollEvent.NowIndex = nowIndex;
                scrollEvent.NextPosition = nowIndex + 1;
                scrollEvent.OffsetDirection = 1;
                scrollEvent.Rate = (_nowScrollX - nowIndex * pageWidth) / (double)pageWidth;
            }
            else if (direction == -1)
            {
                scrollEvent.NextPosition = nowIndex;
                scrollEvent.NowIndex = nowIndex + 1;
                scrollEvent.Rate = Math.Abs(_nowScrollX - scrollEvent.NowIndex * pageWidth) / (double)pageWidth;
                scrollEvent.OffsetDirection = -1;
            }
            else if (direction == 0)
            {
                scrollEvent.NowIndex = nowIndex;
                scrollEvent.Rate = 1;
                scrollEvent.OffsetDirection = 0;
            }

            #endregion

            _xFViewPager.PagerScrollEventDoneByRender(scrollEvent);

            Log.Debug("22", $"手指状态{_pointState},方向{scrollEvent.OffsetDirection} 当前Item{scrollEvent.NowIndex},NextPosition{scrollEvent.NextPosition},rate{scrollEvent.Rate}");
        }


        void ScrollChange_Default(object sender, ScrollChangeEventArgs e)
        {
            _nowScrollX = e.ScrollX;
            _scrollRightDire = e.ScrollX > e.OldScrollX;
        }



        protected override void Dispose(bool disposing)
        {
            UnRegisterPageEvents();
            base.Dispose(disposing);
        }
    }

}