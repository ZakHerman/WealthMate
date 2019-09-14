using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistPage
    {
        private SearchBar _searchBar;
        public ObservableCollection<Stock> WatchListStocks { get; set; }


        public WatchlistPage()
        {
            WatchListStocks = (Application.Current as App).User.WatchListStocks;

            foreach (Stock s in WatchListStocks)
            {
                s.UpdateStock();
            }

            InitializeComponent();
        }

        // Event handler for watchlist stock being pressed
        private async void WatchlistView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        //search functionality below
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (Watchlist.DataSource != null)
            {
                Watchlist.DataSource.Filter = FilterWList;
                Watchlist.DataSource.RefreshFilter();
            }
        }
        //need to change to return stocks, TDs etc.
        private bool FilterWList(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}