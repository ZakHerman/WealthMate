﻿using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage
    {
        private SearchBar _searchBar;

        public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();

        public StockListPage()
        {
            InitializeComponent();

            GenerateExample();
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
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 2});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 1});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 0.01f});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 0f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 3});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 15f});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 4});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
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