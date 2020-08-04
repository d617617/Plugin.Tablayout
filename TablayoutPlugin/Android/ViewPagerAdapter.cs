using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.TablayoutPlugin.Android
{
    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        public IList<Xamarin.Forms.VisualElement> Elements { get; set; }

        public override int Count => Elements.Count;



        public ViewPagerAdapter(FragmentManager fm, IList<Xamarin.Forms.VisualElement> views)

            : base(fm)
        {
            Elements = new List<Xamarin.Forms.VisualElement>();
            this.Elements = views;
        }


        public override Fragment GetItem(int position)
        {
            ViewPagerFragment fragment = new ViewPagerFragment(Elements[position]);
            return fragment;
        }


        protected override void Dispose(bool disposing)
        {
            Elements.Clear();
            base.Dispose(disposing);
        }

    }
}
