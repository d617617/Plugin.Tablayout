using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestApp.Models;
using TestApp.Views;
using TestApp.ViewModels;

namespace TestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public string Test { get; set; } = "我i是手动阀手动阀";
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            List<string> datas = new List<string>();
            for (int i = 0; i < 25; i++)
            {

                datas.Add($"第{i + 1}项");
            }
            //myList2.ItemSource = datas;
            var vc = testPager.PageElements;
            testPager.PageIndexChanged += TestPager_PageIndexChanged;
            //BindableLayout.SetItemsSource(stLayout, datas);          
            // myList.ItemsSource = new List<string>() { "11","22","33","44","55","666"};
        }

        public List<TabItem> BottomTabItems { get; set; }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BottomTabItems = new List<TabItem>();
            BottomTabItems.Add(new TabItem()
            {
                Id = 0,
                TabIcon = "学习",
                TabName = "学习"
            });
            BottomTabItems.Add(new TabItem()
            {
                Id = 1,
                TabIcon = "学习",
                TabName = "设备"
            });
            BottomTabItems.Add(new TabItem()
            {
                Id = 2,
                TabIcon = "学习",
                TabName = "设置"
            });
            BottomTabItems.Add(new TabItem()
            {
                Id = 2,
                TabIcon = "学习",
                TabName = "设置"
            });
            BindableLayout.SetItemsSource(bottomTabs, BottomTabItems);

        }

        private void TestPager_PageIndexChanged(object arg1, EventArgs arg2)
        {
            
        }

      
       
    
    }

    public class TabItem
    {
        public int Id { get; set; }
        public string TabName { get; set; }

        public string TabIcon { get; set; }
    }
}