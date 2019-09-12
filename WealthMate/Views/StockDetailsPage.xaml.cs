using WealthMate.Models;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public Stock Stock { get; }
        public StockHistory StockHistory { get; }

        public StockDetailsPage(Stock stock)            //Displays details of selected stock
        {
            Stock = stock;
            StockHistory = new StockHistory();
            InitializeComponent();

            BindingContext = this;     
        }
    }
}