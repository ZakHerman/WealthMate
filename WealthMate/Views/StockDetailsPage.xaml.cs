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
        private bool _watched;
        public bool Watched
        {
            get
            {
                return _watched;
            }
            set
            {
                _watched = WatchListStocks.Contains(Stock);
            }
        }

        public StockDetailsPage(Stock stock)            //Displays details of selected stock
        {
            Stock = stock;
            stock.UpdateStock();
            StockHistory = new StockHistory();

            WatchListStocks = (Application.Current as App).User.WatchListStocks;
            Watched = _watched;

            InitializeComponent();

            BindingContext = this;     
        }

        private void WatchListStarClicked(object sender, System.EventArgs e)
        {
            if ((Application.Current as App).User.WatchListStocks.Contains(Stock))
            {
                (Application.Current as App).User.WatchListStocks.Remove(Stock);
                (sender as ImageButton).Source = "starunfilled.png";
            }
            else
            {
                (Application.Current as App).User.WatchListStocks.Add(Stock);
                (sender as ImageButton).Source = "starfilled.png";
            }
               
        }
    }
}