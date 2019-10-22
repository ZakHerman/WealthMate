using System.ComponentModel;
using Xamarin.Forms;
using WealthMate.ViewModels;

namespace WealthMate.Views.Portfolio
{
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        public PortfolioPage()
        {
            InitializeComponent();
            BindingContext = new PortfolioViewModel();

            // Center the text of the titleview 
            NavBarLayout.Children.Add(NavBarTitle, new Rectangle(0.5, 0.5, 0.9, 1), AbsoluteLayoutFlags.All);
        }
    }
}