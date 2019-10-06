using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using AndroidPlatform = Xamarin.Forms.PlatformConfiguration.Android;

namespace WealthMate.Views.Markets
{
    [DesignTimeVisible(false)]
    public partial class MarketPage
    {
        public MarketPage()
        {
            InitializeComponent();

            // Center the text of the title view
            NavBarLayout.Children.Add(NavBarTitle, new Rectangle(0.5, 0.5, 0.9, 1), AbsoluteLayoutFlags.All);

            // Disable default scrolling animation for button press
            On<AndroidPlatform>().SetIsSmoothScrollEnabled(false);

            NavBarTitle.BindingContext = ((App) Xamarin.Forms.Application.Current).User.Portfolio;
        }
    }
}