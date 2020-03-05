using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TestApp.Test;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class TabLayout : Layout<View>, ITabElement
    {
        List<View> sourceViews = null;

        #region DataTemplet
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate), typeof(DataTemplate), typeof(TabLayout),
            default(DataTemplate)
         );

        public DataTemplate ItemTemplate
        {
            get { return GetValue(ItemTemplateProperty) as DataTemplate; }

            set => SetValue(ItemTemplateProperty, value);
        }
        #endregion

        #region ItemSource
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            nameof(ItemSource), typeof(IList), typeof(TabLayout),
            default(IList),
            propertyChanged: (obj, o, n) => ((TabLayout)obj).DataRender()
            );
        public IList ItemSource
        {
            get => (IList)GetValue(ItemSourceProperty);
            set { SetValue(ItemSourceProperty, value); }
        }


        #endregion

        #region Space
        public static readonly BindableProperty SpaceProperty = BindableProperty.Create(
            nameof(Space), typeof(double), typeof(TabLayout),
            10d
            );
        public double Space
        {
            get => (double)GetValue(SpaceProperty);
            set { SetValue(SpaceProperty, value); }
        }
        #endregion

        #region TabType
        public static readonly BindableProperty TabTypeProperty = BindableProperty.Create(
            nameof(TabType), typeof(TabType), typeof(TabLayout),
            TabType.Grid
            );
        public TabType TabType
        {
            get => (TabType)GetValue(TabTypeProperty);
            set { SetValue(TabTypeProperty, value); }
        }
        #endregion

        #region TabItemIndex
        public static readonly BindableProperty TabItemIndexProperty = BindableProperty.Create(
            nameof(TabItemIndex), typeof(int), typeof(TabLayout),
           0
            );
        public int TabItemIndex
        {
            get => (int)GetValue(TabItemIndexProperty);
            set { SetValue(TabItemIndexProperty, value); }
        }
        #endregion

        #region ItemSouceBy    
        public static readonly BindableProperty ItemsSourceByProperty =
            BindableProperty.Create("ItemsSourceBy", typeof(VisualElement), typeof(TestBoxViewWrapper),
                default(VisualElement),
            propertyChanged: (bindable, oldValue, newValue)
         => ((TabLayout)bindable).LinkToViewPager());

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public static VisualElement GetItemsSourceBy(BindableObject bindable)
        {
            return (VisualElement)bindable.GetValue(ItemsSourceByProperty);
        }

        public static void SetItemsSourceBy(BindableObject bindable, VisualElement value)
        {
            bindable.SetValue(ItemsSourceByProperty, value);
        }
        #endregion

        #region AnimateHelper
        public static readonly BindableProperty AnimateHelperProperty = BindableProperty.Create(
            nameof(AnimateHelper), typeof(IAnimateHelper), typeof(TabLayout),
           default(IAnimateHelper),
           propertyChanged: (obj, o, n) =>
           {
               ((TabLayout)obj).LinkToViewPager();
           }
            );
        public IAnimateHelper AnimateHelper
        {
            get => (IAnimateHelper)GetValue(AnimateHelperProperty);
            set { SetValue(AnimateHelperProperty, value); }
        }
        #endregion


        void DataRender()
        {
            if (ItemSource == null)
            {
                sourceViews.Clear();
                return;
            }
            if (ItemTemplate == null)
            {
                return;
            }
            foreach (var item in ItemSource)
            {
                var view = ItemTemplate.CreateContent() as View;
                view.BindingContext = item;
                Children.Add(view);
                sourceViews.Add(view);
            }
        }

        void LinkToViewPager()
        {
            var viewPager = GetItemsSourceBy(this) as TestViewPager;
            if (viewPager == null)
            {
                return;
            }
            if (AnimateHelper != null)
            {
                AnimateHelper.Attatch(this);
                AnimateHelper.SetViewPagerAnimate(viewPager);
            }
        }


        public TabLayout()
        {
            sourceViews = new List<View>();
        }


        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var request = new Size();
            double maxHeight = 0;
            double widthAll = 0;
            foreach (var item in sourceViews)
            {
                var size = item.Measure(widthConstraint, heightConstraint).Request;
                if (size.Height > maxHeight)
                {
                    maxHeight = size.Height;
                }
                widthAll += size.Width;
            }
            request.Height = maxHeight;
            switch (TabType)
            {
                case TabType.LinearLayout:
                    request.Width = widthAll;
                    break;
                case TabType.Grid:
                    request.Width = widthConstraint;
                    break;
                case TabType.Center:
                    request.Width = widthConstraint;
                    break;
            }
            return new SizeRequest(request);
        }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            switch (TabType)
            {
                case TabType.LinearLayout:
                    LinearLayout(x, y, width, height);
                    break;
                case TabType.Grid:
                    GridLayout(x, y, width, height);
                    break;
                case TabType.Center:
                    CenterLayout(x, y, width, height);
                    break;
                default:
                    break;
            }
            if (AnimateHelper != null)
            {
                AnimateHelper.LayoutAnimateElement(x, y, width, height);
            }
        }

        void GridLayout(double x, double y, double width, double height)
        {
            var itemWidth = width / sourceViews.Count;
            int count = 0;
            foreach (var item in sourceViews)
            {
                var size = item.Measure(width, height).Request;
                double startX = itemWidth * count + x;
                var itemY = GetCenterY(size.Height, height);
                item.Layout(new Rectangle(startX, itemY, itemWidth, height));
                count++;
            }
        }

        void CenterLayout(double x, double y, double width, double height)
        {
            double widthAll = this.Space * (this.sourceViews.Count - 1);
            Dictionary<View, Size> itemSizes = new Dictionary<View, Size>();
            foreach (var item in sourceViews)
            {
                var size = item.Measure(width, height).Request;
                itemSizes.Add(item, size);
                widthAll += size.Width;
            }
            var startX = (width - widthAll) / 2;
            var xAdd = startX;
            int count = 0;
            foreach (var item in itemSizes)
            {
                if (count > 0)
                {
                    xAdd += Space;
                }
                var view = item.Key;
                var size = item.Value;
                var viewY = GetCenterY(size.Height, height);
                view.Layout(new Rectangle(xAdd, viewY, size.Width, size.Height));
                xAdd += size.Width;
                count++;
            }
        }

        void LinearLayout(double x, double y, double width, double height)
        {
            int count = 0;
            double xAdd = x;
            foreach (var item in sourceViews)
            {
                if (count > 0)
                {
                    xAdd += Space;
                }
                var size = item.Measure(width, height).Request;
                var itemY = GetCenterY(size.Height, height);
                item.Layout(new Rectangle(xAdd, itemY, size.Width, size.Height));
                xAdd += size.Width;
                count++;
            }
        }

        double GetCenterY(double itemHeight, double height)
        {
            return (height + this.Padding.VerticalThickness - itemHeight) / 2;
        }

        public Point GetPoint(int index)
        {
            var bounds = sourceViews[index].Bounds;
            var point = new Point(bounds.X, bounds.Y);
            return point;
        }

        public Size GetSize(int index)
        {
            var size = new Size()
            {
                Width = sourceViews[index].Width,
                Height = sourceViews[index].Height
            };
            return size;
        }

        public Rectangle GetRect(int index)
        {
            var point = GetPoint(index);
            var size = GetSize(index);
            return new Rectangle(point, size);
        }



    }

    public enum TabType
    {
        LinearLayout,
        Grid,
        Center
    }
}
