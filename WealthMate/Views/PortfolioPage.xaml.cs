using System.ComponentModel;
using System.Collections.ObjectModel;
using WealthMate.ViewModels;
using WealthMate.Models;
using Xamarin.Forms;
using Syncfusion;

namespace WealthMate.Views
{
    /**LIST VIEW needs to show:
    * STOCK or TERM DEPOSIT
    * CurrentValue (stocks * current price)
    * Total Return (smaller font)
    * Total ReturnRate (smaller font - this is a percentage)
    */
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