using System.ComponentModel;
using WealthMate.ViewModels;
using Xamarin.Forms;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        public float TotalValue { get; set; }

        public PortfolioPage()
        {
            TotalValue = 634635.5623f;
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );

            NavBarTitle.BindingContext = this;

            BindingContext = new PieChart();
        }
    }
}