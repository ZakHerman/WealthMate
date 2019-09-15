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