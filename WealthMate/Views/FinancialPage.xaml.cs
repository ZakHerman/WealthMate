using System.ComponentModel;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using AndroidPlatform = Xamarin.Forms.PlatformConfiguration.Android;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class FinancialPage
    {
        public FinancialPage()
        {
            InitializeComponent();

            // Center the text of the titleview
            NavBarLayout.Children.Add(NavBarTitle, new Rectangle(0.5, 0.5, 0.9, 1), AbsoluteLayoutFlags.All);

            // Disable default scrolling animation for button press
            On<AndroidPlatform>().SetIsSmoothScrollEnabled(false);

            NavBarTitle.BindingContext = (Xamarin.Forms.Application.Current as App).User.Portfolio;
        }
    }
}