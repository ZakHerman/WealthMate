﻿using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WealthMate.Services;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using System.Collections.ObjectModel;
using System.Linq;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage
    {
        private SearchBar _searchBar;
        public ObservableCollection<Stock> StockList { get; set; }

        public StockListPage()
        {
            StockList = new ObservableCollection<Stock>();
            LoadStocks();
            InitializeComponent();
        }

        private async void LoadStocks()
        {
            await DataService.FetchStocksAsync();
            StockList = DataService.Stocks;
            StockListView.ItemsSource = StockList;
            //           StockList.ItemsSource = DataService.Stocks;
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

            if (StockListView.DataSource != null)
            {
                StockListView.DataSource.Filter = FilterStocks; //filters the data source
                StockListView.DataSource.RefreshFilter(); // refreshes the view
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

        //clears StockList list and re-adds assets to collection
        //this is required due to the return type of OrderBy and OrderByDescending methods
        private void sortList(IOrderedEnumerable<Stock> linqResults)
        {
            var observableC = new ObservableCollection<Stock>(linqResults);
            StockList.Clear();
            foreach (Stock stock in observableC)
            {
                StockList.Add(stock);
            }
        }

        // sorts StockList list according to picker value upon picker index value changing
        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedIndex == 0)
            {
                sortList(StockList.OrderBy(stock => stock.CompanyName));
            }
            else if (picker.SelectedIndex == 1)
            {
                sortList(StockList.OrderByDescending(stock => stock.CurrentPrice));
            }
            else if (picker.SelectedIndex == 2)
            {
                sortList(StockList.OrderBy(stock => stock.CurrentPrice));
            }
            else if (picker.SelectedIndex == 3)
            {
                sortList(StockList.OrderByDescending(stock => stock.DayReturnRate));
            }
        }
    }
}