using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Fragment = Android.Support.V4.App.Fragment;
using Xamarin.Forms.Platform.Android;

namespace TestApp.Droid.Test
{
    public class ViewPagerFragment : Fragment
    {
        Xamarin.Forms.View XFView { get; }        
        public ViewPagerFragment(Xamarin.Forms.View view)
        {
            this.XFView = view;
        }
      

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = ConvertXFPageToNative(XFView, Context);         
            return view;
        }

        public View ConvertXFPageToNative(Xamarin.Forms.View contentView, Context context)
        {
            var vRenderer = contentView.GetRenderer();
            if (vRenderer == null)
            {
                Platform.SetRenderer(contentView, Platform.CreateRendererWithContext(contentView, context));
                vRenderer = contentView.GetRenderer();
            }
            var nativeView = vRenderer.View;
            nativeView.RemoveFromParent();
            vRenderer.Tracker.UpdateLayout();
            return nativeView;
        }
    

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            if (XFView is IDisposable)
            {
                ((IDisposable)XFView).Dispose();
            }
        }
    }
}