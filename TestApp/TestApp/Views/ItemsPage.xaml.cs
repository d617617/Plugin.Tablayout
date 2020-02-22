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

           // XFViewPager xFViewPager = new XFViewPager();
            Console.WriteLine();
         
            testLbl.BindingContext = testPager.Rate;            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.testPager.SetPageIndex(3, true);
        }
    }
}