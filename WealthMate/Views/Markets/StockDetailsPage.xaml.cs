using System;
using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Services;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public Stock Stock { get; }
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        public OwnedStock OwnedStock { get; set; }
        public bool Watched { get; set; }

        //Displays details of selected stock
        public StockDetailsPage(Stock stock)
        {
            Stock = stock;
            OwnedStock = new OwnedStock{Stock = stock, PurchaseDate = DateTime.Now, AssetName = stock.CompanyName};
            LoadStockHistory();
            stock.UpdateStock();

            //Takes users watched list of stocks
            WatchListStocks = ((App)Application.Current).User.WatchListStocks;
            Watched = WatchListStocks.Contains(Stock);

            InitializeComponent();

            BindingContext = this;
        }

        private async void LoadStockHistory()
        {
            await DataService.FetchStockHistoryAsync(Stock.Symbol);

            StockHistoryGraph.ItemsSource = DataService.StockHistory;
        }

        private void WatchListStarClicked(object sender, EventArgs e)
        {
            Watched = WatchListStocks.Contains(Stock);

            if (Watched)
            {
                //Removes stock and empties star when user no longer wants to watch
                WatchListStocks.Remove(Stock);
                ((ImageButton)sender).Source = "starunfilled.png";
            }
            else
            {
                //Adds stock and fills star when user wants to watch stock
                WatchListStocks.Add(Stock);
                ((ImageButton)sender).Source = "starfilled.png";
            }
        }

        //Asks for details of shares purchased
        private void AddToPortfolioClicked(object sender, EventArgs e)
        {
            StockPortfolioForm.IsOpen = true;
        }

        //Adds purchased shares of stock to the users portfolio
        private void AddInPopupClicked(object sender, EventArgs e)
        {
            if(OwnedStock.PurchasedPrice == 0)
            {
                NullValueErrorPopup.IsOpen = true;
            }
            OwnedStock.UpdateOwnedAsset();
            ((App)Application.Current).User.Portfolio.OwnedAssets.Add(OwnedStock);
            StockPortfolioForm.IsOpen = false;
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            int.TryParse(e.Value.ToString(), out var value);
            OwnedStock.SharesPurchased = value;
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedStock.PurchasedPrice = value;
        }
    }
}