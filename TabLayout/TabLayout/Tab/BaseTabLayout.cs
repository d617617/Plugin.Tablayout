using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TabLayout.Tab
{
    public class BaseTabLayout : ContentView
    {
        public static readonly BindableProperty ViewPagerProperty =
            BindableProperty.Create("ViewPager",
            typeof(VisualElement),
            typeof(BaseTabLayout),
            default(VisualElement),
            propertyChanged: (bindable, oldValue, newValue) =>
                (bindable as BaseTabLayout).LinkToViewPager(newValue as CarouselView)
            );

        [TypeConverter(typeof(ReferenceTypeConverter))]
        public static VisualElement GetViewPager(BindableObject bindable)
        {
            return (VisualElement)bindable.GetValue(ViewPagerProperty);
        }

        public static void SetViewPager(BindableObject bindable, VisualElement value)
        {
            bindable.SetValue(ViewPagerProperty, value);
        }


        void LinkToViewPager(CarouselView carouselView)
        {
            
        }


    }
}
