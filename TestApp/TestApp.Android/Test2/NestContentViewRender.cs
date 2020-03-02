using System;
using System.Collections.Generic;
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
using TestApp.Test2;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(NestContentView), typeof(TestApp.Droid.Test2.NestContentViewRender))]
namespace TestApp.Droid.Test2
{
    public class NestContentViewRender : ViewRenderer, INestedScrollingParent
    {

        public NestContentViewRender(Context context) : base(context)
        {
           
        }


        public override bool OnStartNestedScroll(Android.Views.View child, Android.Views.View target, [GeneratedEnum] ScrollAxis nestedScrollAxes)
        {
          
           
            var a=this.NestedScrollAxes;
            Log.Debug("22",$"{nestedScrollAxes}");
            return nestedScrollAxes == ScrollAxis.Horizontal;
        }

        public override void OnNestedPreScroll(Android.Views.View target, int dx, int dy, int[] consumed)
        {
            Log.Error("22",$"{dx},{dy}");
            if (dx < 0) //子View向上滚
            {
                
            }
            else //子View有效滚动
            {
                 
            }

        }
    }
}