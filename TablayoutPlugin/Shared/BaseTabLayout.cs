using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.TablayoutPlugin.Shared
{
    public abstract class BaseTabLayout : Layout<View>, ITabLayoutElement
    {
        #region PageIndex

        #endregion

        public event Action<object, EventArgs> TabItemTapped;
        public event Action<object, PagerScrollEventArgs> ViewPagerScroll;

        public static readonly BindableProperty ItemsSourceByProperty =
           BindableProperty.Create("ItemsSourceBy", typeof(VisualElement), typeof(BaseTabLayout),
               default(VisualElement), propertyChanged: (bindable, oldValue, newValue)
        => ((BaseTabLayout)bindable).LinkToViewPager(bindable as BaseTabLayout, newValue as XFViewPager));

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public static VisualElement GetItemsSourceBy(BindableObject bindable)
        {
            return (VisualElement)bindable.GetValue(ItemsSourceByProperty);
        }

        public static void SetItemsSourceBy(BindableObject bindable, VisualElement value)
        {
            bindable.SetValue(ItemsSourceByProperty, value);
        }

        protected abstract void LinkToViewPager(BaseTabLayout testBoxView, XFViewPager viewPager);       


       

       
    }
}
