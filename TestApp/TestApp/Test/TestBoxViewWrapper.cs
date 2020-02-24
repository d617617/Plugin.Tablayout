using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test
{
    public class TestBoxViewWrapper : Layout<View>
    {
        int pageCount = 4;

        

        BoxView BoxView = new BoxView() { BackgroundColor = Color.Red, HeightRequest = 8 };
        public TestBoxViewWrapper()
        {
            this.Children.Add(BoxView);

        }



        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            this.BoxView.Layout(new Rectangle(x, y, width / pageCount, 10));
        }



        private void TestViewPager_PagerScroll(object arg1, PagerScrollEventArgs scrollArg)
        {
            var targtPoint = new Point(this.Width / pageCount * scrollArg.TargetIndex, 0);
            var startPoint = new Point(this.Width / pageCount * scrollArg.StartIndex, 0);
            var totalX =Math.Abs( targtPoint.X - startPoint.X);
            var changeX = totalX * scrollArg.Rate;
            var noxX = startPoint.X;
            if (scrollArg.TargetIndex>scrollArg.StartIndex) //右移
            {
                noxX += changeX;
            }
            else
            {
                noxX -= changeX;
            }
            BoxView.Layout(new Rectangle(noxX,0,BoxView.Width,BoxView.Height));
        }

    }
}
