using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Services;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public Stock Stock { get; }
        public ObservableCollection<StockHistory> StockHistory{ get; set; }
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        private bool _watched;                                                      //Flag that indicates if stock is being watched

        public Portfolio CurrentPortfolio;
        private SfNumericTextBox numericTextBox;
        private SfNumericTextBox numericTextBox2;
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
            LoadStockHistory(stock.Symbol);
            stock.UpdateStock();

            

            CurrentPortfolio = (Application.Current as App).User.Portfolio;

            WatchListStocks = ((App) Application.Current).User.WatchListStocks;    //Takes users watched list of stocks
            Watched = _watched;

            numericTextBox = new SfNumericTextBox();
            numericTextBox.Value = 0;
            numericTextBox.ValueChanged += Handle_NumSharesChanged;

            numericTextBox2 = new SfNumericTextBox();
            numericTextBox2.Value = 0;
            numericTextBox2.ValueChanged += Handle_PriceChanged;

            InitializeComponent();

            BindingContext = this;     
        }

        private async void LoadStockHistory(string symbol)
        {
            await DataService.FetchStockHistoryAsync(symbol);
            StockHistory = DataService.StockHistory;

            Chart.Series.Add (new LineSeries {
	
                ItemsSource = StockHistory,

                XBindingPath = "Date",

                YBindingPath = "PriceClose"

            });
        }

        private void WatchListStarClicked(object sender, System.EventArgs e)
        {
            if (((App)Application.Current).User.WatchListStocks.Contains(Stock))      //Removes stock and empties star when user no longer wants to watch
            {
                ((App)Application.Current).User.WatchListStocks.Remove(Stock);
                ((ImageButton) sender).Source = "starunfilled.png";
            }
            else
            {
                ((App)Application.Current).User.WatchListStocks.Add(Stock);           //Adds stock and fills star when user wants to watch stock,
                ((ImageButton) sender).Source = "starfilled.png";
            }
        }

        private void AddToPortfolioClicked(object sender, System.EventArgs e)        //Asks for details of shares purchased
        {
            popupLayout.IsOpen = true;
        }

        private void AddInPopupClicked(object sender, System.EventArgs e)        //Adds purchased shares of stock to the users portfolio
        {
            PortfolioPage current = new PortfolioPage();
            float price = float.Parse(numericTextBox2.Value.ToString());
            float noOfShares = float.Parse(numericTextBox.Value.ToString());
            OwnedStock newStock = new OwnedStock(Stock, System.DateTime.Now, price, noOfShares);
            (Application.Current as App).User.Portfolio.OwnedAssets.Add(newStock);
            popupLayout.IsOpen = false;
        }

        private void Handle_NumSharesChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            numericTextBox.Value = e.Value.ToString();

        }

        private void Handle_PriceChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            numericTextBox2.Value = e.Value.ToString();

        }
    }
}