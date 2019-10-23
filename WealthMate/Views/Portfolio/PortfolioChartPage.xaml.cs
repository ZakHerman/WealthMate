using WealthMate.ViewModels;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioChartPage
    {
        public PortfolioChartPage()
        {
            InitializeComponent();
            BindingContext = new PortfolioViewModel();
            picker.SelectedIndex = 0;
        }
    }
}