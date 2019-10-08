﻿using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WealthMate.Services;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage
    {
        private SearchBar _searchBar;

        public StockListPage()
        {
            LoadStocks();
            InitializeComponent();
        }

        private async void LoadStocks()
        {
            await DataService.FetchStocksAsync();
            StockList.ItemsSource = DataService.Stocks;
        }

        // Event handler for watchlist stock being pressed
        private async void StockListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stock details page on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar); //set sender to SearchBar

            if (StockList.DataSource != null)
            {
                StockList.DataSource.Filter = FilterStocks; //filters the data source
                StockList.DataSource.RefreshFilter(); // refreshes the view
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> object representing a search return
        /// <returns></returns> boolean value for checking for text in the serach bar
        private bool FilterStocks(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}