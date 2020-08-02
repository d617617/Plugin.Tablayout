using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using TestApp.Droid.ToolBarTest;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



namespace TestApp.Droid.ToolBarTest
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new CustomToolbarAppearanceTracker();
        }
    }

    class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
    {
        public void Dispose()
        {
            
        }

        public void ResetAppearance(Android.Support.V7.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
          
        }

        public void SetAppearance(Android.Support.V7.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            toolbar = new Android.Support.V7.Widget.Toolbar(CrossCurrentActivity.Current.AppContext);
            ////toolbar.SetPadding(0, 0, 0, 0);
            toolbar.SetContentInsetsAbsolute(0, 0);
            toolbar.SetBackgroundColor(Color.Yellow.ToAndroid());
            toolbar.Title = "4234";
            toolbar.Background.Alpha = 1;
        }
    }
}