using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Droid.Test
{
    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        public IList<Xamarin.Forms.View> XFViews { get; set; }

        public IList<Xamarin.Forms.Page> Pages { get; set; }

        public override int Count => /*XFViews*/Pages.Count;



        public ViewPagerAdapter(FragmentManager fm, IList<Xamarin.Forms.View> views)

            : base(fm)
        {
            XFViews = new List<Xamarin.Forms.View>();
            this.XFViews = views;
        }

        public ViewPagerAdapter(FragmentManager fm, IList<Xamarin.Forms.Page> pages)

         : base(fm)
        {
            Pages = new List<Xamarin.Forms.Page>();
            Pages = pages;
        }

        public override Fragment GetItem(int position)
        {
            //ViewPagerFragment fragment = new ViewPagerFragment(XFViews[position]);
            ViewPagerFragment fragment = new ViewPagerFragment(Pages[position]);
            return fragment;
        }


        protected override void Dispose(bool disposing)
        {
            XFViews.Clear();            
            base.Dispose(disposing);
        }

    }
}
