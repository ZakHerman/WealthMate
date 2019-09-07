using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom); // Set navigation bar to bottom
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false); // Disable tabbed page swiping
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press
        }
    }
}