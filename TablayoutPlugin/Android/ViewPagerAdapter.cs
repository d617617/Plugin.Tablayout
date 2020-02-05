using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.Android;

namespace Plugin.TablayoutPlugin.Android
{
    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        public IList<Xamarin.Forms.Page> Pages { get; set; }

        public override int Count => Pages.Count;



        public ViewPagerAdapter(FragmentManager fm, IList<Xamarin.Forms.Page> pages)
            :base(fm)
        {
            Pages = new List<Xamarin.Forms.Page>();
            this.Pages = pages;           
        }

        public override Fragment GetItem(int position)
        {
            ViewPagerFragment fragment = new ViewPagerFragment(Pages[position]);
            return fragment;
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
