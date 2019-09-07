using WealthMate.Models;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public Stock Stock { get; }

        public StockDetailsPage(Stock stock)
        {
            Stock = stock;
            InitializeComponent();

            BindingContext = this;

            CurrentPriceText.BindingContext = stock;
        }
    }
}