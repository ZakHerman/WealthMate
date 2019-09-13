using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage
    {
        private SearchBar _searchBar;

        public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();

        public StockListPage()
        {
            GenerateExample();
            foreach (Stock s in Stocks)
            {
                s.UpdateStock();
            }

            InitializeComponent();
        }

        // Event handler for watchlist stock being pressed
        private async void StockListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        // Dummy data to use before using database
        private void GenerateExample()
        {
            Stocks.Add(new Stock { Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 2, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 1, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 0.01f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 0f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 3, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 15f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 4, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
            Stocks.Add(new Stock { Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f, PriceDate = DateTime.Now, PriceOpen = 1.20f, PriceClose = 1.23f, DayHigh = 1.25f, DayLow = 1.18f, FiftyTwoWeekHigh = 1.91f, FiftyTwoWeekLow = 1.01f, DayAverage = 1.15f, Shares = 1500, Volume = 100 });
        }

        //search functionality below
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (StockList.DataSource != null)
            {
                StockList.DataSource.Filter = FilterStocks;
                StockList.DataSource.RefreshFilter();
            }
        }

        private bool FilterStocks(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}