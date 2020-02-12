using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestView : ContentView
    {
        public TestView()
        {
            InitializeComponent();
        }

        string _title;
        public string Title2
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                lbl.Text = value;
            }
        }

        Color _backColor;

        public Color BackColor
        {
            set
            {
                _backColor = value;
                this.BackgroundColor = _backColor;
            }
        }


    }
}