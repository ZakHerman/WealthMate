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
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        private bool _watched;                                                      //Flag that indicates if stock is being watched
        public bool Watched
        {
            get
            {
                return _watched;
            }
            set
            {
                _watched = WatchListStocks.Contains(Stock);                         //Checks if user's watchlist contians the viewing stock
            }
        }

        public StockDetailsPage(Stock stock)            //Displays details of selected stock
        {
            Stock = stock;
            stock.UpdateStock();
            StockHistory = new StockHistory();

            WatchListStocks = ((App) Application.Current).User.WatchListStocks;    //Takes users watched list of stocks
            Watched = _watched;

            InitializeComponent();

            BindingContext = this;     
        }

        private void WatchListStarClicked(object sender, System.EventArgs e)
        {
            if (((App) Application.Current).User.WatchListStocks.Contains(Stock))      //Removes stock and empties star when user no longer wants to watch
            {
                ((App) Application.Current).User.WatchListStocks.Remove(Stock);
                ((ImageButton) sender).Source = "starunfilled.png";
            }
            else
            {
                ((App) Application.Current).User.WatchListStocks.Add(Stock);           //Adds stock and fills star when user wants to watch stock,
                ((ImageButton) sender).Source = "starfilled.png";
            }
               
        }

        private void AddToPortfolioClicked(object sender, System.EventArgs e)        //Adds stock to the users portfolio - CURRENTLY IN PROGRESS.
        {
            popupView.IsVisible = true;
            activityIndicator.IsRunning = true;

            float price = 0f;
            float noOfShares = 0f;
            OwnedStock newStock = new OwnedStock(Stock,System.DateTime.Now,price,noOfShares);
            ((App)Application.Current).User.Portfolio.AddAsset(newStock);
        }

    }
}