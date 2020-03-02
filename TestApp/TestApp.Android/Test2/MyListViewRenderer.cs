using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TestApp.Test2;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyListView), typeof(TestApp.Droid.Test2.MyListViewRenderer))]
namespace TestApp.Droid.Test2
{
    public class MyListViewRenderer : CollectionViewRenderer
    {
        public MyListViewRenderer(Context context) : base(context)
        {
           
        }

        MyListView myListView;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var propName = e.PropertyName;
            if (propName == "Renderer")
            {              
                myListView = sender as MyListView;
                myListView.TestAction += Test;
                var d = myListView.ConView;
                var view = ConvertFormsToNative(d,Context);
                var d1 = d.Measure(9999, 9999).Request.Height;
                myListView.HeightRequest = d1;             
            }
           
        }

         

    

        public  Android.Views.View ConvertFormsToNative( Xamarin.Forms.View view, Context context)
        {
            var vRenderer = view.GetRenderer();
            if (vRenderer == null)
            {
                Platform.SetRenderer(view, Platform.CreateRendererWithContext(view, context));
                vRenderer = view.GetRenderer();
            }
            var nativeView = vRenderer.View;
            nativeView.RemoveFromParent();
            vRenderer.Tracker.UpdateLayout();
            return nativeView;
        }

        public void Test() 
        {
            
            //this.SmoothScrollToPosition(5);          
            var c=this.ChildCount;
            for (int i = 0; i < c; i++)
            {
                var a = this.GetChildAt(i) as ViewGroup;
                
                var count = a.ChildCount;
                Log.Debug("22",$"{a.Left},{count}");
            }
            var view = this.GetChildAt(3) as ViewGroup;
            var a3 = view.GetChildAt(0);
            var position = this.GetChildAdapterPosition(view);
        }

        


    }
}