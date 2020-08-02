using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Droid.Test
{
    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        public IList<Xamarin.Forms.View> XFViews { get; set; }        

        public override int Count =>XFViews.Count;



        public ViewPagerAdapter(FragmentManager fm, IList<Xamarin.Forms.View> views)

            : base(fm)
        {
            XFViews = new List<Xamarin.Forms.View>();
            this.XFViews = views;
        }

    
        public override Fragment GetItem(int position)
        {          
            ViewPagerFragment fragment = new ViewPagerFragment(XFViews[position]);
            return fragment;
        }


        protected override void Dispose(bool disposing)
        {
            XFViews.Clear();            
            base.Dispose(disposing);
        }

    }
}
