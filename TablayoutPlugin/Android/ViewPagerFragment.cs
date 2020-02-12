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
        Xamarin.Forms.View XFView { get; } 
        public ViewPagerFragment(Xamarin.Forms.View view)
        {
            this.XFView = view;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view=ConvertXFPageToNative(XFView,Context);
            return view;
        }

        public View ConvertXFPageToNative(Xamarin.Forms.View page, Context context)
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
