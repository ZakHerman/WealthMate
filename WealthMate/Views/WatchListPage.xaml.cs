using System;
using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistPage
    {
        private SearchBar _searchBar;
        public ObservableCollection<Stock> WatchListStocks { get; set; }


        public WatchlistPage()
        {
            WatchListStocks = ((App) Application.Current).User.WatchListStocks;
            GenerateExample();

            foreach (var s in WatchListStocks)
            {
                s.UpdateStock();
            }

            InitializeComponent();
        }

        // Event handler for watchlist stock being pressed
        private async void WatchlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        // Default dummy data
        private void GenerateExample()
        {
            WatchListStocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 2, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 1, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 0.01f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 0f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
            WatchListStocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f, LastTrade = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, Shares = 1500, Volume = 100 });
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (Watchlist.DataSource != null)
            {
                Watchlist.DataSource.Filter = FilterWList;
                Watchlist.DataSource.RefreshFilter();
            }
        }
        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> 
        /// <returns></returns>
        private bool FilterWList(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}