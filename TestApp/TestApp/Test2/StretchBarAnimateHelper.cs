
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class StretchBarAnimateHelper : BarAnimateHelper
    {


        protected override void ViewPagerMove(object arg1, PagerScrollEventArgs scrollArg)
        {
            if (Math.Abs(scrollArg.TargetIndex - scrollArg.StartIndex) > 1)
            {
                
            }

            bool isRight = GetDirection(scrollArg);
            var start = GetRealWidthAndBarX(scrollArg.StartIndex);
            var target = GetRealWidthAndBarX(scrollArg.TargetIndex);
            var rect = isRight ? ToRight(start, target, scrollArg) : ToLeft(start,target,scrollArg);
            LayoutBar(rect);
        }

        bool GetDirection(PagerScrollEventArgs scrollArg)
        {
            return scrollArg.TargetIndex > scrollArg.StartIndex;
        }

        Rectangle ToRight(Tuple<double, double> start, Tuple<double, double> target, PagerScrollEventArgs scrollArg)
        {
            double barWidth = bar.Width, barX = bar.X;
            var stretchDistance = target.Item1 + target.Item2 - (start.Item1 + start.Item2);
            var shrinkDistance = target.Item2 - start.Item2;
            var shouldMove = (stretchDistance + shrinkDistance) * scrollArg.Rate;
            if (shouldMove <= stretchDistance) //处于拉伸阶段
            {
                barWidth = start.Item1 + shouldMove;
            }
            else
            {
                barWidth = target.Item2 + target.Item1 - start.Item2 - (shouldMove - stretchDistance);
                barX = target.Item1 + target.Item2 - barWidth;
            }
            return new Rectangle(barX, bar.Y, barWidth, BarHeight);
        }

        Rectangle ToLeft(Tuple<double, double> start, Tuple<double, double> target, PagerScrollEventArgs scrollArg)
        {
            double barWidth = bar.Width, barX = bar.X;
            var stretchDistance = start.Item2 - target.Item2;
            var shrinkDistance = start.Item2 + start.Item1 - (target.Item2 + target.Item1);
            var shouldMove = (stretchDistance + shrinkDistance) * scrollArg.Rate;
            if (shouldMove <= stretchDistance) //处于拉伸阶段
            {
                barWidth = start.Item1 + shouldMove;
                barX = start.Item2 - shouldMove;
            }
            else
            {
                barWidth = start.Item2 + start.Item1 - target.Item2 - (shouldMove - stretchDistance);
                barX = target.Item2;
            }
            return new Rectangle(barX, bar.Y, barWidth, BarHeight);
        }
    }
}
