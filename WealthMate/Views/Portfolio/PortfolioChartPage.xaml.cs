using Syncfusion.SfChart.XForms;
using WealthMate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioChartPage
    {
        public PortfolioChartPage()
        {
            InitializeComponent();
            BindingContext = new PortfolioPageVM();
            picker.SelectedIndex = 0;
        }

        
    }
}