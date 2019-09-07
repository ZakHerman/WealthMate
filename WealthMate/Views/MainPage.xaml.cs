using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom); // Set navigation bar to bottom
            On<Android>().SetIsSwipePagingEnabled(false); // Disable tabbed page swiping
            On<Android>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press
        }
    }
}