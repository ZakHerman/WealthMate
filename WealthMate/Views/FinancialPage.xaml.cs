using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class FinancialPage
    {
        public FinancialPage()
        {
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );

            On<Android>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press
        }
    }
}