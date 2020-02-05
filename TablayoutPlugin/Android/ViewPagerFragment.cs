using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.Android;

namespace Plugin.TablayoutPlugin.Android
{
    public class ViewPagerFragment:Fragment
    {
        Xamarin.Forms.Page Page { get; } 
        public ViewPagerFragment(Xamarin.Forms.Page page)
        {
            this.Page = page;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view=ConvertXFPageToNative(Page,Context);
            return view;
        }

        public View ConvertXFPageToNative(Xamarin.Forms.Page page, Context context)
        {
            var vRenderer = page.GetRenderer();
            if (vRenderer == null)
            {
                Platform.SetRenderer(page, Platform.CreateRendererWithContext(page, context));
                vRenderer = page.GetRenderer();
            }
            var nativeView = vRenderer.View;
            nativeView.RemoveFromParent();
            vRenderer.Tracker.UpdateLayout();
            return nativeView;
        }
    }
}
