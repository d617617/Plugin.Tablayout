using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using TestApp.Test2;
using Xamarin.Forms.Platform.Android;


[assembly: Xamarin.Forms.ExportRenderer(typeof(ScrollWrapper), typeof(TestApp.Droid.Test2.ScrollTablayoutRender))]
namespace TestApp.Droid.Test2
{
    public class ScrollTablayoutRender : ScrollViewRenderer, INestedScrollingChild
    {
        private View inner;
        private float y;
        private Rect normal = new Rect();


        public ScrollTablayoutRender(Context context) : base(context)
        {

        }

        HorizontalScrollView _scrollView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null) return;



            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                    .GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);                
                _scrollView.HorizontalScrollBarEnabled = false;
            }

        }


    }
}