using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Test2
{
    public class MyListView:CollectionView
    {
        public Action TestAction;
        public void Test() 
        {
            TestAction?.Invoke();
            this.Scrolled += MyListView_Scrolled;
        }

        private void MyListView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var a = e.FirstVisibleItemIndex;
        }

    

        public View ConView => this.ItemTemplate.CreateContent() as View;

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
        
            var a1 = this.ItemTemplate.CreateContent() as View;
            var a2=a1.Measure(widthConstraint, heightConstraint);
            var a= base.OnMeasure(widthConstraint, heightConstraint);
            return a;            
        }

    
    }
}
