using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class BarAnimateHelper : BindableObject, IAnimateHelper
    {
        BoxView bar;
        TabLayout tabLayout;

        public Color BarColor { get; set; } = Color.Black;

        public double BarHeight { get; set; } = 15;

        public double BorderRadius { get; set; } = 10;

        public float WidthScale { get; set; } = 1;

        public float Offset { get; set; } = 5;


        public void Attatch(View parent)
        {
            if (parent==null)
            {
                return;
            }
            bar = new BoxView();
            tabLayout = parent as TabLayout;
            tabLayout.Children.Add(bar);
        }

        public void LayoutAnimateElement(double x, double y, double width, double height)
        {
            var index = tabLayout.TabItemIndex;
            LayoutBar(index);
        }

        void LayoutBar(Rectangle rect)
        {
            this.bar.Layout(rect);
        }

        void LayoutBar(int index)
        {
            var rect = tabLayout.GetRect(index);
            var barTuple = GetRealWidthAndBarX(index);
            LayoutBar(new Rectangle(barTuple.Item2, rect.Y+Offset, barTuple.Item1, BarHeight));
        }

        Tuple<double,double> GetRealWidthAndBarX(int index)
        {
            var rect = tabLayout.GetRect(index);
            var realWidth = rect.Size.Width * WidthScale;
            var startX = (rect.Size.Width - realWidth) / 2 + rect.X;
            return new Tuple<double, double>(realWidth,startX);
        }

        public void ViewPagerMove(object arg1, PagerScrollEventArgs scrollArg)
        {            
            var targetBarTuple = GetRealWidthAndBarX(scrollArg.TargetIndex);//目标索引item的width和X
            var startBarTuple = GetRealWidthAndBarX(scrollArg.StartIndex);//初始索引item的width和X
            var totalX = Math.Abs(targetBarTuple.Item2- startBarTuple.Item2);
            var changeX = totalX * scrollArg.Rate;
            var noxX = startBarTuple.Item2;
            if (scrollArg.TargetIndex > scrollArg.StartIndex) //右移
            {
                noxX += changeX;
            }
            else
            {
                noxX -= changeX;
            }
            var nowWidth = targetBarTuple.Item1 * WidthScale;
            LayoutBar(new Rectangle(noxX,bar.Y,nowWidth,BarHeight));
        }

        public void SetViewPagerAnimate(TestViewPager viewPager)
        {
            viewPager.PagerScroll += ViewPagerMove;
        }

       
    }
}
