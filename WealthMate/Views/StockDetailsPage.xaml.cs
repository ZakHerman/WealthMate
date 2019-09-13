using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

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
            stock.UpdateStock();
            StockHistory = new StockHistory();
            InitializeComponent();

            BindingContext = this;     
        }

        private void WatchListStarClicked(object sender, System.EventArgs e)
        {
            if ((Application.Current as App).User.WatchListStocks.Contains(Stock))
                (Application.Current as App).User.WatchListStocks.Remove(Stock);
            else
                (Application.Current as App).User.WatchListStocks.Add(Stock);
        }
    }
}