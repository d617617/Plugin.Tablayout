using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class TabLayout : Layout<View>, ITabElement
    {
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
            propertyChanged: (obj, o, n) => ((TabLayout)obj).ItemSourcePropChanged()
            );
        public IList ItemSource
        {
            get => (IList)GetValue(ItemSourceProperty);
            set { SetValue(ItemSourceProperty, value); }
        }

        void ItemSourcePropChanged()
        {
            if (ItemSource == null)
            {
                return;
            }
            foreach (var item in ItemSource)
            {
                if (this.ItemTemplate == null)
                {
                    ItemTemplate = new DataTemplate(() => new Label());
                }
                var view = ItemTemplate.CreateContent() as View;
                view.BindingContext = item;
                Children.Add(view);
            }
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

        public TabType TabType { get; set; }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var request = new Size();
            double maxHeight = 0;
            double widthAll = 0;
            foreach (var item in Children)
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
                case TabType.LinearLayoutNoScroll:
                    request.Width = widthConstraint;
                    break;
                default:
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
                case TabType.LinearLayoutNoScroll:
                    LinearLayout(x, y, width, height); 
                    break;
                default:
                    break;
            }
        }

        void GridLayout(double x, double y, double width, double height)
        {
            var itemWidth = width / Children.Count;           
            int count = 0;
            foreach (var item in Children)
            {
                var size = item.Measure(width, height).Request;
                double startX = itemWidth * count+x;
                var itemY = GetCenterY(size.Height,height);
                item.Layout(new Rectangle(startX, itemY, itemWidth, height));
                count++;
            }
        }

        void CenterLayout(double x, double y, double width, double height)
        {
            double widthAll = this.Space * (this.Children.Count - 1);
            Dictionary<View, Size> itemSizes = new Dictionary<View, Size>();
            foreach (var item in Children)
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
                var viewY = GetCenterY(size.Height,height);
                view.Layout(new Rectangle(xAdd, viewY, size.Width, size.Height));
                xAdd += size.Width;                
                count++;               
            }
        }

        void LinearLayout(double x, double y, double width, double height) 
        {
            int count = 0;
            double xAdd = x;
            foreach (var item in Children)
            {
                if (count>0)
                {
                    xAdd += Space;
                }
                var size = item.Measure(width,height).Request;
                var itemY = GetCenterY(size.Height,height);
                item.Layout(new Rectangle(xAdd,itemY,size.Width,size.Height));
                xAdd += size.Width;
                count++;
            }
        }

        double GetCenterY(double itemHeight,double height) 
        {
            return (height + this.Padding.VerticalThickness - itemHeight) / 2; 
        }

        public Point GetPoint(int index)
        {
            var bounds = Children[index].Bounds;
            var point = new Point(bounds.Left,bounds.Top);
            return point;
        }

        public Size GetSize(int index)
        {
            var size = new Size() 
            {
                Width=Children[index].Width,
                Height=Children[index].Height
            };
            return size;
        }

        public Rectangle GetRect(int index)
        {            
            var point = GetPoint(index);
            var size = GetSize(index);
            return new Rectangle(point,size);
        }
    }

    public enum TabType
    {
        LinearLayout,
        Grid,
        Center,
        LinearLayoutNoScroll,
        Custom
    }
}
