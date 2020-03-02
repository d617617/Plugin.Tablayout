using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test
{
    public class TabHeaderLayout : Layout<View>
    {
        BoxView _underLine;

        #region UnderlineHeight
        public static readonly BindableProperty UnderlineHeightProperty =
     BindableProperty.Create(nameof(UnderlineHeight), typeof(double), typeof(TabHeaderLayout),
         default(double), propertyChanged: (obj, o, n) => ((TabHeaderLayout)obj).SetUnderLineHeight((double)n));

        public double UnderlineHeight
        {
            get => (double)GetValue(UnderlineHeightProperty);
            set => SetValue(UnderlineHeightProperty, value);
        }

        void SetUnderLineHeight(double newHeight)
        {
            this._underLine.HeightRequest = newHeight;
        }
        #endregion

        #region UnderlineColor
        public static readonly BindableProperty UnderlineColorProperty =
   BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(TabHeaderLayout),
       Color.AliceBlue, propertyChanged: (obj, o, n) => ((TabHeaderLayout)obj).SetUnderLineBackColor((Color)n));

        public Color UnderlineColor
        {
            get => (Color)GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
        }

        void SetUnderLineBackColor(Color newColor)
        {
            this._underLine.BackgroundColor = newColor;
        }
        #endregion

        #region UnderlineRadius
        public static readonly BindableProperty UnderlineRadiusProperty =
   BindableProperty.Create(nameof(UnderlineRadius), typeof(double), typeof(TabHeaderLayout),
      default(double), propertyChanged: (obj, o, n) => ((TabHeaderLayout)obj).SetUnderLineRadius((double)n));

        public double UnderlineRadius
        {
            get => (double)GetValue(UnderlineRadiusProperty);
            set => SetValue(UnderlineRadiusProperty, value);
        }

        void SetUnderLineRadius(double radius)
        {
            this._underLine.CornerRadius = radius;
        }
        #endregion

        public TabHeaderLayout()
        {
            _underLine = new BoxView();
            StackLayout a = new StackLayout();
            ListView l = new ListView();
           
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {

        }


    }
}
