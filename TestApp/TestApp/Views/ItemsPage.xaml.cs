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
using Plugin.TablayoutPlugin.Shared;

namespace TestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            List<string> datas = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                var random = new Random();
                var count=random.Next(0,5);
                var str = "";
                for (int j = 0; j < count; j++)
                {
                    str += j.ToString();
                }
                datas.Add($"第{str}项");
            }
            myList2.ItemSource = datas;
            //BindableLayout.SetItemsSource(stLayout, datas);          
            // myList.ItemsSource = new List<string>() { "11","22","33","44","55","666"};
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Random r = new Random();
            this.testPager.SetPageIndex(3, true);
            //myList.Test();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var a = sender as View;
           
        }
    }
}