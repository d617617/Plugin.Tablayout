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

namespace Plugin.TablayoutPlugin.Android
{
    public class ViewPagerFragment : Fragment
    {
        Xamarin.Forms.VisualElement element { get; set; }
        IPagerElement PagerElement;
        int PagerWidth = 0;
        int PagerHeight = 0;


        public ViewPagerFragment(Xamarin.Forms.VisualElement element)
        {
            this.element = element;

        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = ConvertXFPageToNative(element, Context);
            PagerElement = container as IPagerElement;
            if (container.Height != 0 && container.Width != 0)
            {
                this.PagerWidth = container.Width;
                this.PagerHeight = container.Height;
            }
            return view;
        }


        public override void OnStart()
        {
            base.OnStart();
            if (PagerHeight != 0)
            {
                LayoutXFElement();
            }
            else if (PagerElement != null)
            {
                PagerElement.ViewPagerLayoutEvent += PagerElement_LayoutEvent;
            }
            if (element is Shared.IViewPagerElement pagerElement)
            {
                pagerElement.OnStart();
            }
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            if (element is Shared.IViewPagerElement pagerElement)
            {
                pagerElement.OnDestory();
            }
        }


        /// <summary>
        /// 当viewpager layout的时候，layoutxf的view
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void PagerElement_LayoutEvent(int width, int height)
        {
            this.PagerWidth = width;
            this.PagerHeight = height;
            LayoutXFElement();
            PagerElement.ViewPagerLayoutEvent -= PagerElement_LayoutEvent;
        }

        private void LayoutXFElement()
        {
            var des = Resources.DisplayMetrics.Density;
            var xfWidth = PagerWidth / des;
            var xfHeight = PagerHeight / des;
            element.Measure(xfWidth, xfHeight);
            element.Layout(new Xamarin.Forms.Rectangle(0, 0, xfWidth, xfHeight));
        }

        public View ConvertXFPageToNative(Xamarin.Forms.VisualElement element, Context context)
        {
            var vRenderer = element.GetRenderer();
            if (vRenderer == null)
            {
                Platform.SetRenderer(element, Platform.CreateRendererWithContext(element, context));
                vRenderer = element.GetRenderer();
            }
            var nativeView = vRenderer.View;
            nativeView.RemoveFromParent();
            vRenderer.Tracker.UpdateLayout();
            return nativeView;
        }


    }
}