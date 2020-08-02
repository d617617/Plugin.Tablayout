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
            testPager.PageIndexChanged += TestPager_PageIndexChanged;
            //BindableLayout.SetItemsSource(stLayout, datas);          
            // myList.ItemsSource = new List<string>() { "11","22","33","44","55","666"};
        }

        private void TestPager_PageIndexChanged(object arg1, EventArgs arg2)
        {
            
        }

        //private  async Task Button_Clicked(object sender, EventArgs e)
        //{
        //    Random r = new Random();
        //    //this.myList2.TabType = Test2.TabType.Grid;
        //    ////this.testPager.SetPageIndex(3, true);
        //    ////myList.Test();
        //    //this.wrapper.ScrollToIndex(8);
        //    await Shell.Current.GoToAsync("//test");
        //}

        private async Task TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        bool isStop = true;
         async void Button_Clicked(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    if (isStop)
            //    {
            //        return;
            //    }
            //    await this.testLbl.RotateTo(360);
            //    testLbl.Rotation = 0;
            //}
        }
    }
}