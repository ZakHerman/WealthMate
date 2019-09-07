using System.ComponentModel;
using WealthMate.ViewModels;

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

            BindingContext = new PieChart();
        }
    }
}