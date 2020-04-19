using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class BarAnimateHelper : BindableObject, IAnimateHelper
    {
        protected BoxView bar;
        protected TabLayout tabLayout;

        public Color BarColor { get; set; } = Color.Blue;

        public double BarHeight { get; set; } = 3;

        public double BorderRadius { get; set; } = 10;

        public float WidthScale { get; set; } = 1;


        /// <summary>
        /// 将bar推送到tablayout中
        /// </summary>
        /// <param name="parent"></param>
        public void Attatch(View parent)
        {
            if (parent == null)
            {
                return;
            }
            bar = new BoxView();
            bar.BindingContext = this;
            bar.SetBinding(BoxView.BackgroundColorProperty, nameof(BarColor));
            bar.SetBinding(BoxView.CornerRadiusProperty, nameof(BorderRadius));
            bar.SetBinding(BoxView.HeightRequestProperty, nameof(BarHeight));
            tabLayout = parent as TabLayout;
            tabLayout.Children.Add(bar);
        }
     
        public void LayoutAnimateElement(double x, double y, double width, double height)
        {
            var index = tabLayout.TabItemIndex;
            LayoutBar(index);
        }

        protected void LayoutBar(Rectangle rect)
        {
            this.bar.Layout(rect);
        }

        protected void LayoutBar(int index)
        {
            var barTuple = GetRealWidthAndBarX(index);
            LayoutBar(new Rectangle(barTuple.Item2, tabLayout.Height - BarHeight, barTuple.Item1, BarHeight));
        }

        protected Tuple<double, double> GetRealWidthAndBarX(int index)
        {
            var rect = tabLayout.GetRect(index);
            var realWidth = rect.Size.Width * WidthScale;
            var startX = (rect.Size.Width - realWidth) / 2 + rect.X;
            return new Tuple<double, double>(realWidth, startX);
        }

        protected virtual void ViewPagerMove(object arg1, PagerScrollEventArgs scrollArg)
        {
            if (scrollArg.OffsetDirection == 0)
            {
                LayoutBar(scrollArg.NowIndex);               
            }
            else
            {
                var targetBarTuple = GetRealWidthAndBarX(scrollArg.NextPosition);//目标索引item的width和X
                var startBarTuple = GetRealWidthAndBarX(scrollArg.NowIndex);//初始索引item的width和X
                var totalX = Math.Abs(targetBarTuple.Item2 - startBarTuple.Item2);
                var changeX = totalX * scrollArg.Rate;
                var noxX = startBarTuple.Item2;
                if (scrollArg.OffsetDirection==1) //右移
                {
                    noxX += changeX;
                }
                else
                {
                    noxX -= changeX;
                }
                var nowWidth = (targetBarTuple.Item1 - startBarTuple.Item1) * scrollArg.Rate + startBarTuple.Item1;
                LayoutBar(new Rectangle(noxX, bar.Y, nowWidth, BarHeight));
            }                   
        }


        protected void ViewPager_PageIndexChanged(object arg1, EventArgs arg2)
        {
            LayoutBar(((TestViewPager)arg1).PageIndex);
        }

        public void SetViewPagerAnimate(TestViewPager viewPager)
        {
            viewPager.PagerScroll += ViewPagerMove;         
        }


    }
}
