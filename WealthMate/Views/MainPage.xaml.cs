using System.ComponentModel;
using WealthMate.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using AndroidPlatform = Xamarin.Forms.PlatformConfiguration.Android;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<AndroidPlatform>().SetToolbarPlacement(ToolbarPlacement.Bottom); // Set navigation bar to bottom
            On<AndroidPlatform>().SetIsSwipePagingEnabled(false); // Disable tabbed page swiping
            On<AndroidPlatform>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press
        }
    }
}