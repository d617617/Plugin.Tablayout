using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Plugin.TablayoutPlugin.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Platform.Android;

namespace Plugin.TablayoutPlugin.Android
{
    public class XFViewPagerRender:ViewRenderer<XFViewPager,ViewPager>
    {
        ViewPager _viewPager = null;
        XFViewPager _xFViewPager = null;

        public XFViewPagerRender(Context context)
            :base(context)
        {
             SetWillNotDraw(false);
        }


        protected override void OnElementChanged(ElementChangedEventArgs<XFViewPager> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _viewPager = new ViewPager(Context);
                SetNativeControl(_viewPager);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Renderer")
            {
                if (_xFViewPager == null)
                {
                    _xFViewPager = sender as XFViewPager;                   
                }
            }
        }

        protected override void OnAttachedToWindow()
        {
            
            base.OnAttachedToWindow();
            if (_viewPager.Adapter == null)
            {
                List<Fragment> fragments = new List<Fragment>();
                //foreach (var item in _LwyViewPager.Children)
                //{
                //    fragments.Add(new PageFragment(Context, item));
                //}
                //_pager.Adapter = new LwyPageFragAdapter(CrossCurrentActivity.Current.Activity.GetFragmentManager(), fragments);
                //_pager.AddOnPageChangeListener(new ViewPagerChangeListerInstance(this));
                //_pager.OffscreenPageLimit = 2;
            }
        }


        
    }
}
