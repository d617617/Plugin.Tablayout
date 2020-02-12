using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Plugin.TablayoutPlugin.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XFViewPager), typeof(Plugin.TablayoutPlugin.Android.XFViewPagerRender))]
namespace Plugin.TablayoutPlugin.Android
{
  
    public class XFViewPagerRender : ViewRenderer<XFViewPager, ViewPager>
    {
        ViewPager _viewPager = null;
        XFViewPager _xFViewPager = null;

        public XFViewPagerRender(Context context)
            : base(context)
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
                    _xFViewPager.BackgroundColor = Xamarin.Forms.Color.Red;
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



    }
}
